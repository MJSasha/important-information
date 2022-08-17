package com.example.backend.data.models;

import com.fasterxml.jackson.annotation.JsonFormat;
import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

import javax.persistence.*;
import java.util.Date;

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
    private Date dateTimeOfCreate = Date.from(java.time.ZonedDateTime.now().toInstant());
    private String message;
    private String pictures;
    private boolean needToSend;

    // TODO: 10.08.2022 Add author
//    @ManyToOne
//    private User author;
}
