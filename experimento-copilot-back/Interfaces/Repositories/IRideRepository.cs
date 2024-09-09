using experimento_copilot_back.DTOs;
using experimento_copilot_back.Entities;

namespace experimento_copilot_back.Interfaces.Repositories
{
    public interface IRideRepository
    {
        Task AddRideAsync(Ride ride);
        Task<bool> UserExistsAsync(Guid userId);
        Task<bool> VehicleExistsAsync(Guid vehicleId);
        Task<bool> HasRideOnSameDay(Guid userId, DateTime date);
        Task<Ride> GetRideByIdAsync(Guid rideId);
        Task DeleteRideAsync(Ride ride);
        Task<IEnumerable<RideDetailsDto>> GetRidesByUserIdAsync(Guid userId);
    }
}
