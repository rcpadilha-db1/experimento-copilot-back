using Experimento.Domain.Entities;
using MediatR;

namespace Experimento.Application.UseCases.DeleteRideById;

public class DeleteRideByIdCommand : IRequest<DeleteRideByIdResult>
{
    public string RideId { get; set; }
}