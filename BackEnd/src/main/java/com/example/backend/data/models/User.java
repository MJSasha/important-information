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
@Table(name = "users")
public class User {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    private String name;
    @Column(unique = true)

    private String login;

    private String token;

    @Column(unique = true, nullable = false)
    private Long chatId;

    @OneToMany(mappedBy = "user", fetch = FetchType.EAGER)
    private List<Note> notes = new ArrayList<>();

    @OneToOne(cascade = CascadeType.ALL)
    @JoinColumn(name = "password_id", referencedColumnName = "id")
    private Password password;

    @Enumerated(EnumType.ORDINAL)
    private UserRole role = UserRole.USER;
}
