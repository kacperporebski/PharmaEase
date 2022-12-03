using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaEase.Data;
using PharmaEase.Models;
using PharmaEase.Views.Doctors;

namespace PharmaEase.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DoctorsController : Controller
    {
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly PharmaEaseContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public DoctorsController(PharmaEaseContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IUserStore<IdentityUser> userStore)
        {
            _userStore = userStore;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Doctors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Doctor.ToListAsync());
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Doctor == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor
                .FirstOrDefaultAsync(m => m.MedicalLicenseId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FName, LName, MedlicenseNum, Username, Phone, Password, ConfirmPassword")] CreateDoctorModel doctor)
        {
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                await _userStore.SetUserNameAsync(user, doctor.Username, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, doctor.Password);

                if (result.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    await _userManager.AddToRoleAsync(user, "Doctor");

                    await _context.AddAsync(new Doctor
                    {
                        Fname = doctor.FName,
                        Lname = doctor.LName,
                        MedicalLicenseId = doctor.MedLicenseNum,
                        ApprovAdminId = _signInManager.UserManager.GetUserId(User),
                        Phone = doctor.Phone,
                        User = user,
                        UserId = userId,
                    });
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(doctor);
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

     
        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Doctor == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicalLicenseId,Phone,Fname,Lname,ApprovAdminId")] Doctor doctor)
        {
            if (id != doctor.MedicalLicenseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.MedicalLicenseId))
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
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Doctor == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor
                .FirstOrDefaultAsync(m => m.MedicalLicenseId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Doctor == null)
            {
                return Problem("Entity set 'PharmaEaseContext.Doctor'  is null.");
            }
            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor != null)
            {
                foreach (var pres in _context.Prescription.Where(x => x.PrescriberLicenseNum == doctor.MedicalLicenseId))
                {
                    _context.Prescription.Remove(pres);
                    _context.RemoveRange(_context.Delivers.Where(x => x.PrescriptionID == pres.PrescriptionId));
                }

                foreach (var patient in _context.Patient.Where(x => x.Doctor.Equals(doctor)))
                {
                    var userDb = _context.Set<IdentityUser>();
                    var accountToDelete = userDb.Find(patient.UserId);
                    if (accountToDelete == null) throw new Exception("This patient doesnt have an account");
                    userDb.Remove(accountToDelete);
                    _context.Patient.Remove(patient);
                }
                _context.Doctor.Remove(doctor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctor.Any(e => e.MedicalLicenseId == id);
        }
    }
}

