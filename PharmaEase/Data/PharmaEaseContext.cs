﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        public DbSet<Prescription> Prescription { get; set; } = default!;
        public DbSet<Medication> Medication { get; set; } = default!;
        public DbSet<Courier> Courier { get; set; }
        public DbSet<PharmaEase.Models.Doctor> Doctor { get; set; }
        public DbSet<PharmaEase.Models.Patient> Patient { get; set; }
        public DbSet<PharmaEase.Models.Pharmacist> Pharmacist { get; set; }
        public DbSet<PharmaEase.Models.Pharmacy> Pharmacy { get; set; }
    }
}