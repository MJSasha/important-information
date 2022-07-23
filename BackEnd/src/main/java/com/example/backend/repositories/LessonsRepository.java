package com.example.backend.repositories;

import com.example.backend.data.models.Lesson;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface LessonsRepository extends CrudRepository<Lesson, Integer> {
}
