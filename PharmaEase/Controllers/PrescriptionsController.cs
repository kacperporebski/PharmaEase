using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaEase.Data;
using PharmaEase.Models;
using System.Data;

namespace PharmaEase.Controllers
{
    public class PrescriptionsViewModel
    {
        public IEnumerable<Prescription> Prescriptions { get; set; }
        public IEnumerable<Delivers> Deliveries { get; set; }
    }

    public class PrescriptionsController : Controller
    {
        private readonly PharmaEaseContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;

        public PrescriptionsController(PharmaEaseContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        // GET: Prescriptions
        public async Task<IActionResult> Index()
        {
            var userId = _signInManager.UserManager.GetUserId(User);
            IQueryable<Prescription> prescriptions;
            if (User.IsInRole("Admin"))
                prescriptions = _context.Prescription.Include(p => p.Doctor).Include(p => p.Medication).Include(p => p.Patient);
            else if (User.IsInRole("Doctor"))
            {
                prescriptions = _context.Prescription.Include(p => p.Doctor).Include(p => p.Medication).Include(p => p.Patient).Where(p => p.Doctor.UserId == userId);
            }
            else if (User.IsInRole("Pharmacist"))
            {
                prescriptions = _context.Prescription.Include(p => p.Doctor).Include(p => p.Medication).Include(p => p.Patient).Where(p=> p.Refills > 0);
            }
            else
                prescriptions = _context.Prescription.Include(p => p.Doctor).Include(p => p.Medication).Include(p => p.Patient).Where(p => p.Patient.UserId == userId && p.Refills > 0);

            return View(new PrescriptionsViewModel()
            {
                Prescriptions = await prescriptions.ToListAsync(),
                Deliveries = await _context.Delivers.ToListAsync()
            });
        }

        // GET: Prescriptions/Deliver/5
        public async Task<IActionResult> Deliver(int? id)
        {
            //redirect to the delivery controller
            return RedirectToAction("Deliver", "Delivers", new { id = id });
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
            ViewData["PatientHealthNum"] = new SelectList(_context.Set<Patient>().Include(x=>x.Doctor).Where(x=>x.Doctor.UserId == _signInManager.UserManager.GetUserId(User)).ToList(), "GovtHealthNum", "ViewBag");
            return View();
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrescriptionId,Refills,Dosage,Quantity,PatientHealthNum,MedicationCommonName")] Prescription prescription)
        {
            var userId = _signInManager.UserManager.GetUserId(User);
            var doctor = _context.Doctor.First(x => x.UserId == userId);
            if (doctor != null)
            {
                prescription.PrescriberLicenseNum = doctor.MedicalLicenseId;
                prescription.Doctor = doctor;
                prescription.CanRefill = false;
            }
            else
            {
                ViewData["PrescriberLicenseNum"] = new SelectList(_context.Set<Doctor>(), "MedicalLicenseId", "MedicalLicenseId", prescription.PrescriberLicenseNum);
                ViewData["MedicationCommonName"] = new SelectList(_context.Medication, "CommonName", "CommonName", prescription.MedicationCommonName);
                ViewData["PatientHealthNum"] = new SelectList(_context.Set<Patient>().Include(x => x.Doctor).Where(x => x.Doctor.UserId == _signInManager.UserManager.GetUserId(User)).ToList(), "GovtHealthNum", "ViewBag");
                return View(prescription); 
            }

            _context.Add(prescription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
                return Problem("Entity set 'PharmaEaseContext.Prescription' is null.");
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

        // Request Refill functionality. Requests and updates refill quantity
        public async Task<IActionResult> RequestRefill(int? id)
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

            prescription.CanRefill = false;
            _context.Update(prescription);

            await _context.SaveChangesAsync();
            return View(prescription);
        }
    }
}
