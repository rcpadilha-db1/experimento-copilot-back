using Experimento.Application.Services.Interfaces;
using Experimento.Domain.Entities;
using Experimento.Domain.Interfaces;
using Experimento.Domain.Notification;

namespace Experimento.Application.Services;

public class CheckRideRequirementsRequirementsService : ICheckRideRequirementsService
{
    private readonly IRideRepository _rideRepository;
    private readonly NotificationContext _notificationContext;

    public CheckRideRequirementsRequirementsService(IRideRepository rideRepository, NotificationContext notificationContext)
    {
        _rideRepository = rideRepository;
        _notificationContext = notificationContext;
    }
    public async Task CheckIfAreRequirementsToRide(string rideId, string riderId, string vehicleId, CancellationToken cancellationToken)
    {
        var existentRide = await _rideRepository.AreRideExistsAsync(rideId, cancellationToken);
        var existentRider = await _rideRepository.AreUserExistsAsync(riderId, cancellationToken);
        var existentVehicle = await _rideRepository.AreVehicleExistsAsync(vehicleId, cancellationToken);
        if (existentRide || !existentRider || !existentVehicle)
        {
            _notificationContext.AddNotification("Invalid Ride requirements");
            return;
        }
    }
}