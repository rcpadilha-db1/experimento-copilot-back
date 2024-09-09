using experimento_copilot_back.Entities;
using experimento_copilot_back.Infra;
using experimento_copilot_back.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace experimento_copilot_back.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext _context;

        public VehicleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Vehicle>> GetAllVehiclesAsync()
        {
            return await _context.Vehicles.ToListAsync();
        }
    }

}
