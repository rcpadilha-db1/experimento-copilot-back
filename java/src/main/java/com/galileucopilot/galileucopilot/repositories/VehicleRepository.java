package com.galileucopilot.galileucopilot.repositories;

import com.galileucopilot.galileucopilot.entities.Vehicle;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface VehicleRepository extends JpaRepository<Vehicle, Long> {
}