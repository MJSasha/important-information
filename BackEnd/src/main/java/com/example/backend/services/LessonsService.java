package com.example.backend.services;

import com.example.backend.baseClasses.BaseCRUDService;
import com.example.backend.data.models.Lesson;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Service;

@Service
public class LessonsService extends BaseCRUDService<Lesson, Integer> {

    public LessonsService(CrudRepository<Lesson, Integer> repository) {
        super(repository);
    }
}
