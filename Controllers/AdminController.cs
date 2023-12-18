using Hastane.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Hastane.Controllers
{
    public class AdminController : Controller
    {
        
        private readonly HastaneDataContext? _context;
        public AdminController(HastaneDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            Admin _admin = new Admin();
            return View ();
        }

        [HttpPost]
        public IActionResult Login(Admin _admin)
        {
            
            HastaneDataContext context = new HastaneDataContext();
            var status = context.Admins.Where(x=>x.AdminName== _admin.AdminName&& x.AdminPassword== _admin.AdminPassword).FirstOrDefault();
            if (status == null)
            {
                ViewBag.LoginStatus = 0;

            }
            else
            {
                HttpContext.Session.SetString("SessionUser", _admin.AdminName);
                var cookieOpt = new CookieOptions
                {
                    Expires = DateTime.Now.AddSeconds(5)
                };
                
                return RedirectToAction("Panel","Admin");
            }
            return View(_admin);
        }
        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Admin");
        }
        public IActionResult Panel() 
        {
            if (HttpContext.Session.GetString("SessionUser") is null)
            {
                TempData["hata"] = "Bu sayfaya erişim yetkiniz yok.";
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
