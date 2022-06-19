package com.example.backend.data.models;

import com.example.backend.data.definitions.UserRole;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import javax.persistence.*;
import java.util.ArrayList;
import java.util.List;

@Getter
@Setter
@NoArgsConstructor
@Entity
@Table(name = "users", uniqueConstraints = {
        @UniqueConstraint(columnNames = "id"),
        @UniqueConstraint(columnNames = "login")
})
public class User {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer id;

    private String name;
    private String login;
    private String token;
    private Long chatId;

    @OneToMany(mappedBy = "user", fetch = FetchType.EAGER)
    private List<Note> notes = new ArrayList<>();

    @OneToOne(cascade = CascadeType.ALL)
    @JoinColumn(name = "password_id", referencedColumnName = "id")
    private Password password;

    @Enumerated(EnumType.ORDINAL)
    private UserRole role;

    // TODO: 6/6/2022 Delete 
    public User(String name, String login, Password password, UserRole role) {
        this.name = name;
        this.login = login;
        this.password = password;
        this.role = role;
    }
}
