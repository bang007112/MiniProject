using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBlog.DB;
using BBlog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BBlog.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly ProgramDbContext _context;
        public ContactController(ILogger<BlogController> logger,ProgramDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("Username")!=null){
            string username = HttpContext.Session.GetString("Username"); 
            if(_context.Users.Find(username).isAdmin==true)
            {
                IEnumerable<Contact> listContact = this._context.Contacts;
                return View(listContact);
            }
            }
            return NotFound();
        }
        
        public IActionResult CreateContact(Contact contact)
        {
            if(ModelState.IsValid){
                _context.Contacts.Add(contact);
                _context.SaveChanges();
            }
            return Redirect("/Home/Index");
        }
    }
}