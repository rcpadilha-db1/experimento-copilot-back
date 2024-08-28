package com.experimento.copilot.usecase;

import com.experimento.copilot.controller.request.RideRequest;
import com.experimento.copilot.entity.Ride;
import com.experimento.copilot.entity.User;
import com.experimento.copilot.entity.Vehicle;
import com.experimento.copilot.exceptions.UserNotFoundException;
import com.experimento.copilot.exceptions.UserRideException;
import com.experimento.copilot.exceptions.VehicleFullException;
import com.experimento.copilot.exceptions.VehicleNotFoundException;
import com.experimento.copilot.repository.RideRepositoryFacade;
import com.experimento.copilot.repository.UserRepositoryFacade;
import com.experimento.copilot.repository.VehicleRepositoryFacade;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;

import java.time.LocalDateTime;
import java.util.Collections;
import java.util.List;

import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.*;

@ExtendWith(MockitoExtension.class)
class CreateRideUsecaseTest {

    @Mock
    private RideRepositoryFacade rideRepositoryFacade;

    @Mock
    private UserRepositoryFacade userRepositoryFacade;

    @Mock
    private VehicleRepositoryFacade vehicleRepositoryFacade;

    @InjectMocks
    private CreateRideUsecase createRideUsecase;

    private RideRequest rideRequest;
    private User user;
    private Vehicle vehicle;

    @BeforeEach
    void setUp() {
        rideRequest = new RideRequest();
        rideRequest.setUserId(1L);
        rideRequest.setVehicleId(1L);
        rideRequest.setDate(LocalDateTime.now());

        user = new User();
        user.setId(1L);
        user.setRides(Collections.emptyList());

        vehicle = new Vehicle();
        vehicle.setId(1L);
        vehicle.setCapacity(4);
        vehicle.setRides(Collections.emptyList());
    }

    @Test
    void shouldCreateRideSuccessfully() {
        when(userRepositoryFacade.findById(rideRequest.getUserId())).thenReturn(user);
        when(vehicleRepositoryFacade.findById(rideRequest.getVehicleId())).thenReturn(vehicle);

        createRideUsecase.execute(rideRequest);

        verify(rideRepositoryFacade, times(1)).save(any(Ride.class));
    }

    @Test
    void shouldThrowUserNotFoundException() {
        when(userRepositoryFacade.findById(rideRequest.getUserId())).thenThrow(UserNotFoundException.class);

        assertThrows(UserNotFoundException.class, () -> createRideUsecase.execute(rideRequest));
    }

    @Test
    void shouldThrowVehicleNotFoundException() {
        when(userRepositoryFacade.findById(rideRequest.getUserId())).thenReturn(user);
        when(vehicleRepositoryFacade.findById(rideRequest.getVehicleId())).thenThrow(VehicleNotFoundException.class);

        assertThrows(VehicleNotFoundException.class, () -> createRideUsecase.execute(rideRequest));
    }

    @Test
    void shouldThrowUserRideException() {
        Ride existingRide = new Ride();
        existingRide.setDate(rideRequest.getDate());
        user.setRides(List.of(existingRide));

        when(userRepositoryFacade.findById(rideRequest.getUserId())).thenReturn(user);

        assertThrows(UserRideException.class, () -> createRideUsecase.execute(rideRequest));
    }

    @Test
    void shouldThrowVehicleFullException() {
        Ride existingRide = new Ride();
        existingRide.setDate(rideRequest.getDate());
        vehicle.setRides(List.of(existingRide, existingRide, existingRide, existingRide));

        when(userRepositoryFacade.findById(rideRequest.getUserId())).thenReturn(user);
        when(vehicleRepositoryFacade.findById(rideRequest.getVehicleId())).thenReturn(vehicle);

        assertThrows(VehicleFullException.class, () -> createRideUsecase.execute(rideRequest));
    }

}