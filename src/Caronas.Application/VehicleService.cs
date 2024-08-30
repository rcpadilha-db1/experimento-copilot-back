using Caronas.Application.Contratos;
using Caronas.Domain;
using Caronas.Persistence.Contratos;

namespace Caronas.Application
{
    public class VehicleService : IVehicleService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IVehiclePersist _vehiclePersist;

        public VehicleService(IGeralPersist geralPersist, IVehiclePersist vehiclePersist)
        {
            _geralPersist = geralPersist;
            _vehiclePersist = vehiclePersist;   
        }

        public async Task<Vehicle> AddVehicle(Vehicle model)
        {
            try
            {
                _geralPersist.Add<Vehicle>(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _vehiclePersist.GetVehicleByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Vehicle> UpdateVehicle(string vehicleId, Vehicle model)
        {
            try
            {
                var vehicle = await _vehiclePersist.GetVehicleByIdAsync(vehicleId);
                if (vehicle == null) return null;

                model.Id = vehicle.Id;
                _geralPersist.Update(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _vehiclePersist.GetVehicleByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteVehicle(string vehicleId)
        {
            try
            {
                var vehicle = await _vehiclePersist.GetVehicleByIdAsync(vehicleId);
                if (vehicle == null) throw new Exception("Veículo para delete não encontrado");

                _geralPersist.Delete<Vehicle>(vehicle);
                return await _geralPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Vehicle[]> GetAllVehiclesAsync()
        {
            try
            {
                var vehicles = await _vehiclePersist.GetAllVehiclesAsync();
                if (vehicles == null) return null;

                return vehicles;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Vehicle[]> GetAllVehiclesByUserIdAsync(string userId)
        {
            try
            {
                return await _vehiclePersist.GetAllVehiclesByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Vehicle> GetVehiclesByIdAsync(string id)
        {
            try
            {
                return await _vehiclePersist.GetVehicleByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}