package com.example.backend.services;

import com.example.backend.baseClasses.BaseCRUDService;
import com.example.backend.data.models.News;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Service;

@Service
public class NewsService extends BaseCRUDService<News, Integer> {

    public NewsService(CrudRepository<News, Integer> repository) {
        super(repository);
    }
}
