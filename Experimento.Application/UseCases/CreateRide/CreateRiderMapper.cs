using AutoMapper;
using Experimento.Domain.Entities;

namespace Experimento.Application.UseCases.CreateRide;

public class CreateRiderMapper : Profile
{
    public CreateRiderMapper()
    {
        CreateMap<CreateRideCommand, Ride>();
        CreateMap<Ride, CreateRideResult>();
    }
}