package com.example.backend.controllers;

import com.example.backend.data.definitions.UserRole;
import com.example.backend.data.models.Password;
import com.example.backend.data.models.User;
import com.example.backend.services.PasswordsService;
import com.example.backend.services.UsersService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.ArrayList;


// TODO: 6/6/2022 Delete 

@RestController
@RequestMapping("api/start")
public class StartDataGenerateController {

    UsersService usersService;
    PasswordsService passwordsService;

    @Autowired
    public StartDataGenerateController(UsersService usersService, PasswordsService passwordsService) {
        this.usersService = usersService;
        this.passwordsService = passwordsService;
    }

    @GetMapping
    public ResponseEntity<ArrayList<User>> createStartData(){
        ArrayList<Password> passwords = new ArrayList<>();
        passwords.add(new Password("qwerty"));
        passwords.add(new Password("123456"));
        passwords.add(new Password("1q2w3e"));
        passwordsService.create(passwords);

        ArrayList<User> users = new ArrayList<>();
        users.add(new User("Vova", "vova@gmail.com", passwords.get(0), "VovaToken", UserRole.ADMIN));
        users.add(new User("Petya", "petya@gmail.com", passwords.get(1), "PetyaToken", UserRole.USER));
        users.add(new User("Lena", "lena@gmail.com", passwords.get(2), "LenaToken", UserRole.USER));
        usersService.create(users);

        return ResponseEntity.ok(users);
    }
}
