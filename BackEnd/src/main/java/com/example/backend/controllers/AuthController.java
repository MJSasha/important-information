package com.example.backend.controllers;

import com.example.backend.data.exceptions.NotAuthException;
import com.example.backend.data.models.AuthModel;
import com.example.backend.services.AuthService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/auth")
public class AuthController {

    AuthService authService;

    @Autowired
    public AuthController(AuthService authService) {
        this.authService = authService;
    }

    @PostMapping
    public ResponseEntity<String> authenticate(@RequestBody AuthModel authModel){
        try {
            String token = authService.createToken(authModel);
            return ResponseEntity.ok(token);
        } catch (NotAuthException e){
            return ResponseEntity.badRequest().body(e.getMessage());
        }
    }
}
