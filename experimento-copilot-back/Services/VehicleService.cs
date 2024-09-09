using experimento_copilot_back.Entities;
using experimento_copilot_back.Interfaces.Repositories;
using experimento_copilot_back.Interfaces.Services;

namespace experimento_copilot_back.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task CreateVehicleAsync(Vehicle vehicle)
        {
            if (string.IsNullOrWhiteSpace(vehicle.Plate))
                throw new ArgumentException("A placa do veículo é obrigatória.");

            if (vehicle.Capacity <= 0)
                throw new ArgumentException("A capacidade do veículo deve ser maior que zero.");

            await _vehicleRepository.AddVehicleAsync(vehicle);
        }

        public async Task<IList<Vehicle>> GetAllVehiclesAsync()
        {
            return await _vehicleRepository.GetAllVehiclesAsync();
        }
    }

}
