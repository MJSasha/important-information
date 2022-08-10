package com.example.backend.data.models;

import com.fasterxml.jackson.annotation.JsonFormat;
import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

import javax.persistence.*;
import java.time.ZonedDateTime;

@Getter
@Setter
@ToString
@Entity
@Table(name = "news")
public class News {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    @JsonFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private ZonedDateTime dateTimeOfCreate = ZonedDateTime.now();
    private String message;
    private String pictures;
    private boolean needToSend;

    // TODO: 10.08.2022 Add author
//    @ManyToOne
//    private User author;
}
