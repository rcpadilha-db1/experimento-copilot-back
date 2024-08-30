using Caronas.Domain;

namespace Caronas.Application.Contratos
{
    public interface IRideService
    {
        Task<Ride> AddRide(Ride model);
        Task<Ride> UpdateRide(string rideId, Ride model);
        Task<bool> DeleteRide(string rideId);
        
        Task<Ride[]> GetAllRidesAsync();
        Task<Ride[]> GetAllRidesByUserIdAsync(string userId);
        Task<Ride[]> GetAllRidesByVehicleIDAsync(string vehicleId);
        Task<Ride> GetRideByIdAsync(string id);
    }
}