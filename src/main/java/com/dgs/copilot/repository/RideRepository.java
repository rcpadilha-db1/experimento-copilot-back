package com.dgs.copilot.repository;

import com.dgs.copilot.model.Ride;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Date;
import java.util.List;
import java.util.Optional;

@Repository
public interface RideRepository extends JpaRepository<Ride, String> {

    long countByVehicleIdAndDate(String vehicleId, Date date);

    Optional<Ride> findByRider_IdAndDate(String userId, Date date);

    List<Ride> findByRider_Name(String riderName);
}
