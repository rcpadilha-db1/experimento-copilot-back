package com.experimento.copilot.repository;

import com.experimento.copilot.entity.User;

public interface UserRepositoryFacade {

    User findById(Long userId);
}
