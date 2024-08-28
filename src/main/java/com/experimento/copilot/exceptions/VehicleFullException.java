package com.experimento.copilot.exceptions;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;

@ResponseStatus(value = HttpStatus.UNPROCESSABLE_ENTITY)
public class VehicleFullException extends RuntimeException {
    public VehicleFullException(String message) {
        super(message);
    }
}
