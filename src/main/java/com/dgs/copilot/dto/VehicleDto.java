package com.dgs.copilot.dto;

import lombok.Builder;
import lombok.Data;

@Data
@Builder
public class VehicleDto {
    private String id;
    private String plate;
    private Integer capacity;
    private UserDto owner;
}
