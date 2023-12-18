using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Has.Models;

namespace Has.Controllers
{
    public class PoliclinicController : Controller
    {
        private readonly HasDataContext _context;

        public PoliclinicController(HasDataContext context)
        {
            _context = context;
        }

        // GET: Policlinic
        public async Task<IActionResult> Index()
        {
              return _context.Policlinics != null ? 
                          View(await _context.Policlinics.ToListAsync()) :
                          Problem("Entity set 'HasDataContext.Policlinics'  is null.");
        }

        // GET: Policlinic/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Policlinics == null)
            {
                return NotFound();
            }

            var policlinic = await _context.Policlinics
                .FirstOrDefaultAsync(m => m.PolicID == id);
            if (policlinic == null)
            {
                return NotFound();
            }

            return View(policlinic);
        }

        // GET: Policlinic/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Policlinic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PolicID,PolicName")] Policlinic policlinic)
        {
            if (ModelState.IsValid)
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
        public async Task<IActionResult> Edit(int id, [Bind("PolicID,PolicName")] Policlinic policlinic)
        {
            if (id != policlinic.PolicID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policlinic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoliclinicExists(policlinic.PolicID))
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
            if (id == null || _context.Policlinics == null)
            {
                return NotFound();
            }

            var policlinic = await _context.Policlinics
                .FirstOrDefaultAsync(m => m.PolicID == id);
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
                return Problem("Entity set 'HasDataContext.Policlinics'  is null.");
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
          return (_context.Policlinics?.Any(e => e.PolicID == id)).GetValueOrDefault();
        }
    }
}
