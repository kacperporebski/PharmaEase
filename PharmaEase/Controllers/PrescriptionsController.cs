using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaEase.Data;
using PharmaEase.Models;

namespace PharmaEase.Controllers
{
    public class PrescriptionsController : Controller
    {
        private readonly PharmaEaseContext _context;

        public PrescriptionsController(PharmaEaseContext context)
        {
            _context = context;
        }

        // GET: Prescriptions
        public async Task<IActionResult> Index()
        {
            var pharmaEaseContext = _context.Prescription.Include(p => p.Doctor).Include(p => p.Medication).Include(p => p.Patient);
            return View(await pharmaEaseContext.ToListAsync());
        }

        // GET: Prescriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prescription == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescription
                .Include(p => p.Doctor)
                .Include(p => p.Medication)
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.PrescriptionId == id);
            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }

        // GET: Prescriptions/Create
        public IActionResult Create()
        {
            ViewData["PrescriberLicenseNum"] = new SelectList(_context.Set<Doctor>(), "MedicalLicenseId", "MedicalLicenseId");
            ViewData["MedicationCommonName"] = new SelectList(_context.Medication, "CommonName", "CommonName");
            ViewData["PatientHealthNum"] = new SelectList(_context.Set<Patient>().Where(x=>x.GovtHealthNum != "0"), "GovtHealthNum", "GovtHealthNum");
            return View();
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrescriptionId,Refills,Dosage,Quantity,PrescriberLicenseNum,PatientHealthNum,MedicationCommonName")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrescriberLicenseNum"] = new SelectList(_context.Set<Doctor>(), "MedicalLicenseId", "MedicalLicenseId", prescription.PrescriberLicenseNum);
            ViewData["MedicationCommonName"] = new SelectList(_context.Medication, "CommonName", "CommonName", prescription.MedicationCommonName);
            ViewData["PatientHealthNum"] = new SelectList(_context.Set<Patient>(), "GovtHealthNum", "GovtHealthNum", prescription.PatientHealthNum);
            return View(prescription);
        }

        // GET: Prescriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prescription == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescription.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }
            ViewData["PrescriberLicenseNum"] = new SelectList(_context.Set<Doctor>(), "MedicalLicenseId", "FullName", prescription.PrescriberLicenseNum);
            ViewData["MedicationCommonName"] = new SelectList(_context.Medication, "CommonName", "CommonName", prescription.MedicationCommonName);
            ViewData["PatientHealthNum"] = new SelectList(_context.Set<Patient>(), "GovtHealthNum", "FullName", prescription.PatientHealthNum);
            return View(prescription);
        }

        // POST: Prescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrescriptionId,Refills,Dosage,Quantity,PrescriberLicenseNum,PatientHealthNum,MedicationCommonName")] Prescription prescription)
        {
            if (id != prescription.PrescriptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrescriptionExists(prescription.PrescriptionId))
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
            ViewData["PrescriberLicenseNum"] = new SelectList(_context.Set<Doctor>(), "MedicalLicenseId", "MedicalLicenseId", prescription.PrescriberLicenseNum);
            ViewData["MedicationCommonName"] = new SelectList(_context.Medication, "CommonName", "CommonName", prescription.MedicationCommonName);
            ViewData["PatientHealthNum"] = new SelectList(_context.Set<Patient>(), "GovtHealthNum", "GovtHealthNum", prescription.PatientHealthNum);
            return View(prescription);
        }

        // GET: Prescriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prescription == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescription
                .Include(p => p.Doctor)
                .Include(p => p.Medication)
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.PrescriptionId == id);
            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }

        // POST: Prescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prescription == null)
            {
                return Problem("Entity set 'PharmaEaseContext.Prescription'  is null.");
            }
            var prescription = await _context.Prescription.FindAsync(id);
            if (prescription != null)
            {
                _context.Prescription.Remove(prescription);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrescriptionExists(int id)
        {
          return _context.Prescription.Any(e => e.PrescriptionId == id);
        }
    }
}
