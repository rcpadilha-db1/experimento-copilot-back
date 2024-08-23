package com.dgs.copilot.service;

import com.dgs.copilot.dto.RideDto;
import com.dgs.copilot.dto.UserDto;
import com.dgs.copilot.dto.VehicleDto;
import com.dgs.copilot.exception.RideException;
import com.dgs.copilot.mapper.RideMapper;
import com.dgs.copilot.model.Ride;
import com.dgs.copilot.repository.RideRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.MockitoAnnotations;

import java.util.Date;
import java.util.Optional;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.ArgumentMatchers.anyString;
import static org.mockito.Mockito.when;

class RideServiceTests {

    @InjectMocks
    RideService rideService;

    @Mock
    VehicleService vehicleService;

    @Mock
    UserService userService;

    @Mock
    RideRepository rideRepository;

    @Mock
    RideMapper rideMapper;



    @BeforeEach
    public void setUp() {
        MockitoAnnotations.initMocks(this);
    }


    @Test
    void shouldSaveRideTests() throws RideException {
        RideDto rideDto =  RideDto.builder().date(new Date()).userId("id#1").vehicleId("id#1").build();
        Ride ride =  Ride.builder().date(new Date()).id("1").build();
        VehicleDto vehicleDto = VehicleDto.builder().capacity(2).build();

        when(userService.getUser(anyString())).thenReturn(new UserDto());
        when(vehicleService.getVehicle(anyString())).thenReturn(vehicleDto);
        when(rideRepository.countByVehicleIdAndDate(anyString(), any())).thenReturn(0L);
        when(rideMapper.rideDtoToRide(any(RideDto.class))).thenReturn(ride);
        when(rideMapper.rideToRideDto(any(Ride.class))).thenReturn(rideDto);

        rideService.saveRide(rideDto);

        Mockito.verify(rideRepository, Mockito.times(1)).save(any(Ride.class));
    }

    @Test
    void shouldThrowExceptionForUser() {
        RideDto rideDto =  RideDto.builder().date(new Date()).vehicleId("id#1").build();

        IllegalArgumentException e = assertThrows(IllegalArgumentException.class,
                () -> rideService.saveRide(rideDto)
        );

        assertEquals("User not valid", e.getMessage());
    }

    @Test
    void shouldThrowExceptionForVehicle() {
        RideDto rideDto =  RideDto.builder().date(new Date()).userId("id#1").build();
        when(userService.getUser(anyString())).thenReturn(new UserDto());

        IllegalArgumentException e = assertThrows(IllegalArgumentException.class,
                () -> rideService.saveRide(rideDto)
        );

        assertEquals("Vehicle not valid", e.getMessage());
    }

    @Test
    void shouldThrowExceptionForDate() {
        RideDto rideDto =  RideDto.builder().userId("id#1").vehicleId("id#1").build();
        VehicleDto vehicleDto = VehicleDto.builder().id("id#1").capacity(2).build();

        when(userService.getUser(anyString())).thenReturn(new UserDto());
        when(vehicleService.getVehicle(anyString())).thenReturn(vehicleDto);

        IllegalArgumentException e = assertThrows(IllegalArgumentException.class,
                () -> rideService.saveRide(rideDto)
        );

        assertEquals("Date not valid", e.getMessage());
    }

    @Test
    void shouldThrowExceptionForCarSeat() {
        RideDto rideDto =  RideDto.builder().date(new Date()).userId("id#1").vehicleId("id#1").build();
        VehicleDto vehicleDto = VehicleDto.builder().id("id#1").capacity(2).build();

        when(userService.getUser(anyString())).thenReturn(new UserDto());
        when(vehicleService.getVehicle(anyString())).thenReturn(vehicleDto);
        when(rideRepository.countByVehicleIdAndDate(anyString(), any())).thenReturn(2L);

        RideException e = assertThrows(RideException.class,
                () -> rideService.saveRide(rideDto)
        );

        assertEquals("All seats in this vehicle are taken", e.getMessage());
    }

    @Test
    void shouldThrowExceptionForRider() {
        RideDto rideDto =  RideDto.builder().date(new Date()).userId("id#1").vehicleId("id#1").build();
        VehicleDto vehicleDto = VehicleDto.builder().id("id#1").capacity(2).build();

        when(userService.getUser(anyString())).thenReturn(new UserDto());
        when(vehicleService.getVehicle(anyString())).thenReturn(vehicleDto);
        when(rideRepository.countByVehicleIdAndDate(anyString(), any())).thenReturn(0L);
        when(rideRepository.findByRider_IdAndDate(anyString(), any(Date.class))).thenReturn(Optional.of(Ride.builder().build()));

        RideException e = assertThrows(RideException.class,
                () -> rideService.saveRide(rideDto)
        );

        assertEquals(e.getMessage(), "Rider already has a scheduled ride in this date");
    }
}
