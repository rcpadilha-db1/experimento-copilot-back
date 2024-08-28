package com.experimento.copilot.usecase;

import com.experimento.copilot.dto.UserRidesDTO;
import com.experimento.copilot.entity.Ride;
import com.experimento.copilot.entity.User;
import com.experimento.copilot.entity.Vehicle;
import com.experimento.copilot.exceptions.UserNotFoundException;
import com.experimento.copilot.repository.UserRepositoryFacade;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;

import java.time.LocalDateTime;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.when;

@ExtendWith(MockitoExtension.class)
class ListRideUsecaseTest {

    @Mock
    private UserRepositoryFacade userRepositoryFacade;

    @InjectMocks
    private ListRideUsecase listRideUsecase;

    private User user;
    private Vehicle vehicle;
    private Ride ride;

    @BeforeEach
    void setUp() {
        user = new User();
        user.setId(1L);
        user.setName("John Doe");

        vehicle = new Vehicle();
        vehicle.setPlate("ABC123");

        ride = new Ride();
        ride.setDate(LocalDateTime.now());
        ride.setVehicle(vehicle);

        user.setRides(List.of(ride));
    }

    @Test
    void shouldRetrieveUserRidesSuccessfully() {
        when(userRepositoryFacade.findById(1L)).thenReturn(user);

        UserRidesDTO result = listRideUsecase.execute(1L);

        assertEquals("John Doe", result.getNome());
        assertEquals(1, result.getRides().size());
        assertEquals("ABC123", result.getRides().get(0).getVehiclePlate());
    }

    @Test
    void shouldThrowUserNotFoundException() {
        when(userRepositoryFacade.findById(1L)).thenThrow(UserNotFoundException.class);

        assertThrows(UserNotFoundException.class, () -> listRideUsecase.execute(1L));
    }

}