package com.galileucopilot.galileucopilot.repositories;


import com.galileucopilot.galileucopilot.entities.Ride;
import com.galileucopilot.galileucopilot.entities.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Date;
import java.util.List;

@Repository
public interface RideRepository extends JpaRepository<Ride, Long> {
    boolean existsByRiderAndDate(User rider, Date date);
    List<Ride> findByRider(User rider);
}
