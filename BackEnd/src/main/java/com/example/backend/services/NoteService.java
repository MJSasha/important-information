package com.example.backend.services;

import com.example.backend.baseClasses.BaseCRUDService;
import com.example.backend.data.models.Note;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Service;

@Service
public class NoteService extends BaseCRUDService<Note, Integer> {

    public NoteService(CrudRepository<Note, Integer> repository) {
        super(repository);
    }
}
