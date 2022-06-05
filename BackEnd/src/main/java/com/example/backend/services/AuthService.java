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

    public String createToken(AuthModel authModel) throws NotAuthException {
        var user = usersService.readUserByLogin(authModel.getLogin());

        if (Objects.equals(user.getPassword().getValue(), authModel.getPassword())){
            return user.getToken();
        }
        throw new NotAuthException("Такого пользователя не существует");
    }
}
