using Experimento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Experimento.Application.Data;

public interface IDatabaseContext
{
    DbSet<Ride> Ride { get; set; }
    DbSet<User> User { get; set; }
    DbSet<Vehicle> Vehicle { get; set; }
    
}