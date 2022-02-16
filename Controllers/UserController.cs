using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BBlog.DB;
using BBlog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using BBlog.Models;

namespace BBlog.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly ProgramDbContext _context;
        public UserController(ILogger<BlogController> logger,ProgramDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if(Request.Cookies["Username"] != null)
            {
               User user =  _context.Users.FirstOrDefault(u => u.Username == Request.Cookies["Username"]);
                return View(user);
            }
            return NotFound();
        }
        public IActionResult ManageUser()
        {
            if(Request.Cookies["Username"]=="bangddh06052001")
            {
               IEnumerable<User> listUser = this._context.Users;
                return View(listUser);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult DeleteMember(User u)
        {
            if(Request.Cookies["Username"]=="bangddh06052001"){
                return Redirect("/Home/Login");
            }
            if(u == null){
                return NotFound();
            }
            this._context.Users.Remove(u);
            this._context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult UpdateAvatar(IFormFile imageFile)
        {
            User user =  _context.Users.FirstOrDefault(u => u.Username == Request.Cookies["Username"]);
            if(Request.Cookies["Username"].ToString() != "")
            {
               string ImageName = $"id_{user.Username}" + Path.GetExtension(imageFile.FileName);
                //Get url To Save
                string saveRelativePath = $"/Image/UserImage";

                string savePath = Directory.GetCurrentDirectory().Replace("\\", "/") + "/wwwroot" + saveRelativePath + ImageName;

                Directory.CreateDirectory(Path.GetDirectoryName(savePath));

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
                user.Avatar = saveRelativePath + ImageName;
                _context.Users.Update(user);
                _context.SaveChanges();
                return Redirect("/User/Index");
            }
            return NotFound();
        }

    }
}