using MediatR;

namespace Experimento.Application.UseCases.CreateRide;

public class CreateRideCommand : IRequest<CreateRideResult>
{
    public string Id { get; set; }
    public string RiderId { get; set; }
    public string VehicleId { get; set; }
    public DateTime Date { get; set; }
}