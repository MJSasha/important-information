package com.example.backend.controllers;

import com.example.backend.baseClasses.BaseCRUDService;
import com.example.backend.baseClasses.BaseController;
import com.example.backend.data.models.Day;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("api/days")
public class DaysController extends BaseController<Day, Integer> {

    public DaysController(BaseCRUDService<Day, Integer> service) {
        super(service);
    }
}
