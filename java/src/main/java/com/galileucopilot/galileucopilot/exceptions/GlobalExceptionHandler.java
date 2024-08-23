package com.galileucopilot.galileucopilot.exceptions;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.ResponseStatus;

@ControllerAdvice
public class GlobalExceptionHandler {

    @ExceptionHandler(UserNotFoundException.class)
    @ResponseStatus(HttpStatus.NOT_FOUND)
    public String handleUserNotFoundException(UserNotFoundException ex) {
        return ex.getMessage();
    }

    @ExceptionHandler(VehicleNotFoundException.class)
    @ResponseStatus(HttpStatus.NOT_FOUND)
    public String handleVehicleNotFoundException(VehicleNotFoundException ex) {
        return ex.getMessage();
    }

    @ExceptionHandler(NoAvailableSeatsException.class)
    @ResponseStatus(HttpStatus.UNPROCESSABLE_ENTITY)
    public String handleNoAvailableSeatsException(NoAvailableSeatsException ex) {
        return ex.getMessage();
    }

    @ExceptionHandler(RideAlreadyScheduledException.class)
    @ResponseStatus(HttpStatus.UNPROCESSABLE_ENTITY)
    public String handleRideAlreadyScheduledException(RideAlreadyScheduledException ex) {
        return ex.getMessage();
    }

}