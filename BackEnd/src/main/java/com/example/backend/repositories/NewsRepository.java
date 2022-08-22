package com.example.backend.repositories;

import com.example.backend.data.models.News;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface NewsRepository extends CrudRepository<News, Integer> {
    News findByLessonId(Integer lessonId);
}
