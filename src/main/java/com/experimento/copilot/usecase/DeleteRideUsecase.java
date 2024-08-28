package com.experimento.copilot.usecase;

import com.experimento.copilot.repository.RideRepositoryFacade;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Component;

@Component
@RequiredArgsConstructor
public class DeleteRideUsecase {

    private final RideRepositoryFacade rideRepositoryFacade;

    public void execute(Long rideId) {
        rideRepositoryFacade.deleteById(rideId);
    }
}
