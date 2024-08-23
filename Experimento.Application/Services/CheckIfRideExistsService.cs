using Experimento.Application.Services.Interfaces;
using Experimento.Domain.Entities;
using Experimento.Domain.Interfaces;
using Experimento.Domain.Notification;

namespace Experimento.Application.Services;

public class CheckIfRideExistsService : ICheckIfRideExistsService
{
    private readonly IRideRepository _rideRepository;
    private readonly NotificationContext _notificationContext;

    public CheckIfRideExistsService(IRideRepository rideRepository, NotificationContext notificationContext)
    {
        _rideRepository = rideRepository;
        _notificationContext = notificationContext;
    }
    public async Task<Ride?> Check(string rideId, CancellationToken cancellationToken)
    {
        var existentRide = await _rideRepository.ListRideById(rideId, cancellationToken);
        if (existentRide == null)
        {
            _notificationContext.AddNotification("Ride not found");
            return null;
        }

        return existentRide;
    }
}