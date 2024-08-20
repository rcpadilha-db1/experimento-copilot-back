package com.galileucopilot.galileucopilot.repositories;

import com.galileucopilot.galileucopilot.entities.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface UserRepository extends JpaRepository<User, Long> {
}
