using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BBlog.DB;
using BBlog.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly ProgramDbContext _context;
        public BlogController(ILogger<BlogController> logger,ProgramDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if(Request.Cookies["Username"]=="bangddh06052001")
            {
                return View();
            }
            return Redirect("/Home/Login");
        }
        [HttpPost]
        public IActionResult CreateBlog(BlogInfo blogInfo, IFormFile imageFile)
        {
                string ImageName = $"id_{blogInfo.BlogTitle}" + Path.GetExtension(imageFile.FileName);
                //Get url To Save
                string saveRelativePath = $"/Image/BlogImage";

                string savePath = Directory.GetCurrentDirectory().Replace("\\", "/") + "/wwwroot" + saveRelativePath + ImageName;

                Directory.CreateDirectory(Path.GetDirectoryName(savePath));

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
                blogInfo.BlogImage = saveRelativePath + ImageName;
                _context.BlogInfos.Add(blogInfo);
                _context.SaveChanges();
            return Redirect("/Home/Index");
        }
        public IActionResult ManageBlog()
        {
            if(Request.Cookies["Username"]=="bangddh06052001")
            {
                IEnumerable<BlogInfo> listblog = this._context.BlogInfos;
                return View(listblog);
            }
            return Redirect("/Home/Login");
        }
        [HttpGet]
        public IActionResult DeleteBlog(int blogID)
        {
            if(Request.Cookies["Username"]=="bangddh06052001")
            {
                BlogInfo b = _context.BlogInfos.Find(blogID);
                _context.BlogInfos.Remove(b);
                _context.SaveChanges();
                return Redirect("/Blog/ManageBlog");
            }
            return Redirect("/Home/Login");
        }
    }
}
