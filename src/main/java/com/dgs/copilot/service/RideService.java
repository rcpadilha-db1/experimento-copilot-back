package com.dgs.copilot.service;

import com.dgs.copilot.dto.RideByRiderResponse;
import com.dgs.copilot.dto.RideDto;
import com.dgs.copilot.dto.VehicleDto;
import com.dgs.copilot.exception.RideException;
import com.dgs.copilot.mapper.RideMapper;
import com.dgs.copilot.model.Ride;
import com.dgs.copilot.repository.RideRepository;
import io.micrometer.common.util.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.NoSuchElementException;

@Service
public class RideService {

    @Autowired
    private RideRepository rideRepository;

    @Autowired
    private RideMapper rideMapper;

    @Autowired
    private UserService userService;

    @Autowired
    private VehicleService vehicleService;

    public RideDto saveRide(RideDto rideDto) throws RideException, IllegalArgumentException {
        validateRide(rideDto);

        VehicleDto vehicleDto = vehicleService.getVehicle(rideDto.getVehicleId());

        validateCarSeat(rideDto, vehicleDto);
        validateRider(rideDto);

        Ride ride = rideMapper.rideDtoToRide(rideDto);
        ride = rideRepository.save(ride);
        return rideMapper.rideToRideDto(ride);
    }

    private void validateRide(RideDto rideDto) throws IllegalArgumentException {
        if (StringUtils.isBlank(rideDto.getUserId()) || userService.getUser(rideDto.getUserId()) == null) {
            throw new IllegalArgumentException("User not valid");
        }

        if (StringUtils.isBlank(rideDto.getVehicleId()) || vehicleService.getVehicle(rideDto.getVehicleId()) == null) {
            throw new IllegalArgumentException("Vehicle not valid");
        }

        if (rideDto.getDate() == null) {
            throw new IllegalArgumentException("Date not valid");
        }
    }

    private void validateCarSeat(RideDto rideDto, VehicleDto vehicleDto) throws RideException {
        long takenSeat = rideRepository.countByVehicleIdAndDate(vehicleDto.getId(), rideDto.getDate());

        if (takenSeat >= vehicleDto.getCapacity()) {
            throw new RideException("All seats in this vehicle are taken");
        }
    }

    private void validateRider(RideDto rideDto) throws RideException {
        if (rideRepository.findByRider_IdAndDate(rideDto.getUserId(), rideDto.getDate()).isPresent()) {
            throw new RideException("Rider already has a scheduled ride in this date");
        }
    }

    private void validateRideId(String rideId) {
        if (StringUtils.isBlank(rideId)) {
            throw new IllegalArgumentException("Id not valid");
        }

        if (rideRepository.findById(rideId).isEmpty()) {
            throw new NoSuchElementException("Ride not found");
        }
    }

    public void deleteRide(String rideId) throws IllegalArgumentException {
        validateRideId(rideId);
        rideRepository.deleteById(rideId);
    }

    public List<RideByRiderResponse> getRidesByRider(String riderName) {
        if (StringUtils.isBlank(riderName)) {
            throw new IllegalArgumentException("Rider name not valid");
        }

        List<Ride> rides = rideRepository.findByRider_Name(riderName);

        return rides.stream()
                .map(ride -> RideByRiderResponse.builder()
                        .date(ride.getDate())
                        .plate(ride.getVehicle().getPlate())
                        .ownerName(ride.getVehicle().getOwner().getName())
                        .build())
                .toList();
    }
}
