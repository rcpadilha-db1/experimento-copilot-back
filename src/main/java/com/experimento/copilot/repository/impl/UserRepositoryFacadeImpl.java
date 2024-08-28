package com.experimento.copilot.repository.impl;

import com.experimento.copilot.entity.User;
import com.experimento.copilot.exceptions.UserNotFoundException;
import com.experimento.copilot.repository.UserRepository;
import com.experimento.copilot.repository.UserRepositoryFacade;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class UserRepositoryFacadeImpl implements UserRepositoryFacade {

    private final UserRepository userRepository;

    @Override
    public User findById(Long userId) {
        return userRepository.findById(userId).orElseThrow(() -> new UserNotFoundException(String.format("User %s not found", userId)));
    }
}
