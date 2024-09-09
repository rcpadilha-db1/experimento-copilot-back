using experimento_copilot_back.Entities;

namespace experimento_copilot_back.Interfaces.Repositories
{
    public interface IVehicleRepository
    {
        Task AddVehicleAsync(Vehicle vehicle);
        Task<IList<Vehicle>> GetAllVehiclesAsync();
    }

}
