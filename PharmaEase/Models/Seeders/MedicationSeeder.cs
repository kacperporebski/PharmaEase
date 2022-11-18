using Microsoft.EntityFrameworkCore;
using PharmaEase.Data;
using PharmaEase.Models;

public static class MedicationSeeder
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new PharmaEaseContext(
            serviceProvider.GetRequiredService<DbContextOptions<PharmaEaseContext>>()))
        {
            // Look for any movies.
            if (context.Medication.Any())
            {
                return;   // DB has been seeded
            }
            context.Medication.AddRange(
                new Medication
                {
                    CommonName = "ChemicalX",
                    DoseNum = 1,
                    Miligrams = 10
                },
                new Medication
                {

                    CommonName = "Xanax",
                    DoseNum = 1,
                    Miligrams = 10
                },
                new Medication
                {
                    CommonName = "Vicodin",
                    DoseNum = 3,
                    Miligrams = 5
                },
                new Medication
                {
                    CommonName = "Allergex",
                    DoseNum = 1,
                    Miligrams = 10
                }
            );
            context.SaveChanges();
        }
    }

}
