package com.example.backend.data.exceptions;

import lombok.Getter;

@Getter
public class NotAuthException extends Exception{

    private String message;
    public NotAuthException(String message) {
        super(message);

        this.message = message;
    }
}
