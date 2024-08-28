package com.experimento.copilot.repository.impl;

import com.experimento.copilot.entity.Vehicle;
import com.experimento.copilot.exceptions.VehicleNotFoundException;
import com.experimento.copilot.repository.VehicleRepository;
import com.experimento.copilot.repository.VehicleRepositoryFacade;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class VehicleRepositoryFacadeImpl implements VehicleRepositoryFacade {

    private final VehicleRepository vehicleRepository;

    @Override
    public Vehicle findById(Long vehicleId) {
        return vehicleRepository.findById(vehicleId).orElseThrow(() -> new VehicleNotFoundException(String.format("Vehicle %s not found", vehicleId)));
    }
}
