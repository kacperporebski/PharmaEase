using Microsoft.EntityFrameworkCore;
using PharmaEase.Data;

namespace PharmaEase.Models.Seeders
{
    public static class PrescriptionSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider, string adminID)
        {
            using (var context = new PharmaEaseContext(
                serviceProvider.GetRequiredService<DbContextOptions<PharmaEaseContext>>()))
            {
                // Look for any movies.
                if (context.Prescription.Any())
                {
                    return;   // DB has been seeded
                }


                var patient1 = new Patient
                {
                    City = "Calgary",
                    Fname = "John",
                    Lname = "Doe",
                    Street = "244 Chapman Circle SE",
                    PostalCode = "T2X 3T7",
                    Province = "Alberta",
                    GovtHealthNum = "872903520"
                };

                var patient2 = new Patient
                {
                    City = "Vancouver",
                    Fname = "Jane",
                    Lname = "Watson",
                    Street = "12 University Drive NE",
                    PostalCode = "Z6Y K7L",
                    Province = "British Columbia",
                    GovtHealthNum = "12356236"
                };

                var doctor1 = new Doctor
                {
                    ApprovAdminId = adminID,
                    Fname = "Sherlock",
                    Lname = "Holmes",
                    Phone = "560-509-2333"
                };


                context.Prescription.AddRange(
                    new Prescription
                    {
                        Dosage = 2,
                        Patient = patient1,
                        Doctor = doctor1,
                        Quantity = 4,
                        Refills = 2,
                        Medication = new Medication {CommonName = "SuperStrength", DoseNum = 5, Miligrams= 2}
                    },
                    new Prescription
                    {
                        Dosage = 15,
                        Medication = new Medication { CommonName = "Stretchy", DoseNum = 5, Miligrams = 2 },
                        Patient = patient2,
                        Doctor = doctor1,
                        Quantity = 1,
                        Refills = 5
                    },
                    new Prescription
                    {
                        Dosage = 4,
                        Medication = new Medication { CommonName = "Tasty", DoseNum = 5, Miligrams = 2 },
                        Patient = patient1,
                        Doctor = new Doctor
                        {
                            ApprovAdminId = adminID,
                            Fname = "Luke",
                            Lname = "Holmes",
                            Phone = "234-555-5231"
                        },
                        Quantity = 6,
                        Refills = 3
                    },
                    new Prescription
                    {
                        Dosage = 17,
                        Medication = new Medication { CommonName = "DontEatThis", DoseNum = 5, Miligrams = 2 },
                        Patient = patient2,
                        Doctor = new Doctor
                        {
                            ApprovAdminId = adminID,
                            Fname = "Baby",
                            Lname = "Yoda",
                            Phone = "123-234-5667"
                        },
                        Quantity = 25,
                        Refills = 1
                    }
                ) ;
                context.SaveChanges();
            }
        }

    }
}
