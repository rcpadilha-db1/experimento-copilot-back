using Caronas.Domain;
using Microsoft.EntityFrameworkCore;

namespace Caronas.Presistence.Contextos
{
    public class CaronasContext : DbContext
    {
        public CaronasContext(DbContextOptions<CaronasContext> options) : base(options)
        {
            Rides = Rides ?? throw new ArgumentNullException(nameof(Rides));
            Users = Users ?? throw new ArgumentNullException(nameof(Users));
            Vehicles = Vehicles ?? throw new ArgumentNullException(nameof(Vehicles));
        }

        public DbSet<Ride> Rides { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}