package com.example.backend.controllers;

import com.example.backend.baseClasses.BaseCRUDService;
import com.example.backend.baseClasses.BaseController;
import com.example.backend.data.exceptions.NotAuthException;
import com.example.backend.data.models.User;
import com.example.backend.services.UsersService;
import org.springframework.beans.factory.annotation.Autowired;
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
    public ResponseEntity<User> getCurrentUser(HttpServletRequest request) throws NotAuthException {
        String token = Arrays.stream(request.getCookies())
                .filter(c -> Objects.equals(c.getName(), "token"))
                .findFirst().get().getValue();

        var user = usersService.readByToken(token);
        if (user == null) return ResponseEntity.notFound().build();

        user.getPassword().setValue(null);
        user.getNotes().forEach(n -> {
            n.setUser(null);
            n.getDay().setLessonsAndTimes(null);
        });

        return ResponseEntity.ok(user);
    }

    @GetMapping("/byChatId/{chatId}")
    public ResponseEntity<User> getByChatId(@PathVariable Long chatId) {
        try {
            return ResponseEntity.ok(usersService.readByChatId(chatId));
        } catch (Exception ex) {
            ex.printStackTrace();
            return ResponseEntity.noContent().build();
        }
    }
}
