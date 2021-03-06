package com.example.backend.controllers;

import com.example.backend.baseClasses.BaseCRUDService;
import com.example.backend.baseClasses.BaseController;
import com.example.backend.data.models.Lesson;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("api/lessons")
public class LessonsController extends BaseController<Lesson, Integer> {

    public LessonsController(BaseCRUDService<Lesson, Integer> service) {
        super(service);
    }
}
