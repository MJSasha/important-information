package com.example.backend.services;

import com.example.backend.baseClasses.BaseCRUDService;
import com.example.backend.data.models.Day;
import com.example.backend.repositories.DaysRepository;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Service;

@Service
public class DaysService extends BaseCRUDService<Day, Integer> {

    private final DaysRepository daysRepository;

    public DaysService(DaysRepository daysRepository) {
        super(daysRepository);

        this.daysRepository = daysRepository;
    }

    public Day readByDate(String date) {
        return daysRepository.getByDate(date);
    }
}
