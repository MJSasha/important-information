package com.example.backend.controllers;

import com.example.backend.baseClasses.BaseController;
import com.example.backend.data.models.News;
import com.example.backend.services.NewsService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("/api/news")
public class NewsController extends BaseController<News, Integer> {

    private final NewsService newsService;

    public NewsController(NewsService newsService) {
        super(newsService);

        this.newsService = newsService;
    }

    @GetMapping("/unsent")
    public ResponseEntity<List<News>> getUnsent() {
        var unsentNews = newsService.read().stream().filter(News::isNeedToSend).toList();
        if (unsentNews == null) return ResponseEntity.noContent().build();
        return ResponseEntity.ok(unsentNews);
    }
}
