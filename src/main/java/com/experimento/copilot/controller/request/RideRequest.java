package com.experimento.copilot.controller.request;


import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.validation.constraints.NotNull;
import java.time.LocalDateTime;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class RideRequest {

    @NotNull(message = "User is required")
    private Long userId;
    @NotNull(message = "Vehicle is required")
    private Long vehicleId;
    @NotNull(message = "Date is required")
    private LocalDateTime date;
}
