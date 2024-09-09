using experimento_copilot_back.DTOs;
using experimento_copilot_back.Entities;
using experimento_copilot_back.Interfaces.Repositories;
using experimento_copilot_back.Interfaces.Services;
using Mapster;

namespace experimento_copilot_back.Services
{
    public class RideService : IRideService
    {
        private readonly IRideRepository _rideRepository;

        public RideService(IRideRepository rideRepository)
            => _rideRepository = rideRepository;

        public async Task<bool> AddRideAsync(RideCreateDto rideCreateDto)
        {
            var userExists = await _rideRepository.UserExistsAsync(rideCreateDto.RiderId);
            if (!userExists)
                throw new ArgumentException("Usuário não existe");

            var vehicleExists = await _rideRepository.VehicleExistsAsync(rideCreateDto.VehicleId);
            if (!vehicleExists)
                throw new ArgumentException("Veículo não existe");

            var hasRideOnSameDay = await _rideRepository.HasRideOnSameDay(rideCreateDto.RiderId, rideCreateDto.Date);
            if (hasRideOnSameDay)
                throw new ArgumentException("Usuário já possui uma corrida marcada para o mesmo dia");

            var ride = rideCreateDto.Adapt<Ride>();

            await _rideRepository.AddRideAsync(ride);

            return true;
        }

        public async Task<bool> DeleteRideAsync(Guid rideId)
        {
            var ride = await _rideRepository.GetRideByIdAsync(rideId);
            if (ride == null) throw new ArgumentException("Usuário não existe");

            await _rideRepository.DeleteRideAsync(ride);
            
            return true;
        }

        public async Task<List<RideDetailsDto>> GetRidesByUserId(Guid userId)
        {
            var rides = await _rideRepository.GetRidesByUserIdAsync(userId);
            
            return rides.ToList();
        }
    }
}
