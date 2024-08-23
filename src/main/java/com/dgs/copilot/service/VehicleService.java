package com.dgs.copilot.service;

import com.dgs.copilot.dto.VehicleDto;
import com.dgs.copilot.mapper.VehicleMapper;
import com.dgs.copilot.model.Vehicle;
import com.dgs.copilot.repository.VehicleRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
public class VehicleService {

    @Autowired
    private VehicleRepository vehicleRepository;

    @Autowired
    private VehicleMapper vehicleMapper;

    public VehicleDto saveVehicle(VehicleDto vehicleDto) {
        Vehicle vehicle = vehicleMapper.vehicleDtoToVehicle(vehicleDto);
        vehicle = vehicleRepository.save(vehicle);
        return vehicleMapper.vehicleToVehicleDto(vehicle);
    }

    public VehicleDto getVehicle(String id) {
        Optional<Vehicle> vehicle = vehicleRepository.findById(id);
        return vehicle.map(value -> vehicleMapper.vehicleToVehicleDto(value)).orElse(null);
    }
}
