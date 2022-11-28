using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaEase.Data;
using PharmaEase.Models;

namespace PharmaEase.Controllers
{
    public class DeliversController : Controller
    {

        private readonly PharmaEaseContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;

        public DeliversController(PharmaEaseContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Deliver(int? id)
        {

            ViewData["Couriers"] = new SelectList(_context.Courier.ToList(), "CourierId", "Name");
            return View(new Delivers());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deliver([Bind("CourierID")] Delivers userInput, int? id)
        {
            if (id == null || _context.Prescription == null)
            {
                return NotFound();
            }

            if (userInput.CourierID == 0) return NotFound();

            var prescription = await _context.Prescription
                .Include(p => p.Doctor)
                .Include(p => p.Medication)
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.PrescriptionId == id);

            if (prescription == null)
            {
                return NotFound();
            }

            var userId = _signInManager.UserManager.GetUserId(User);

            var pharmacist = _context.Pharmacist.First(x => x.UserId == userId);

            var pharmacy = _context.Pharmacy.First(x => x.PharmacyId == pharmacist.WorksAt);

            var deliveryModel = new Delivers()
            {
                Prescription = prescription,
                PrescriptionID = prescription.PrescriptionId,
                Patient = prescription.Patient,
                PatientHealthNum = prescription.PatientHealthNum,
                Pharmacy = pharmacy,
                PharmacyID = pharmacy.PharmacyId,
                CourierID = userInput.CourierID
            };

            _context.Add(deliveryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
