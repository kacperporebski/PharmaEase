using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmaEase.Models;

namespace PharmaEase.Data
{
    public class PharmaEaseContext : IdentityDbContext
    {
        public PharmaEaseContext(DbContextOptions<PharmaEaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasOne(d => d.Doctor)
                    .WithMany()
                    .HasForeignKey(d => d.PrescriberLicenseNum)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Patient).WithMany().HasForeignKey(p => p.PatientHealthNum).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasOne(d => d.Doctor)
                    .WithMany()
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<Delivers>(entity =>
            {
                entity.HasKey(d => new { d.TimeDelivered, d.PrescriptionID, d.CourierID, d.PharmacyID});
                entity.HasOne(d => d.Prescription)
                    .WithMany()
                    .HasForeignKey(d => d.PrescriptionID)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(d => d.Courier)
                    .WithMany()
                    .HasForeignKey(d => d.CourierID)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(d => d.Pharmacy)
                    .WithMany()
                    .HasForeignKey(d => d.PharmacyID)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(d => d.Patient)
                    .WithMany()
                    .HasForeignKey(d => d.PatientHealthNum)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            base.OnModelCreating(modelBuilder);       
        }


        public DbSet<Prescription> Prescription { get; set; } = default!;
        public DbSet<Medication> Medication { get; set; } = default!;
        public DbSet<Courier> Courier { get; set; }
        public DbSet<PharmaEase.Models.Doctor> Doctor { get; set; }
        public DbSet<PharmaEase.Models.Pharmacist> Pharmacist { get; set; }
        public DbSet<PharmaEase.Models.Pharmacy> Pharmacy { get; set; }
        public DbSet<PharmaEase.Models.Patient> Patient { get; set; }
        public DbSet<PharmaEase.Models.Delivers> Delivers { get; set; }
    }
}