using MediatR;

namespace Experimento.Application.UseCases;

public class ListRidesByRiderIdQuery : IRequest<List<ListRidesByRiderIdResult>>
{
    public string RiderId { get; set; }
}