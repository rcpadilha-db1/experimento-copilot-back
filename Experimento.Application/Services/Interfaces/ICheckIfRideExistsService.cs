using Experimento.Domain.Entities;

namespace Experimento.Application.Services.Interfaces;

public interface ICheckIfRideExistsService
{
    Task<Ride?> Check(string rideId, CancellationToken cancellationToken);
}