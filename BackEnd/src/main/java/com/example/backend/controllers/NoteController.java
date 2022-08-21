package com.example.backend.controllers;

import com.example.backend.baseClasses.BaseCRUDService;
import com.example.backend.baseClasses.BaseController;
import com.example.backend.data.models.Note;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PatchMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("api/notes")
public class NoteController extends BaseController<Note, Integer> {

    public NoteController(BaseCRUDService<Note, Integer> service) {
        super(service);
    }

    @Override
    @PatchMapping("/{id}")
    public ResponseEntity<String> update(Note note, @PathVariable Integer id) {
        note.setId(id);
        return super.update(note, id);
    }

    @Override
    protected void RemoveUnnecessaryLinks(Note note) {
        note.getUser().setNotes(null);
        note.getDay().setCurrentUserNote(null);
    }
}
