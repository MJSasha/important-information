package com.example.backend.controllers;

import com.example.backend.baseClasses.BaseController;
import com.example.backend.data.models.User;
import com.example.backend.services.UsersService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletRequest;
import java.util.Arrays;
import java.util.Objects;

@RestController
@RequestMapping("/api/users")
public class UsersController extends BaseController<User, Integer> {

    private final UsersService usersService;

    public UsersController(UsersService usersService) {
        super(usersService);

        this.usersService = usersService;
    }


    @Override
    @PatchMapping("/{id}")
    public ResponseEntity<String> update(@RequestBody User user, @PathVariable Integer id) {
        user.setId(id);
        return super.update(user, id);
    }

    @GetMapping("/current")
    public ResponseEntity<User> getCurrentUser(HttpServletRequest request) {
        String token = Arrays.stream(request.getCookies())
                .filter(c -> Objects.equals(c.getName(), "token"))
                .findFirst().get().getValue();

        var user = usersService.readByToken(token);
        if (user == null) return ResponseEntity.noContent().build();

        user.getPassword().setValue(null);
        user.getNotes().forEach(n -> {
            n.setUser(null);
            n.getDay().setLessonsAndTimes(null);
        });

        return ResponseEntity.ok(user);
    }

    @GetMapping("/byChatId/{chatId}")
    public ResponseEntity<User> getByChatId(@PathVariable Long chatId) {
        var user = usersService.readByChatId(chatId);
        if (user == null) return ResponseEntity.noContent().build();
        RemoveUnnecessaryLinks(user);
        return ResponseEntity.ok(user);
    }

    @Override
    protected void RemoveUnnecessaryLinks(User user) {
        user.getNotes().forEach(note -> {
            note.setUser(null);
            note.getDay().setCurrentUserNote(null);
        });
    }
}
