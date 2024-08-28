package com.experimento.copilot.repository.impl;

import com.experimento.copilot.entity.Ride;
import com.experimento.copilot.repository.RideRepository;
import com.experimento.copilot.repository.RideRepositoryFacade;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class RideRepositoryFacadeImpl implements RideRepositoryFacade {

    private final RideRepository repository;

    @Override
    public void save(Ride ride) {
        repository.save(ride);
    }

    @Override
    public void deleteById(Long rideId) {
        repository.deleteById(rideId);
    }
}
