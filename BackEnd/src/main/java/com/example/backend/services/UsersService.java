package com.example.backend.services;

import com.example.backend.models.User;
import com.example.backend.repositories.UsersRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class UsersService{

    private final UsersRepository usersRepository;

    @Autowired
    public UsersService(UsersRepository usersRepository) {
        this.usersRepository = usersRepository;
    }

    public void create(User user){
        usersRepository.save(user);
    }
    public User readById(Integer id){
        return usersRepository.findById(id).get();
    }
}
