package com.dgs.copilot.controller;

import com.dgs.copilot.dto.VehicleDto;
import com.dgs.copilot.service.VehicleService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(value = "/api/vehicle")
public class VehicleController {

    @Autowired
    private VehicleService vehicleService;

    @PostMapping
    public VehicleDto saveVehicle(@RequestBody VehicleDto vehicleDto) {
        return vehicleService.saveVehicle(vehicleDto);
    }

}
