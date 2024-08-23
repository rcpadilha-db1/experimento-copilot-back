using Experimento.Application.Services.Interfaces;
using Experimento.Application.UseCases;
using Experimento.Domain.Interfaces;

namespace Experimento.Application.Services;

public class ListRidesByRiderIdService : IListRidesByRiderIdService
{
    private readonly IRideRepository _rideRepository;

    public ListRidesByRiderIdService(IRideRepository rideRepository)
    {
        _rideRepository = rideRepository;
    }
    
    public async Task<List<ListRidesByRiderIdResult>> ValidateRideAsync(string riderId, CancellationToken cancellationToken)
    {
        var rides = await _rideRepository.ListRidesByRiderId(riderId, cancellationToken);
        if (!rides.Any())
        {
            return new List<ListRidesByRiderIdResult>();
        }
        
        var rideDetails = rides.Select(ride => new ListRidesByRiderIdResult
        {
            Date = ride.Date,
            VehiclePlate = ride.Vehicle.Plate,
            VehicleOwnerName = ride.Vehicle.Owner.Name
        }).ToList();

        return rideDetails;
    }
}