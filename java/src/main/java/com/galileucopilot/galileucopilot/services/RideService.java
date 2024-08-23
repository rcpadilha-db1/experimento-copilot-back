package com.galileucopilot.galileucopilot.services;

import com.galileucopilot.galileucopilot.dto.RidesByUserDto;
import com.galileucopilot.galileucopilot.entities.Ride;
import com.galileucopilot.galileucopilot.entities.User;
import com.galileucopilot.galileucopilot.entities.Vehicle;
import com.galileucopilot.galileucopilot.exceptions.*;
import com.galileucopilot.galileucopilot.repositories.RideRepository;
import com.galileucopilot.galileucopilot.repositories.UserRepository;
import com.galileucopilot.galileucopilot.repositories.VehicleRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Date;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
public class RideService {

    @Autowired
    private RideRepository rideRepository;

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private VehicleRepository vehicleRepository;

    public void createRide(Long userId, Long vehicleId, Date date) throws Exception {
        Optional<User> user = userRepository.findById(userId);
        Optional<Vehicle> vehicle = vehicleRepository.findById(vehicleId);

        if (user.isEmpty()) {
            throw new UserNotFoundException("User not found");
        }

        if (vehicle.isEmpty()) {
            throw new VehicleNotFoundException("Vehicle not found");
        }

        if (vehicle.get().getCapacity() <= 0) {
            throw new NoAvailableSeatsException("Vehicle has no available seats");
        }

        if (rideRepository.existsByRiderAndDate(user.get(), date)) {
            throw new RideAlreadyScheduledException("User already has a ride on this date");
        }

        Ride ride = new Ride();
        ride.setRider(user.get());
        ride.setVehicle(vehicle.get());
        ride.setDate(date);

        rideRepository.save(ride);
    }

    public void deleteRide(Long rideId) throws Exception {
        Optional<Ride> ride = rideRepository.findById(rideId);

        if (ride.isEmpty()) {
            throw new RideNotFoundException("Ride not found");
        }

        rideRepository.delete(ride.get());
    }

    public List<RidesByUserDto> getRidesByUser(Long userId) throws Exception {
        Optional<User> user = userRepository.findById(userId);

        if (user.isEmpty()) {
            throw new UserNotFoundException("User not found");
        }

        List<Ride> rides = rideRepository.findByRider(user.get());

        return rides.stream()
                .map(ride -> {
                    RidesByUserDto dto = new RidesByUserDto();
                    dto.setOwner(ride.getVehicle().getOwner().getName());
                    dto.setPlate(ride.getVehicle().getPlate());
                    dto.setDate(ride.getDate());
                    return dto;
                })
                .collect(Collectors.toList());
    }
}