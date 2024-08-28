package com.experimento.copilot.repository;

import com.experimento.copilot.entity.Vehicle;

public interface VehicleRepositoryFacade {

    Vehicle findById(Long vechicleId);
}
