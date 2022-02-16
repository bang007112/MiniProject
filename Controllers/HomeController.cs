using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BBlog.Models;
using BBlog.DB;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Authentication.Facebook;
using Facebook;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace BBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        string appid = string.Empty;
        string appsecret = string.Empty;

        private readonly ProgramDbContext _context;
        

        public HomeController(ILogger<HomeController> logger,ProgramDbContext context)
        {
            _context = context;

            _logger= logger;
            var configuration = GetConfiguration();
            appid = configuration.GetSection("AppID").Value;
            appsecret = configuration.GetSection("AppSecret").Value;

           
        }
        public IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Headers["Referer"].ToString());
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        [HttpGet]
        public IActionResult Facebook()
        {
            var fb = new FacebookClient();
            
            var loginurl = fb.GetLoginUrl(new
            {
                client_id = appid,
                client_secret=appsecret,

                redirect_uri=RedirectUri.AbsoluteUri,
                response_type="code",
                scope= "email"
            });
            return Redirect(loginurl.AbsoluteUri);
        }
        public async Task<IActionResult> FacebookCallbackAsync(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = appid,
                client_secret=appsecret,
                redirect_uri=RedirectUri.AbsoluteUri,
                code=code
            });
            var accesstoken = result.access_token;
            fb.AccessToken = accesstoken;
            dynamic data = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");
            string email = data.email;
            User user = new User();
            if(_context!= null){
                user = _context.Users.FirstOrDefault(e => e.Email == email);
            }
            if(user != null){
                string username = data.email.Split("@")[0];

                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Append("Username", username, options);

                if(email.Equals("bangddh06052001@gmail.com")){
                    return Redirect("/Blog/ManageBlog");
                }
                return RedirectToAction("Index", "Home");
            }else{
                RegisterAsync(data);
                return RedirectToAction("Index", "Home");
            }
            
            
        }
        public async Task RegisterAsync(dynamic data){
            string email = data.email;
            string picture = data.picture.ToString();
            string username = data.email.Split("@")[0];
            bool is_admin;
            if (email.Equals("bangddh06052001@gmail.com")){ 
                is_admin = true;
            } else{ 
                is_admin = false;
            }
            User user = new User{Username = username, Avatar = picture, Email = email, isAdmin = is_admin};
            this._context.Users.Add(user);
            this._context.SaveChanges();
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMinutes(30);

            Response.Cookies.Append("Username", username, options);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact(){
            return View();
        }

        public IActionResult HandlingContact(){
            
            return View();
        }
        public void ManageContact(Contact ct){
            
        }

        public IActionResult Login(){
            return View();
        }
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return Redirect("/Home/Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
