using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaEase.Data;
using PharmaEase.Models;

namespace PharmaEase.Controllers
{
    public class PharmacistsController : Controller
    {
        private readonly PharmaEaseContext _context;

        public PharmacistsController(PharmaEaseContext context)
        {
            _context = context;
        }

        // GET: Pharmacists
        public async Task<IActionResult> Index()
        {
            var pharmaEaseContext = _context.Pharmacist.Include(p => p.Pharmacy);
            return View(await pharmaEaseContext.ToListAsync());
        }

        // GET: Pharmacists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pharmacist == null)
            {
                return NotFound();
            }

            var pharmacist = await _context.Pharmacist
                .Include(p => p.Pharmacy)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (pharmacist == null)
            {
                return NotFound();
            }

            return View(pharmacist);
        }

        // GET: Pharmacists/Create
        public IActionResult Create()
        {
            ViewData["WorksAt"] = new SelectList(_context.Set<Pharmacy>(), "PharmacyId", "PharmacyId");
            return View();
        }

        // POST: Pharmacists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Fname,Lname,ApprovAdmindID,WorksAt")] Pharmacist pharmacist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pharmacist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorksAt"] = new SelectList(_context.Set<Pharmacy>(), "PharmacyId", "PharmacyId", pharmacist.WorksAt);
            return View(pharmacist);
        }

        // GET: Pharmacists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pharmacist == null)
            {
                return NotFound();
            }

            var pharmacist = await _context.Pharmacist.FindAsync(id);
            if (pharmacist == null)
            {
                return NotFound();
            }
            ViewData["WorksAt"] = new SelectList(_context.Set<Pharmacy>(), "PharmacyId", "PharmacyId", pharmacist.WorksAt);
            return View(pharmacist);
        }

        // POST: Pharmacists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,Fname,Lname,ApprovAdmindID,WorksAt")] Pharmacist pharmacist)
        {
            if (id != pharmacist.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pharmacist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PharmacistExists(pharmacist.EmployeeId))
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
            ViewData["WorksAt"] = new SelectList(_context.Set<Pharmacy>(), "PharmacyId", "PharmacyId", pharmacist.WorksAt);
            return View(pharmacist);
        }

        // GET: Pharmacists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pharmacist == null)
            {
                return NotFound();
            }

            var pharmacist = await _context.Pharmacist
                .Include(p => p.Pharmacy)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (pharmacist == null)
            {
                return NotFound();
            }

            return View(pharmacist);
        }

        // POST: Pharmacists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pharmacist == null)
            {
                return Problem("Entity set 'PharmaEaseContext.Pharmacist'  is null.");
            }
            var pharmacist = await _context.Pharmacist.FindAsync(id);
            if (pharmacist != null)
            {
                _context.Pharmacist.Remove(pharmacist);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PharmacistExists(int id)
        {
          return _context.Pharmacist.Any(e => e.EmployeeId == id);
        }
    }
}
