package com.dgs.copilot.mapper;

import com.dgs.copilot.dto.VehicleDto;
import com.dgs.copilot.model.Vehicle;
import org.mapstruct.InjectionStrategy;
import org.mapstruct.Mapper;
import org.mapstruct.MappingConstants;
import org.mapstruct.factory.Mappers;

@Mapper(componentModel = MappingConstants.ComponentModel.SPRING, injectionStrategy = InjectionStrategy.FIELD)
public interface VehicleMapper {

    VehicleMapper INSTANCE = Mappers.getMapper(VehicleMapper.class);

    VehicleDto vehicleToVehicleDto(Vehicle vehicle);

    Vehicle vehicleDtoToVehicle(VehicleDto vehicleDto);
}
