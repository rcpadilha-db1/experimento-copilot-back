package com.experimento.copilot.usecase;

import com.experimento.copilot.repository.RideRepositoryFacade;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;

import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.mockito.Mockito.*;

@ExtendWith(MockitoExtension.class)
class DeleteRideUsecaseTest {

    @Mock
    private RideRepositoryFacade rideRepositoryFacade;

    @InjectMocks
    private DeleteRideUsecase deleteRideUsecase;

    @Test
    void shouldDeleteRideSuccessfully() {
        Long rideId = 123L;

        deleteRideUsecase.execute(rideId);

        verify(rideRepositoryFacade, times(1)).deleteById(rideId);
    }

    @Test
    void shouldHandleRideNotFound() {
        Long rideId = 123L;
        doThrow(new RuntimeException("Ride not found")).when(rideRepositoryFacade).deleteById(rideId);

        assertThrows(RuntimeException.class, () -> deleteRideUsecase.execute(rideId));
    }

}