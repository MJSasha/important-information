package com.example.backend.services;

import com.example.backend.baseClasses.BaseCRUDService;
import com.example.backend.data.models.User;
import com.example.backend.repositories.UsersRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Service;

@Service
public class UsersService extends BaseCRUDService<User, Integer> {

    UsersRepository repository;

    @Autowired
    public UsersService(CrudRepository<User, Integer> repository) {
        super(repository);

        this.repository = (UsersRepository) repository;
    }

    public User readUserByLogin(String login){
        return repository.findByLogin(login);
    }
}
