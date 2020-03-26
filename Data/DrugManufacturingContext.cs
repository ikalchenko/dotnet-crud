using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DrugManufacturing.Entities;

namespace DrugManufacturing.Data
{
    public class DrugManufacturingContext : DbContext
    {
        public DrugManufacturingContext(DbContextOptions<DrugManufacturingContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Applicant> Applicants { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            { 
                entity.HasOne(u => u.Applicant)
                    .WithOne(a => a.User)
                    .HasForeignKey<Applicant>(a => a.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(u => u.Manufacturer)
                    .WithOne(m => m.User)
                    .HasForeignKey<Manufacturer>(m => m.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DrugManufacturer>(entity =>
            {
                entity.HasKey(dm => new { dm.DrugId, dm.ManufacturerId });

                entity.HasOne(dm => dm.Drug)
                    .WithMany(d => d.DrugManufacturers)
                    .HasForeignKey(dm => dm.DrugId);

                entity.HasOne(dm => dm.Manufacturer)
                    .WithMany(m => m.DrugManufacturers)
                    .HasForeignKey(dm => dm.ManufacturerId);

            });

            modelBuilder.Entity<Applicant>(entity =>
            {
                entity.HasMany(a => a.Drugs)
                    .WithOne(d => d.Applicant)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

        }
    }
}
