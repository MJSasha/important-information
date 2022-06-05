package com.example.backend.controllers;

import com.example.backend.models.User;
import com.example.backend.services.UsersService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/users")
public class UsersController {

    private final UsersService usersService;

    @Autowired
    public UsersController(UsersService usersService) {
        this.usersService = usersService;
    }

    @PostMapping
    public ResponseEntity create(@RequestBody User user){
        try {
            usersService.create(user);
            return ResponseEntity.ok("Create successful");
        } catch (Exception exception){
            return ResponseEntity.badRequest().body(exception.getMessage());
        }
    }

    @GetMapping("/{id}")
    public ResponseEntity<User> read(@PathVariable Integer id){
        return ResponseEntity.ok(usersService.readById(id));
    }
}
