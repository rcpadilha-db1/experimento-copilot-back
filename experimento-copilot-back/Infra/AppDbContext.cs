using experimento_copilot_back.Entities;
using Microsoft.EntityFrameworkCore;

namespace experimento_copilot_back.Infra
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Ride> Rides { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Owner)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(v => v.OwnerId);

            modelBuilder.Entity<Ride>()
                .HasOne(r => r.Vehicle)
                .WithMany()
                .HasForeignKey(r => r.VehicleId);

            modelBuilder.Entity<Ride>()
                .HasOne(r => r.Rider)
                .WithMany()
                .HasForeignKey(r => r.RiderId);
        }
    }
}
