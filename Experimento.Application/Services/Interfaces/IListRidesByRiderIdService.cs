using Experimento.Application.UseCases;

namespace Experimento.Application.Services.Interfaces;

public interface IListRidesByRiderIdService
{
    Task<List<ListRidesByRiderIdResult>> ValidateRideAsync(string riderId, CancellationToken cancellationToken);
}