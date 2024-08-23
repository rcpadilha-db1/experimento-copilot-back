package com.dgs.copilot.service;

import com.dgs.copilot.dto.UserDto;
import com.dgs.copilot.mapper.UserMapper;
import com.dgs.copilot.model.User;
import com.dgs.copilot.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
public class UserService {

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private UserMapper userMapper;

    public UserDto saveUser(UserDto userDto) {
        User user = userMapper.userDtoToUser(userDto);
        user = userRepository.save(user);
        return userMapper.userToUserDto(user);
    }

    public UserDto getUser(String id) {
        Optional<User> user = userRepository.findById(id);
        return user.map(value -> userMapper.userToUserDto(value)).orElse(null);
    }
}
