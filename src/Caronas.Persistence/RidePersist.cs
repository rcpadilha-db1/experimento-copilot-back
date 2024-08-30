using Caronas.Domain;
using Caronas.Persistence.Contratos;
using Caronas.Presistence.Contextos;
using Microsoft.EntityFrameworkCore;

namespace Caronas.Persistence
{
    public class RidePersist : IRidePersist
    {
        private readonly CaronasContext _context;
        public RidePersist(CaronasContext context)
        {
            _context = context;
            
        }
        
        public async Task<Ride[]> GetAllRidesAsync() {
            IQueryable<Ride> query = _context.Rides
                .Include(r => r.User)
                .Include(r => r.Vehicle);

            query = query.AsNoTracking().OrderBy(r => r.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Ride> GetRidesByIdAsync(string id)
        {
            IQueryable<Ride> query = _context.Rides
                .Include(r => r.User)
                .Include(r => r.Vehicle);

            query = query.AsNoTracking().OrderBy(r => r.Id)
                         .Where(r => r.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Ride[]> GetAllRidesByUserIdAsync(string userId)
        {
            IQueryable<Ride> query = _context.Rides
                .Include(r => r.User)
                .Include(r => r.Vehicle);

            query = query.AsNoTracking().OrderBy(r => r.UserId)
                         .Where(r => r.UserId == userId);

            return await query.ToArrayAsync();
        }

        public async Task<Ride[]> GetAllRidesByVehicleIDAsync(string vehicleId)
        {
            IQueryable<Ride> query = _context.Rides
                .Include(r => r.User)
                .Include(r => r.Vehicle);

            query = query.AsNoTracking().OrderBy(r => r.VehicleId)
                         .Where(r => r.VehicleId == vehicleId);

            return await query.ToArrayAsync();
        }

        public async Task<Ride> GetRideByIdAsync(string id)
        {
            IQueryable<Ride> query = _context.Rides;

            query = query.AsNoTracking().OrderBy(r => r.Id)
                         .Where(r => r.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}