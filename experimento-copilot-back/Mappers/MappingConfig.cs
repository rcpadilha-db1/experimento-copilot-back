using experimento_copilot_back.DTOs;
using experimento_copilot_back.Entities;
using Mapster;

namespace experimento_copilot_back.Mappers
{
    public static class MappingConfig
    {
        public static void Mappings()
        {
            TypeAdapterConfig<RideDto, Ride>.NewConfig()
                .Map(dest => dest.Vehicle, src => new Vehicle
                {
                    Id = src.VehicleId,
                    Capacity = src.Vehicle.Capacity,
                    Owner = new User
                    {
                        Id = src.Vehicle.OwnerId
                    },
                    Plate = src.Vehicle.Plate,
                    OwnerId = src.Vehicle.OwnerId
                })
                .Map(dest => dest.Rider, src => new User
                {
                    Name = src.Rider.Name,
                    Email = src.Rider.Email
                });

            TypeAdapterConfig<User, UserDto>.NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Email, src => src.Email);

            TypeAdapterConfig<UserDto, User>.NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Email, src => src.Email);

            TypeAdapterConfig<Vehicle, VehicleDto>.NewConfig()
                .Map(dest => dest.Plate, src => src.Plate)
                .Map(dest => dest.Capacity, src => src.Capacity)
                .Map(dest => dest.OwnerId, src => src.OwnerId);

            TypeAdapterConfig<VehicleDto, Vehicle>.NewConfig()
                .Map(dest => dest.Plate, src => src.Plate)
                .Map(dest => dest.Capacity, src => src.Capacity)
                .Map(dest => dest.OwnerId, src => src.OwnerId);
        }
    }
}
