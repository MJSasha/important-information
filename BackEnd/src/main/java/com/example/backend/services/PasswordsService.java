package com.example.backend.services;

import com.example.backend.baseClasses.BaseCRUDService;
import com.example.backend.data.models.Password;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Service;

@Service
public class PasswordsService extends BaseCRUDService<Password, Integer> {

    public PasswordsService(CrudRepository<Password, Integer> repository) {
        super(repository);
    }
}
