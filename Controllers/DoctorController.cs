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
    public class PoliclinicController : Controller
    {
        private readonly HospitalDataContext _context;

        public PoliclinicController(HospitalDataContext context)
        {
            _context = context;
        }

        // GET: Policlinic
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("SessionAdmin") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login","Admin");
            }

            return _context.Policlinics != null ? 
                          View(await _context.Policlinics.ToListAsync()) :
                          Problem("Entity set 'HospitalDataContext.Policlinics'  is null.");
            
            
        }

        // GET: Policlinic/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("SessionAdmin") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Admin");
            }

            if (id == null || _context.Policlinics == null)
            {
                return NotFound();
            }


            var policlinic = await _context.Policlinics
                .FirstOrDefaultAsync(m => m.PolicId == id);
            if (policlinic == null)
            {
                return NotFound();
            }

            return View(policlinic);
        }

        // GET: Policlinic/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("SessionAdmin") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        // POST: Policlinic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PolicId,PolicName")] Policlinic policlinic)
        {
            if (ModelState.IsValid || true)
            {
                _context.Add(policlinic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(policlinic);
        }

        // GET: Policlinic/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("SessionAdmin") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Admin");
            }
            if (id == null || _context.Policlinics == null)
            {
                return NotFound();
            }

            var policlinic = await _context.Policlinics.FindAsync(id);
            if (policlinic == null)
            {
                return NotFound();
            }
            return View(policlinic);
        }

        // POST: Policlinic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PolicId,PolicName")] Policlinic policlinic)
        {
            if (id != policlinic.PolicId)
            {
                return NotFound();
            }

            if (ModelState.IsValid || true)
            {
                try
                {
                    _context.Update(policlinic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoliclinicExists(policlinic.PolicId))
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
            return View(policlinic);
        }

        // GET: Policlinic/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("SessionAdmin") is null)
            {
                TempData["error"] = "You are not authorized to access this page.";
                return RedirectToAction("Login", "Admin");
            }

            if (id == null || _context.Policlinics == null)
            {
                return NotFound();
            }

            var policlinic = await _context.Policlinics
                .FirstOrDefaultAsync(m => m.PolicId == id);
            if (policlinic == null)
            {
                return NotFound();
            }

            return View(policlinic);
        }

        // POST: Policlinic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Policlinics == null)
            {
                return Problem("Entity set 'HospitalDataContext.Policlinics'  is null.");
            }
            var policlinic = await _context.Policlinics.FindAsync(id);
            if (policlinic != null)
            {
                _context.Policlinics.Remove(policlinic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoliclinicExists(int id)
        {
          return (_context.Policlinics?.Any(e => e.PolicId == id)).GetValueOrDefault();
        }
    }
}
