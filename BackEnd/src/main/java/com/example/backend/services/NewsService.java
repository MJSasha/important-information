package com.example.backend.services;

import com.example.backend.baseClasses.BaseCRUDService;
import com.example.backend.data.models.News;
import com.example.backend.repositories.NewsRepository;
import org.springframework.stereotype.Service;

@Service
public class NewsService extends BaseCRUDService<News, Integer> {

    private final NewsRepository newsRepository;

    public NewsService(NewsRepository newsRepository) {
        super(newsRepository);

        this.newsRepository = newsRepository;
    }

    public News readByLessonId(Integer lessonId) {
        return newsRepository.findByLessonId(lessonId);
    }
}
