package com.galileucopilot.galileucopilot.controllers;

import com.galileucopilot.galileucopilot.dto.RidesByUserDto;
import com.galileucopilot.galileucopilot.services.RideService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.Date;
import java.util.List;

@RestController
@RequestMapping("/rides")
public class RideController {

    @Autowired
    private RideService rideService;

    @PostMapping("/{userId}/{vehicleId}")
    public ResponseEntity<Void> createRide(@PathVariable Long userId, @PathVariable Long vehicleId, @RequestBody Date date) throws Exception {
        rideService.createRide(userId, vehicleId, date);
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @DeleteMapping("/{rideId}")
    public ResponseEntity<Void> deleteRide(@PathVariable Long rideId) throws Exception {
        rideService.deleteRide(rideId);
        return new ResponseEntity<>(HttpStatus.NO_CONTENT);
    }

    @GetMapping("/{userId}")
    public ResponseEntity<List<RidesByUserDto>> getRidesByUser(@PathVariable Long userId) throws Exception {
        return new ResponseEntity<>(rideService.getRidesByUser(userId), HttpStatus.OK);
    }
}