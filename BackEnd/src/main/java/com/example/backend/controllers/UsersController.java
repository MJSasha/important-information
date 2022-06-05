package com.example.backend.controllers;

import com.example.backend.baseClasses.BaseController;
import com.example.backend.data.definitions.UserRole;
import com.example.backend.data.models.User;
import com.example.backend.services.UsersService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/users")
@CrossOrigin
public class UsersController extends BaseController<User, Integer> {

    @Autowired
    public UsersController(UsersService usersService) {
        super(usersService);
    }

    @Override
    @PatchMapping("/{id}")
    public ResponseEntity<String> update(@RequestBody User user, @PathVariable Integer id) {
        user.setId(id);
        return super.update(user, id);
    }
}
