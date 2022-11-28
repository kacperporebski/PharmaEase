using Microsoft.EntityFrameworkCore;
using PharmaEase.Data;

namespace PharmaEase.Models.Seeders
{
    public static class PharmacySeeder
    {
        public static void Initialize(IServiceProvider serviceProvider, string adminId, string pharmacistId)
        {
            using (var context = new PharmaEaseContext(
                serviceProvider.GetRequiredService<DbContextOptions<PharmaEaseContext>>()))
            {
                // Look for any movies.
                if (context.Pharmacy.Any())
                {
                    return;   // DB has been seeded
                }

                var pharmacy = new Pharmacy
                {
                    Address = "123 Local Drive SW",
                    PhoneNum = "403-222-2222"
                };

                var pharmacist = new Pharmacist
                {
                    ApprovAdminID = adminId,
                    Fname = "Pharma",
                    Lname = "Cist",
                    Pharmacy = pharmacy,
                    UserId = pharmacistId,
                };

                context.Add(pharmacy);
                context.Add(pharmacist);
                context.SaveChanges();
            }
        }

    }

}
