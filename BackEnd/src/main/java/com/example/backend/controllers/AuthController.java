package com.example.backend.controllers;

import com.example.backend.data.exceptions.NotAuthException;
import com.example.backend.data.models.AuthModel;
import com.example.backend.services.AuthService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServletResponse;

@RestController
@RequestMapping("/api/auth")
public class AuthController {

    AuthService authService;

    @Autowired
    public AuthController(AuthService authService) {
        this.authService = authService;
    }

    @PostMapping
    public ResponseEntity<String> authenticate(@RequestBody AuthModel authModel, HttpServletResponse response) {
        try {
            String token = authService.authenticate(authModel);

            Cookie cookie = new Cookie("token", token);
            response.addCookie(cookie);

            return ResponseEntity.ok(token);
        } catch (NotAuthException e) {
            return new ResponseEntity<>(e.getMessage(), HttpStatus.UNAUTHORIZED);
        }
    }

    @PostMapping("/byToken")
    public ResponseEntity<String> authenticateByToken(@RequestBody String token, HttpServletResponse response) {
        try {
            authService.authenticate(token);
            Cookie cookie = new Cookie("token", token);
            response.addCookie(cookie);
            return ResponseEntity.ok("Authenticate successful");
        } catch (NotAuthException e) {
            return new ResponseEntity<>(e.getMessage(), HttpStatus.UNAUTHORIZED);
        }
    }
}
