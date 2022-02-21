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
            if(Request.Cookies["Username"]!=null){
            string username = Request.Cookies["Username"]; 
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
        public IActionResult UpdateContact(Contact contact)
        {
            if(ModelState.IsValid){
                _context.Contacts.Update(contact);
                _context.SaveChanges();
            }
            return Redirect("/Contact/Index");
        }
        public IActionResult DeleteContact(int contactID)
        {
            if(contactID != null){
                Contact c = _context.Contacts.Find(contactID);
                _context.Contacts.Add(c);
                _context.SaveChanges();
            }
            return Redirect("/Contact/Index");
        }
        public IActionResult UpdateStatus(string name)
        {
            if(name != null){
                Contact c = _context.Contacts.Find(name);
                _context.Contacts.Update(c);
                _context.SaveChanges();
            }
            return Redirect("/Contact/Index");
        }
    }
}