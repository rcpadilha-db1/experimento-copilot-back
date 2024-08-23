using Experimento.Application.Services.Interfaces;
using MediatR;

namespace Experimento.Application.UseCases.ListRidesByRiderId;

public class ListRidesByRiderIdHandler : IRequestHandler<ListRidesByRiderIdQuery, List<ListRidesByRiderIdResult>>
{
    private readonly IListRidesByRiderIdService _service;

    public ListRidesByRiderIdHandler(IListRidesByRiderIdService service)
    {
        _service = service;
    }
    
    public async Task<List<ListRidesByRiderIdResult>> Handle(ListRidesByRiderIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.ValidateRideAsync(request.RiderId, cancellationToken);
    }
}