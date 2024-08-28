package com.experimento.copilot.controller;

import com.experimento.copilot.controller.request.RideRequest;
import com.experimento.copilot.controller.response.UserRidesResponse;
import com.experimento.copilot.usecase.CreateRideUsecase;
import com.experimento.copilot.usecase.DeleteRideUsecase;
import com.experimento.copilot.usecase.ListRideUsecase;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;

@RestController
@RequestMapping("/rides")
@RequiredArgsConstructor
public class RideController {

    private final CreateRideUsecase createRideUsecase;
    private final DeleteRideUsecase deleteRideUsecase;
    private final ListRideUsecase listRideUsecase;

    @PostMapping
    public ResponseEntity<String> createRide(@RequestBody @Valid RideRequest request) {
        try {
            createRideUsecase.execute(request);
            return new ResponseEntity<>("Ride created with success", HttpStatus.CREATED);
        } catch (Exception e) {
            return new ResponseEntity<>(e.getMessage(), HttpStatus.UNPROCESSABLE_ENTITY);
        }
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteRide(@PathVariable Long id) {
        deleteRideUsecase.execute(id);
        return new ResponseEntity<>(HttpStatus.NO_CONTENT);
    }

    @GetMapping("/user/{userId}")
    public ResponseEntity<UserRidesResponse> listRidesByUser(@PathVariable Long userId) {
        UserRidesResponse response = new UserRidesResponse(listRideUsecase.execute(userId));
        return new ResponseEntity<>(response, HttpStatus.OK);
    }
}
