using Experimento.Application.UseCases;
using Experimento.Data.Persistence;
using Experimento.Domain.Entities;
using Experimento.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Experimento.Data.Repositories;

public class RideRepository : IRideRepository
{
    private readonly ExperimentoContext _context;
    
    public RideRepository(ExperimentoContext context)
    {
        _context = context;
    }
    
    public async Task CreateRideAsync(Ride ride, CancellationToken cancellationToken)
    {
        await _context.Ride.AddAsync(ride, cancellationToken);
    }

    public async Task DeleteRide(Ride ride, CancellationToken cancellationToken)
    {
        _context.Ride.Remove(ride);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Ride>> ListRidesByRiderId(string riderId, CancellationToken cancellationToken)
    {
        return await _context.Ride
            .Include(ride => ride.Vehicle)
            .ThenInclude(vehicle => vehicle.Owner)
            .Where(ride => ride.RiderId == riderId)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Ride?> ListRideById(string rideId, CancellationToken cancellationToken)
    {
        return await _context.Ride
            .Where(ride => ride.Id == rideId)
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<bool> AreRideExistsAsync(string rideId, CancellationToken cancellationToken)
    {
        var rideExists = await _context.Ride.AnyAsync(r => r.Id == rideId, cancellationToken);

        return rideExists;
    }
    
    public async Task<bool> AreUserExistsAsync(string riderId, CancellationToken cancellationToken)
    {
        var userExists = await _context.User.AnyAsync(u => u.Id == riderId, cancellationToken);

        return userExists;
    }
    
    public async Task<bool> AreVehicleExistsAsync(string vehicleId, CancellationToken cancellationToken)
    {
        var vehicleExists = await _context.Vehicle.AnyAsync(v => v.Id == vehicleId, cancellationToken);

        return vehicleExists;
    }
}