using Hastane.Migrations;
using Hastane.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hastane.Controllers
{
    public class PatientController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            List<Patient> patientlist = new List<Patient>();
            return View();
        }
        [HttpPost]
        public IActionResult Login(List<Patient> patients)
        {
            HastaneDataContext _context = new HastaneDataContext();
            foreach (Patient patient in patients)
            {
                var status = _context.Patients.Where(x => x.PatientTC == patient.PatientTC && x.PatientPassword == patient.PatientPassword);
                if (status == null)
                {
                    ViewBag.Status = 0;
                }
                else
                {
                    HttpContext.Session.SetString("SessionUser", patient.PatientTC);
                    var cookieOpt = new CookieOptions
                    {
                        Expires = DateTime.Now.AddSeconds(5)
                    };

                    return RedirectToAction("Index", "Patient");
                }
                
            }
            return View(patients);
            
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Patient");
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("SessionUser") is null)
            {
                TempData["hata"] = "Bu sayfaya erişim yetkiniz yok.";
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Signin()
        {
            return View();
        }
    }
}
