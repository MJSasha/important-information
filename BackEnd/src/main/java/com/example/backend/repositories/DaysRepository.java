package com.example.backend.repositories;

import com.example.backend.data.models.Day;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface DaysRepository extends CrudRepository<Day, Integer> {
    Day getByDate(String date);
}
