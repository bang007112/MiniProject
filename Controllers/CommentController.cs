using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BBlog.DB;
using BBlog.Models;

namespace BBlog.Controllers
{

    public class CommentController : Controller
    {
        private readonly ILogger<CommentController> _logger;

        private readonly ProgramDbContext _context;
        public CommentController(ILogger<CommentController> logger, ProgramDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public IActionResult OnPost(BlogComment obj) {
            if(obj.Username == Request.Cookies["Username"]){
                _context.BlogComments.Add(obj);
                _context.SaveChanges();
            }
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}