package com.experimento.copilot.exceptions;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;

@ResponseStatus(value = HttpStatus.UNPROCESSABLE_ENTITY)
public class UserRideException extends RuntimeException {
    public UserRideException(String message) {
        super(message);
    }
}
