using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TesteCopilot.Repository.Models;
using Microsoft.Extensions.Configuration; // Add this line

namespace TesteCopilot.Repository.AppContext;


public class AppDatabaseContext : DbContext
{
    public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options) { }   

    public DbSet<User> Users { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Ride> Riders { get; set; }

}

