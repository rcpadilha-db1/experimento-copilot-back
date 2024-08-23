using AutoMapper;
using Experimento.Domain.Entities;

namespace Experimento.Application.UseCases.DeleteRideById;

public class DeleteRideByIdMapper : Profile
{
    public DeleteRideByIdMapper()
    {
        CreateMap<DeleteRideByIdCommand, Ride>();
        CreateMap<Ride, DeleteRideByIdResult>();
    }
}