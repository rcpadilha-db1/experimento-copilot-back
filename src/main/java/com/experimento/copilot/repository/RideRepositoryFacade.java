package com.experimento.copilot.repository;

import com.experimento.copilot.entity.Ride;

public interface RideRepositoryFacade {

    void save(Ride ride);

    void deleteById(Long rideId);

}
