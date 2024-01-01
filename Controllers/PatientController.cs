using Hastane.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity;
using System.Numerics;

namespace Hastane.Controllers
{
    public class PatientController : Controller
    {

        HospitalDataContext _context = new HospitalDataContext();
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("SessionUser") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login");
            }
            
            return View();
        }

        public IActionResult Signin()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Signin([Bind("PatientName,PatientSurname,tc,PatientPassword")] Patient p)
        {
            _context.Patients.Add(p);
            _context.SaveChanges();

            return RedirectToAction("Login");


        }

        public IActionResult Login()
        {
            Patient patient = new Patient();
            return View();
        }

        [HttpPost]
        public IActionResult Login(Patient p)
        {
            var status = _context.Patients.FirstOrDefault(x => x.tc == p.tc && x.PatientPassword == p.PatientPassword);
            
            if (status == null)
            {
                ViewBag.Status = 0;
            }
            else
            {
                HttpContext.Session.SetString("SessionUser", p.tc);
                var cookieOpt = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(5),
                };

                return RedirectToAction("Index", "Patient");

            }
            return View(p);

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Patient");
        }
        
        public async Task<IActionResult> MakeAppointment()
        {
            if (HttpContext.Session.GetString("SessionUser") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login");
            }
            
            ViewData["DoctorList"] = new SelectList(_context.Doctors, "doctorId", "doctorName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeAppointment([Bind("AppoId, DoctorId")]Appointment appointment)
        {

            string Tc = HttpContext.Session.GetString("SessionUser");
            Patient patient = _context.Patients.FirstOrDefault(p => p.tc == Tc);
            appointment.PatientId = patient.PatientId;
            if (ModelState.IsValid || true)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["PoliclinicList"] = new SelectList(_context.Policlinics, "PolicId", "PolicName");
            ViewData["DoctorList"] = new SelectList(_context.Doctors, "doctorId", "doctorName" );
            ViewData["WorkDayList"] = new SelectList(_context.Doctors, "doctorId", "workTime");
            //ViewData["PatientList"] = new SelectList(_context.Patients, "PatientId", "PatientName");
            return View(appointment);
        }

    }
}
