package com.experimento.copilot.usecase;

import com.experimento.copilot.dto.RideDTO;
import com.experimento.copilot.dto.UserRidesDTO;
import com.experimento.copilot.entity.Ride;
import com.experimento.copilot.entity.User;
import com.experimento.copilot.repository.UserRepositoryFacade;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Component;

import java.util.List;

@Component
@RequiredArgsConstructor
public class ListRideUsecase {

    private final UserRepositoryFacade userRepositoryFacade;

    public UserRidesDTO execute(Long userId) {
        User user = userRepositoryFacade.findById(userId);
        List<Ride> rides = user.getRides();

        List<RideDTO> userRides = rides.stream()
                .map(ride -> new RideDTO(ride.getVehicle().getPlate(), ride.getDate()))
                .toList();

        return UserRidesDTO.builder()
                .nome(user.getName())
                .rides(userRides)
                .build();

    }
}
