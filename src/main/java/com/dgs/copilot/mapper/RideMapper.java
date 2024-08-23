package com.dgs.copilot.mapper;

import com.dgs.copilot.dto.RideDto;
import com.dgs.copilot.model.Ride;
import org.mapstruct.InjectionStrategy;
import org.mapstruct.Mapper;
import org.mapstruct.Mapping;
import org.mapstruct.MappingConstants;
import org.mapstruct.factory.Mappers;

@Mapper(componentModel = MappingConstants.ComponentModel.SPRING, injectionStrategy = InjectionStrategy.FIELD)
public interface RideMapper {

    RideMapper INSTANCE = Mappers.getMapper(RideMapper.class);

    @Mapping(source = "rider.id", target = "userId")
    @Mapping(source = "vehicle.id", target = "vehicleId")
    RideDto rideToRideDto(Ride ride);

    @Mapping(source = "userId", target = "rider.id")
    @Mapping(source = "vehicleId", target = "vehicle.id")
    Ride rideDtoToRide(RideDto rideDto);
}
