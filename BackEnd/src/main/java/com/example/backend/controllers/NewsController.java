package com.example.backend.controllers;

import com.example.backend.baseClasses.BaseController;
import com.example.backend.data.models.News;
import com.example.backend.data.viewModels.StartEndDate;
import com.example.backend.services.NewsService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import javax.servlet.http.HttpServletRequest;
import java.util.List;

@RestController
@RequestMapping("/api/news")
public class NewsController extends BaseController<News, Integer> {

    private final NewsService newsService;

    public NewsController(NewsService newsService) {
        super(newsService);

        this.newsService = newsService;
    }

    @GetMapping("/byDates")
    public ResponseEntity<List<News>> readByDates(@RequestBody StartEndDate startEndDate, HttpServletRequest request) {
        var news = newsService.read().stream().filter(d ->
                d.getDateTimeOfCreate().after(startEndDate.getStart()) && d.getDateTimeOfCreate().before(startEndDate.getEnd())).toList();

        return ResponseEntity.ok(news);
    }

    @Override
    public ResponseEntity<String> update(News news, Integer id) {
        news.setId(id);
        return super.update(news, id);
    }

    @GetMapping("/unsent")
    public ResponseEntity<List<News>> getUnsent() {
        var unsentNews = newsService.read().stream().filter(News::isNeedToSend).toList();
        if (unsentNews == null) return ResponseEntity.noContent().build();
        return ResponseEntity.ok(unsentNews);
    }
}
