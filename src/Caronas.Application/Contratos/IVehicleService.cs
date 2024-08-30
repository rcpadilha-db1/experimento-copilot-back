using Caronas.Domain;

namespace Caronas.Application.Contratos
{
    public interface IVehicleService
    {
        Task<Vehicle> AddVehicle(Vehicle model);
        Task<Vehicle> UpdateVehicle(string vehicleId, Vehicle model);
        Task<bool> DeleteVehicle(string vehicleId);
        
        Task<Vehicle[]> GetAllVehiclesAsync();
        Task<Vehicle[]> GetAllVehiclesByUserIdAsync(string userId);
        Task<Vehicle> GetVehiclesByIdAsync(string id);
    }
}