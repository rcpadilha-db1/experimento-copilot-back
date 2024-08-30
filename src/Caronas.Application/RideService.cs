using Caronas.Application.Contratos;
using Caronas.Domain;
using Caronas.Persistence.Contratos;

namespace Caronas.Application
{
    public class RideService : IRideService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IRidePersist _ridePersist;

        public RideService(IGeralPersist geralPersist, IRidePersist ridePersist)
        {
            _geralPersist = geralPersist;
            _ridePersist = ridePersist;   
        }
        
        public async Task<Ride> AddRide(Ride model)
        {
            try
            {
                _geralPersist.Add<Ride>(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _ridePersist.GetRideByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Ride> UpdateRide(string rideId, Ride model)
        {
            try
            {
                var ride = await _ridePersist.GetRideByIdAsync(rideId);
                if (ride == null) return null;

                model.Id = ride.Id;
                _geralPersist.Update(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _ridePersist.GetRideByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteRide(string rideId)
        {
            try
            {
                var ride = await _ridePersist.GetRideByIdAsync(rideId);
                if (ride == null) throw new Exception("Carona para delete não encontrado");

                _geralPersist.Delete<Ride>(ride);
                return await _geralPersist.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Ride[]> GetAllRidesAsync()
        {
            try
            {
                var rides = await _ridePersist.GetAllRidesAsync();
                if (rides == null) return null;

                return rides;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Ride[]> GetAllRidesByUserIdAsync(string userId)
        {
            try
            {
                var rides = await _ridePersist.GetAllRidesByUserIdAsync(userId);
                if (rides == null) return null;

                return rides;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Ride[]> GetAllRidesByVehicleIDAsync(string vehicleId)
        {
            try
            {
                var rides = await _ridePersist.GetAllRidesByVehicleIDAsync(vehicleId);
                if (rides == null) return null;

                return rides;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Ride> GetRideByIdAsync(string id)
        {
            try
            {
                return await _ridePersist.GetRideByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}