using experimento_copilot_back.DTOs;
using experimento_copilot_back.Entities;
using experimento_copilot_back.Infra;
using experimento_copilot_back.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace experimento_copilot_back.Repositories
{
    public class RideRepository : IRideRepository
    {
        private readonly AppDbContext _context;

        public RideRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRideAsync(Ride ride)
        {
            await _context.Rides.AddAsync(ride);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(Guid userId)
            => await _context.Users.AnyAsync(u => u.Id == userId);

        public async Task<bool> VehicleExistsAsync(Guid vehicleId)
            => await _context.Vehicles.AnyAsync(v => v.Id == vehicleId);

        public async Task<bool> HasRideOnSameDay(Guid userId, DateTime date)
            => await _context.Rides.AnyAsync(r => r.RiderId == userId && r.Date.Date == date.Date);

        public async Task<Ride> GetRideByIdAsync(Guid rideId)
            => await _context.Rides.FindAsync(rideId);

        public async Task DeleteRideAsync(Ride ride)
        {
            _context.Rides.Remove(ride);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RideDetailsDto>> GetRidesByUserIdAsync(Guid userId)
        {
            return await _context.Rides
                .Where(r => r.RiderId == userId)
                .Include(r => r.Vehicle)
                    .ThenInclude(v => v.Owner)
                .Select(r => new RideDetailsDto
                {
                    Date = r.Date,
                    VehiclePlate = r.Vehicle.Plate,
                    VehicleOwnerName = r.Vehicle.Owner.Name
                })
                .ToListAsync();
        }
    }
}
