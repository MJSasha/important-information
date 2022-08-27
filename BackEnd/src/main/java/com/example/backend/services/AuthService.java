package com.example.backend.services;

import com.example.backend.configurations.ApiConfig;
import com.example.backend.data.SubModels.AuthModel;
import com.example.backend.data.exceptions.NotAuthException;
import com.example.backend.helpers.TokenGenerator;
import org.springframework.stereotype.Service;

import java.util.Objects;

@Service
public class AuthService {

    private final UsersService usersService;
    private final ApiConfig apiConfig;

    public AuthService(UsersService usersService, ApiConfig apiConfig) {
        this.usersService = usersService;
        this.apiConfig = apiConfig;
    }

    public String authenticate(AuthModel authModel) throws Exception {
        var user = usersService.readByLogin(authModel.getLogin());

        if (user != null && Objects.equals(user.getPassword().getValue(), authModel.getPassword())) {
            TokenGenerator passwordGenerator = new TokenGenerator.TokenGeneratorBuilder()
                    .useDigits(true)
                    .useLower(true)
                    .useUpper(true)
                    .build();
            String token = passwordGenerator.generate(16);
            user.setToken(token);
            usersService.update(user, user.getId());
            return token;
        }
        throw new NotAuthException("Такого пользователя не существует");
    }

    public void authenticate(String token) throws NotAuthException {
        if (token.equals(apiConfig.getToken())) return;
        if (usersService.readByToken(token) == null) throw new NotAuthException("Токен истек или не верен");
    }
}
