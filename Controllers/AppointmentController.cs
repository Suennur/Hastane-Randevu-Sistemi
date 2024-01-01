using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hastane.Models;

namespace Hastane.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly HospitalDataContext _context;

        public AppointmentController(HospitalDataContext context)
        {
            _context = context;
        }

        // GET: Appointment
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("SessionAdmin") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Admin");
            }
            var hospitalDataContext = _context.Appointments.Include(a => a.doctor).Include(a => a.patient);
            return View(await hospitalDataContext.ToListAsync());
        }
        public async Task<IActionResult> IndexForPatient()
        {
            if (HttpContext.Session.GetString("SessionUser") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Patient");
            }

            string Tc = HttpContext.Session.GetString("SessionUser");

            var patient = await _context.Patients.FirstOrDefaultAsync(p=>p.tc == Tc);

            if (patient == null)
            {
                ViewBag.NoAppointment = "You do not hane any appointment.";
                return RedirectToAction("Index","Patient");
            }

            var hospitalDataContext = _context.Appointments
                                                .Where(a => a.PatientId == patient.PatientId)
                                                .Include(a => a.doctor);
            return View(await hospitalDataContext.ToListAsync());
        }

        // GET: Appointment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("SessionAdmin") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Admin");
            }
            
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.doctor)
                .Include(a => a.patient)
                .FirstOrDefaultAsync(m => m.AppoId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointment/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("SessionAdmin") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Admin");
            }

            ViewData["DoctorId"] = new SelectList(_context.Doctors, "doctorId", "doctorName");
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientName");
            return View();
        }

        // POST: Appointment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppoId,DoctorId,PatientId")] Appointment appointment)
        {
            if (ModelState.IsValid || true)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "doctorId", "doctorName", appointment.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientName", appointment.PatientId);
            return View(appointment);
        }

        // GET: Appointment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("SessionAdmin") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Admin");
            }

            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "doctorId", "doctorName", appointment.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientName", appointment.PatientId);
            return View(appointment);
        }

        // POST: Appointment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppoId,DoctorId,PatientId")] Appointment appointment)
        {
            if (id != appointment.AppoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid || true)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "doctorId", "doctorName", appointment.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientName", appointment.PatientId);
            return View(appointment);
        }

        // GET: Appointment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("SessionAdmin") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Admin");
            }


            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.doctor)
                .Include(a => a.patient)
                .FirstOrDefaultAsync(m => m.AppoId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }
        public async Task<IActionResult> DeleteForPatient(int? id)
        {
            if (HttpContext.Session.GetString("SessionUser") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Patient");
            }


            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.doctor)
                .Include(a => a.patient)
                .FirstOrDefaultAsync(m => m.AppoId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'HospitalDataContext.Appointments'  is null.");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("DeleteForPatient")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedForPatient(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'HospitalDataContext.Appointments'  is null.");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Patient");
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointments?.Any(e => e.AppoId == id)).GetValueOrDefault();
        }
    }
}
