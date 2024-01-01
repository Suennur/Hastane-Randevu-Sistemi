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
    public class DoctorController : Controller
    {
        private readonly HospitalDataContext _context;

        public DoctorController(HospitalDataContext context)
        {
            _context = context;
        }

        // GET: Doctor
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("SessionAdmin") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Admin");
            }
            /*return _context.Doctors != null ? 
                          View(await _context.Doctors.ToListAsync()) :
                          Problem("Entity set 'HospitalDataContext.Doctors'  is null.");
            */
            var doctorsWithPoliclinics = _context.Doctors
                                         .Include(d => d.policlinic) 
                                         .ToListAsync();
            return View(await doctorsWithPoliclinics);
        }
        public async Task<IActionResult> IndexForPatient()
        {
            if (HttpContext.Session.GetString("SessionUser") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Patient");
            }
            var doctorsWithPoliclinics = _context.Doctors
                                         .Include(d => d.policlinic)
                                         .ToListAsync();
            return View(await doctorsWithPoliclinics);
        }

        // GET: Doctor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("SessionAdmin") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Admin");
            }
            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .Include(d => d.policlinic)
                .FirstOrDefaultAsync(m => m.doctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctor/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("SessionAdmin") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Admin");
            }
            ViewData["PoliclinicList"] = new SelectList(_context.Policlinics, "PolicId", "PolicName");
            return View();
        }

        // POST: Doctor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("doctorId,doctorName,polic,workTime")] Doctor doctor)
        {
            if (ModelState.IsValid || true)
            {
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PoliclinicList"] = new SelectList(_context.Policlinics, "PolicId", "PolicName");
            return View(doctor);
        }

        // GET: Doctor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetString("SessionAdmin") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Admin");
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            ViewData["PoliclinicList"] = new SelectList(_context.Policlinics, "PolicId", "PolicName");
            return View(doctor);
            
        }

        // POST: Doctor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("doctorId,doctorName,polic,workTime")] Doctor doctor)
        {
            if (id != doctor.doctorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid || true)
            {
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.doctorId))
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
            ViewData["PoliclinicList"] = new SelectList(_context.Policlinics, "PolicId", "PolicName");
            return View(doctor);
        }

        // GET: Doctor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("SessionAdmin") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Admin");
            }
            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .Include(d => d.policlinic)
                .FirstOrDefaultAsync(m => m.doctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Doctors == null)
            {
                return Problem("Entity set 'HospitalDataContext.Doctors'  is null.");
            }
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
          return (_context.Doctors?.Any(e => e.doctorId == id)).GetValueOrDefault();
        }
    }
}
