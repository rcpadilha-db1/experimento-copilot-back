namespace Experimento.Application.Services.Interfaces;

public interface ICheckRideRequirementsService
{
    Task CheckIfAreRequirementsToRide(string rideId, string riderId, string vehicleId,
        CancellationToken cancellationToken);
}