package com.dgs.copilot.controller;

import com.dgs.copilot.dto.RideByRiderResponse;
import com.dgs.copilot.dto.RideDto;
import com.dgs.copilot.exception.RideException;
import com.dgs.copilot.service.RideService;
import lombok.extern.apachecommons.CommonsLog;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.util.Date;
import java.util.List;
import java.util.NoSuchElementException;

@RestController
@RequestMapping(value = "/api/ride")
@CommonsLog
public class RideController {

    @Autowired
    RideService rideService;

    @PostMapping
    public ResponseEntity<RideDto> saveRide(@RequestBody RideDto rideDto) {
        try {
            RideDto rideResponse = rideService.saveRide(rideDto);
            return new ResponseEntity<>(rideResponse, HttpStatus.OK);
        } catch (RideException e) {
            log.error(e.getMessage(), e);
            return new ResponseEntity<>(HttpStatus.UNPROCESSABLE_ENTITY);
        } catch (IllegalArgumentException e) {
            log.error(e.getMessage(), e);
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        } catch (Exception e) {
            log.error(e.getMessage(), e);
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @DeleteMapping
    public ResponseEntity deleteRide(@RequestParam String rideId) {
        try {
            rideService.deleteRide(rideId);
            return new ResponseEntity<>(HttpStatus.OK);
        } catch (NoSuchElementException e) {
            log.error(e.getMessage(), e);
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        } catch (IllegalArgumentException e) {
            log.error(e.getMessage(), e);
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        } catch (Exception e) {
            log.error(e.getMessage(), e);
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @GetMapping(value = "/rider")
    public ResponseEntity<List<RideByRiderResponse>> getRidesByRider(@RequestParam String riderName) {
        try {
            List<RideByRiderResponse> rides = rideService.getRidesByRider(riderName);
            return new ResponseEntity<>(rides, HttpStatus.OK);
        } catch (IllegalArgumentException e) {
            log.error(e.getMessage(), e);
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        } catch (Exception e) {
            log.error(e.getMessage(), e);
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
}
