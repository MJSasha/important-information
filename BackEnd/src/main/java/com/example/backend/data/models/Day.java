package com.example.backend.data.models;

import com.fasterxml.jackson.annotation.JsonIgnore;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

@Getter
@Setter
@Entity
@Table(name = "days")
public class Day {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer id;

    private String date;
    private String information;

    @ManyToMany(cascade = CascadeType.ALL, fetch = FetchType.EAGER)
    @JoinTable(name = "days_lessons")
    private List<LessonAndTime> lessonsAndTimes = new ArrayList<>();

    @Transient
    private String currentUserNote;

    @JsonIgnore
    public Date getStringAsDate() throws ParseException {
        return new SimpleDateFormat("dd-MM-yyyy").parse(date);
    }
}