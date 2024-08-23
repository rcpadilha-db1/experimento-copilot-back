using Experimento.Domain.Entities;

namespace Experimento.Domain.Interfaces;

public interface IRideRepository
{
    Task CreateRideAsync(Ride ride, CancellationToken cancellationToken);
    Task <List<Ride>>ListRidesByRiderId(string riderId, CancellationToken cancellationToken);
    Task<Ride?> ListRideById(string rideId, CancellationToken cancellationToken);
    Task<bool> AreRideExistsAsync(string rideId, CancellationToken cancellationToken);
    Task<bool> AreUserExistsAsync(string riderId, CancellationToken cancellationToken);
    Task<bool> AreVehicleExistsAsync(string vehicleId, CancellationToken cancellationToken);
    Task DeleteRide(Ride ride, CancellationToken cancellationToken);
}