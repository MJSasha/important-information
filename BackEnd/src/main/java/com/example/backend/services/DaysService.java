package com.example.backend.services;

import com.example.backend.baseClasses.BaseCRUDService;
import com.example.backend.data.models.Day;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Service;

@Service
public class DaysService extends BaseCRUDService<Day, Integer> {

    public DaysService(CrudRepository<Day, Integer> repository) {
        super(repository);
    }
}
