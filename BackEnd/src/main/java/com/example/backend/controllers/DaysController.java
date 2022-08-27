package com.example.backend.controllers;

import com.example.backend.baseClasses.BaseController;
import com.example.backend.data.SubModels.StartEndDate;
import com.example.backend.data.models.Day;
import com.example.backend.services.DaysService;
import com.example.backend.services.UsersService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletRequest;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Arrays;
import java.util.List;
import java.util.Objects;

@RestController
@RequestMapping("api/days")
public class DaysController extends BaseController<Day, Integer> {

    private final DaysService daysService;
    private final UsersService usersService;

    public DaysController(DaysService daysService, UsersService usersService) {
        super(daysService);

        this.daysService = daysService;
        this.usersService = usersService;
    }

    @Override
    @PatchMapping("/{id}")
    public ResponseEntity<String> update(@RequestBody Day day,@PathVariable Integer id) {
        day.setId(id);
        return super.update(day, id);
    }

    @GetMapping("/byDates")
    public ResponseEntity<List<Day>> readByDates(@RequestBody StartEndDate startEndDate, HttpServletRequest request) {
        var days = daysService.read().stream().filter(d ->
                d.getDate().after(startEndDate.getStart()) && d.getDate().before(startEndDate.getEnd())).toList();

        try {
            String token = Arrays.stream(request.getCookies())
                    .filter(c -> Objects.equals(c.getName(), "token"))
                    .findFirst().get().getValue();

            var currentUser = usersService.readByToken(token);
            if (currentUser == null) return ResponseEntity.ok(days);
            for (var note : currentUser.getNotes()) {
                days.forEach(day -> {
                    if (Objects.equals(note.getDay().getId(), day.getId())) day.setCurrentUserNote(note.getDescription());
                });
            }
        } catch (Exception ignored) {}

        return ResponseEntity.ok(days);
    }

    @GetMapping("/byDate")
    public ResponseEntity<Day> readByDate(@RequestBody String date, HttpServletRequest request) throws ParseException {
        var day = daysService.readByDate(new SimpleDateFormat("yyyy-MM-dd").parse(date));

        if (day == null) return ResponseEntity.noContent().build();

        try {
            String token = Arrays.stream(request.getCookies())
                    .filter(c -> Objects.equals(c.getName(), "token"))
                    .findFirst().get().getValue();

            var currentUser = usersService.readByToken(token);
            if (currentUser == null) return ResponseEntity.ok(day);
            for (var note : currentUser.getNotes()) {
                if (Objects.equals(note.getDay().getId(), day.getId())) day.setCurrentUserNote(note.getDescription());
            }
        } catch (Exception ignored) {}

        return ResponseEntity.ok(day);
    }
}
