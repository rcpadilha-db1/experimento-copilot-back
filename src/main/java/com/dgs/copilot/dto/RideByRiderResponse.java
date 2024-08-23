package com.dgs.copilot.dto;

import lombok.Builder;
import lombok.Data;

import java.util.Date;

@Data
@Builder
public class RideByRiderResponse {
    private Date date;
    private String plate;
    private String ownerName;
}
