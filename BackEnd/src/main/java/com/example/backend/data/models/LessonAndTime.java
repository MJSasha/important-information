package com.example.backend.data.models;

import com.example.backend.data.definitions.LessonType;
import com.fasterxml.jackson.annotation.JsonFormat;
import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

import javax.persistence.*;
import java.sql.Time;

@Getter
@Setter
@ToString
@Entity
@Table(name = "lesson_time")
public class LessonAndTime {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    @Enumerated(EnumType.ORDINAL)
    private LessonType type;

    @JsonFormat(pattern = "HH:mm:ss")
    private Time time;

    @ManyToOne(cascade = CascadeType.DETACH, fetch = FetchType.EAGER)
    @JoinColumn(name = "lesson_id")
    private Lesson lesson;
}
