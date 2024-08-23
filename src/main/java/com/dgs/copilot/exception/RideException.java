package com.dgs.copilot.exception;

import lombok.NoArgsConstructor;

@NoArgsConstructor
public class RideException extends Exception{

    public RideException(String message)
    {
        super(message);
    }
}
