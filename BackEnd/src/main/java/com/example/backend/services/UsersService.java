package com.example.backend.services;

import com.example.backend.baseClasses.BaseCRUDService;
import com.example.backend.models.User;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Service;

@Service
public class UsersService extends BaseCRUDService<User, Integer> {

    @Autowired
    public UsersService(CrudRepository<User, Integer> repository) {
        super(repository);
    }
}
