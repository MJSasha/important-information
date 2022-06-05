package com.example.backend.data.models;

import com.example.backend.data.definitions.UserRole;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import org.hibernate.annotations.Type;

import javax.management.relation.Role;
import javax.persistence.*;

@Entity
@Table(name = "users")
@Getter @Setter
public class User {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer id;

    private String name;

    private String login;

    private String password;

    @Enumerated(EnumType.ORDINAL)
    private UserRole role;
}
