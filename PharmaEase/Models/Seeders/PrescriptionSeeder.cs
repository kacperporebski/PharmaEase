using Microsoft.EntityFrameworkCore;
using PharmaEase.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace PharmaEase.Models.Seeders
{
    public static class PrescriptionSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
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

                    BuildingNum = 244,
                    City = "Calgary",
                    Fname = "John",
                    Lname = "Doe",
                    Street = "Chapman Circle SE",
                    PhoneNum = "403-444-1234",
                    PostalCode = "T2X 3T7",
                    Province = "Alberta"
                };

                var patient2 = new Patient
                {

                    BuildingNum = 111,
                    City = "Vancouver",
                    Fname = "Jane",
                    Lname = "Watson",
                    Street = "University Drive NE",
                    PhoneNum = "403-777-7878",
                    PostalCode = "Z6Y K7L",
                    Province = "British Columbia"
                };

                var doctor1 = new Doctor
                {
                    ApprovAdminId = 1,
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
                            ApprovAdminId = 1,
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
                            ApprovAdminId = 1,
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
