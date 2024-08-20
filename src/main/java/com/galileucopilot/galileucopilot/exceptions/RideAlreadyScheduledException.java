package com.galileucopilot.galileucopilot.exceptions;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;

@ResponseStatus(value = HttpStatus.UNPROCESSABLE_ENTITY)
public class RideAlreadyScheduledException extends RuntimeException {
    public RideAlreadyScheduledException(String message) {
        super(message);
    }
}