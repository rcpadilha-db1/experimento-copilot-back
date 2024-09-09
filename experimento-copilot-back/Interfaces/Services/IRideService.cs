using experimento_copilot_back.DTOs;

namespace experimento_copilot_back.Interfaces.Services
{
    public interface IRideService
    {
        Task<bool> AddRideAsync(RideCreateDto rideCreateDto);
        Task<bool> DeleteRideAsync(Guid rideId);
        Task<List<RideDetailsDto>> GetRidesByUserId(Guid userId);
    }
}
