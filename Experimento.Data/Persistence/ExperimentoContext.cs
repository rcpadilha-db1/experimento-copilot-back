using Experimento.Application.Data;
using Experimento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Experimento.Data.Persistence;

public class ExperimentoContext : DbContext, IDatabaseContext
{
    public ExperimentoContext() {
        
    }
    
    public ExperimentoContext(DbContextOptions<ExperimentoContext> options) : base(options)
    {
    }
    
    public DbSet<Ride> Ride { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Vehicle> Vehicle { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=ExperimentoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}