package com.dgs.copilot.dto;

import lombok.Builder;
import lombok.Data;

import java.util.Date;

@Data
@Builder
public class RideDto {
    private String id;
    private String vehicleId;
    private String userId;
    private Date date;
}
