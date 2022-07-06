package com.example.backend.data.models;

import com.fasterxml.jackson.annotation.JsonFormat;
import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

import javax.persistence.*;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

@Getter
@Setter
@ToString
@Entity
@Table(name = "lesson_time")
public class LessonAndTime {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer id;

    private String time;

    // TODO: 6/26/2022 one to many
    @OneToOne(cascade = CascadeType.ALL)
    @JoinColumn(name = "lesson_id", referencedColumnName = "id")
    private Lesson lesson;
}
