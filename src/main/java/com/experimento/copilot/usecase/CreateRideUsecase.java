package com.experimento.copilot.usecase;

import com.experimento.copilot.controller.request.RideRequest;
import com.experimento.copilot.entity.Ride;
import com.experimento.copilot.entity.User;
import com.experimento.copilot.entity.Vehicle;
import com.experimento.copilot.exceptions.UserRideException;
import com.experimento.copilot.exceptions.VehicleFullException;
import com.experimento.copilot.repository.RideRepositoryFacade;
import com.experimento.copilot.repository.UserRepositoryFacade;
import com.experimento.copilot.repository.VehicleRepositoryFacade;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Component;

import java.time.LocalDateTime;
import java.util.List;

@Component
@RequiredArgsConstructor
public class CreateRideUsecase {

    private final RideRepositoryFacade rideRepositoryFacade;
    private final UserRepositoryFacade userRepositoryFacade;
    private final VehicleRepositoryFacade vehicleRepositoryFacade;

    public void execute(RideRequest request) {
        User user = userRepositoryFacade.findById(request.getUserId());
        verifyUserRide(user, request.getDate());

        Vehicle vehicle = vehicleRepositoryFacade.findById(request.getVehicleId());
        verifyVehicleCapacity(vehicle, request.getDate());

        Ride ride = new Ride();
        ride.setRider(user);
        ride.setVehicle(vehicle);
        ride.setDate(request.getDate());
        rideRepositoryFacade.save(ride);
    }

    private void verifyVehicleCapacity(Vehicle vehicle, LocalDateTime date) {
        List<Ride> vehicleRides = vehicle.getRides()
                .stream()
                .filter(ride -> ride.getDate().equals(date))
                .toList();

        if (vehicleRides.size() == vehicle.getCapacity()) {
            throw new VehicleFullException("Vehicle is full");
        }
    }

    private void verifyUserRide(User user, LocalDateTime date) {
        user.getRides()
                .stream()
                .filter(ride -> ride.getDate().equals(date))
                .findAny()
                .ifPresent(ride -> {
                    throw new UserRideException("User already has a ride for this date");
                });
    }
}
