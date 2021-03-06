package com.example.backend.controllers;

import com.example.backend.baseClasses.BaseCRUDService;
import com.example.backend.baseClasses.BaseController;
import com.example.backend.data.models.Note;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("api/notes")
public class NoteController extends BaseController<Note, Integer> {

    public NoteController(BaseCRUDService<Note, Integer> service) {
        super(service);
    }
}
