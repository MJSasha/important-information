package com.example.backend.data.viewModels;


import lombok.Getter;

@Getter
public class RegistrationModel {

    private String name;
    private String login;
    private String password;

    public AuthModel toAuthModel(){
        return new AuthModel(login, password);
    }
}
