using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SemCopilot.Models;

namespace SemCopilot.Data
{
    public class SemCopilotDbContext : DbContext
    {
        public SemCopilotDbContext(DbContextOptions<SemCopilotDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Ride> Rides { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Vehicles)
                .WithOne(v => v.Owner)
                .HasForeignKey(v => v.OwnerId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Rides)
                .WithOne(r => r.Rider)
                .HasForeignKey(r => r.RiderId);

            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Rides)
                .WithOne(r => r.Vehicle)
                .HasForeignKey(r => r.VehicleId);
        }
    }
}