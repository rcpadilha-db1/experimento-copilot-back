using Caronas.Domain;

namespace Caronas.Persistence.Contratos
{
    public interface IRidePersist
    {
        Task<Ride[]> GetAllRidesAsync();
        Task<Ride[]> GetAllRidesByUserIdAsync(string userId);
        Task<Ride[]> GetAllRidesByVehicleIDAsync(string vehicleId);
        Task<Ride> GetRideByIdAsync(string id);
    }
}