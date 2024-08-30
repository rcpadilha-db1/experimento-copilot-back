using Caronas.Domain;

namespace Caronas.Persistence.Contratos
{
    public interface IVehiclePersist
    {
        Task<Vehicle[]> GetAllVehiclesAsync();
        Task<Vehicle[]> GetAllVehiclesByUserIdAsync(string userId);
        Task<Vehicle> GetVehicleByIdAsync(string id);
    }
}