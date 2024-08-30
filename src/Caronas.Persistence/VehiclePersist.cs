using Caronas.Domain;
using Caronas.Persistence.Contratos;
using Caronas.Presistence.Contextos;
using Microsoft.EntityFrameworkCore;

namespace Caronas.Persistence
{
    public class VehiclePersist : IVehiclePersist
    {
        private readonly CaronasContext _context;
        public VehiclePersist(CaronasContext context)
        {
            _context = context;
        }
        
        public async Task<Vehicle[]> GetAllVehiclesAsync()
        {
            IQueryable<Vehicle> query = _context.Vehicles
                .Include(v => v.User);

            query = query.AsNoTracking().OrderBy(v => v.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Vehicle> GetVehicleByIdAsync(string id)
        {
            IQueryable<Vehicle> query = _context.Vehicles
                .Include(v => v.User);

            query = query.AsNoTracking().OrderBy(v => v.Id)
                         .Where(v => v.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Vehicle[]> GetAllVehiclesByUserIdAsync(string userId)
        {
            IQueryable<Vehicle> query = _context.Vehicles
                .Include(v => v.User);

            query = query.AsNoTracking().OrderBy(v => v.UserId)
                         .Where(v => v.UserId == userId);

            return await query.ToArrayAsync();
        }
    }
}