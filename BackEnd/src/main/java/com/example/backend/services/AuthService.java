package com.example.backend.services;

import com.example.backend.data.exceptions.NotAuthException;
import com.example.backend.data.models.AuthModel;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Objects;

@Service
public class AuthService {

    UsersService usersService;

    @Autowired
    public AuthService(UsersService usersService) {
        this.usersService = usersService;
    }

    public String authenticate(AuthModel authModel) throws NotAuthException {
        var user = usersService.readByLogin(authModel.getLogin());

        if (user != null && Objects.equals(user.getPassword().getValue(), authModel.getPassword())) {
            return user.getToken();
        }
        throw new NotAuthException("Такого пользователя не существует");
    }

    public void authenticate(String token) throws NotAuthException {
        if (usersService.readByToken(token) == null) throw new NotAuthException("Токен истек или не верен");
    }
}
