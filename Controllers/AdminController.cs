using Hastane.Models;
using Microsoft.AspNetCore.Mvc;


namespace Hastane.Controllers
{
    public class AdminController : Controller
    {
        HospitalDataContext _context = new HospitalDataContext();
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("SessionAdmin") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Login()
        {
            Admin admin = new Admin();
            return View();
        }

        [HttpPost]
        public IActionResult Login(Admin a)
        {
            var status = _context.Admins.FirstOrDefault(x => x.AdminName == a.AdminName && x.AdminPassword == a.AdminPassword);

            if(status == null)
            {
                ViewBag.Status = 0;

            }
            else
            {
                HttpContext.Session.SetString("SessionAdmin", a.AdminName);

                var cookieOpt = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(5),
                };
                
                return RedirectToAction("Index","Admin");
            }

            return View(a);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Admin");
        }

    }
}
