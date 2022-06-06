package com.example.backend.data.models;

import com.example.backend.data.definitions.UserRole;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;

@Getter
@Setter
@Entity
@Table(name = "users")
public class User {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer id;

    private String name;

    private String login;

    @OneToOne
    @JoinColumn(name = "password_id", referencedColumnName = "id")
    private Password password;

    private String token;

    @Enumerated(EnumType.ORDINAL)
    private UserRole role;
}
