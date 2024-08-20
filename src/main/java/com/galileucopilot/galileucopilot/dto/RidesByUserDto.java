package com.galileucopilot.galileucopilot.dto;

import lombok.Data;

import java.util.Date;

@Data
public class RidesByUserDto {

    private String owner;
    private String plate;
    private Date date;
}
