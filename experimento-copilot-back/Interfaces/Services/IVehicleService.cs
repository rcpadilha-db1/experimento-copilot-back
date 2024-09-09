using experimento_copilot_back.Entities;

namespace experimento_copilot_back.Interfaces.Services
{
    public interface IVehicleService
    {
        Task CreateVehicleAsync(Vehicle vehicle);
        Task<IList<Vehicle>> GetAllVehiclesAsync();
    }

}
