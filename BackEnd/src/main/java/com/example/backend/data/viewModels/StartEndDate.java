package com.example.backend.data.viewModels;

import com.fasterxml.jackson.annotation.JsonFormat;
import lombok.Getter;
import lombok.Setter;

import java.util.Date;

@Getter
@Setter
public class StartEndDate {
    @JsonFormat(pattern = "dd-MM-yyyy")
    private Date start;
    @JsonFormat(pattern = "dd-MM-yyyy")
    private Date end;
}
