package com.example.backend.data.models;

import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import javax.persistence.*;

@Getter
@Setter
@NoArgsConstructor
@Entity
@Table(name = "passwords")
public class Password {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer id;

    private String value;

    // TODO: 6/6/2022 Delete 
    public Password(String value) {
        this.value = value;
    }
}