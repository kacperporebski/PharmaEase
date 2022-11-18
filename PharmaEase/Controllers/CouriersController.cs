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
    public class CouriersController : Controller
    {
        private readonly PharmaEaseContext _context;

        public CouriersController(PharmaEaseContext context)
        {
            _context = context;
        }

        // GET: Couriers
        public async Task<IActionResult> Index()
        {
              return View(await _context.Courier.ToListAsync());
        }

        // GET: Couriers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Courier == null)
            {
                return NotFound();
            }

            var courier = await _context.Courier
                .FirstOrDefaultAsync(m => m.CourierId == id);
            if (courier == null)
            {
                return NotFound();
            }

            return View(courier);
        }

        // GET: Couriers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Couriers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourierId,Name,OperatingRange")] Courier courier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courier);
        }

        // GET: Couriers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Courier == null)
            {
                return NotFound();
            }

            var courier = await _context.Courier.FindAsync(id);
            if (courier == null)
            {
                return NotFound();
            }
            return View(courier);
        }

        // POST: Couriers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourierId,Name,OperatingRange")] Courier courier)
        {
            if (id != courier.CourierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourierExists(courier.CourierId))
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
            return View(courier);
        }

        // GET: Couriers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Courier == null)
            {
                return NotFound();
            }

            var courier = await _context.Courier
                .FirstOrDefaultAsync(m => m.CourierId == id);
            if (courier == null)
            {
                return NotFound();
            }

            return View(courier);
        }

        // POST: Couriers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Courier == null)
            {
                return Problem("Entity set 'PharmaEaseContext.Courier'  is null.");
            }
            var courier = await _context.Courier.FindAsync(id);
            if (courier != null)
            {
                _context.Courier.Remove(courier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourierExists(int id)
        {
          return _context.Courier.Any(e => e.CourierId == id);
        }
    }
}
