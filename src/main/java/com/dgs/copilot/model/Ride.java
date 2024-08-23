package com.dgs.copilot.model;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import lombok.Builder;
import lombok.Data;
import org.hibernate.annotations.UuidGenerator;

import java.util.Date;

@Data
@Entity
@Builder
public class Ride {

    @Id
    @UuidGenerator
    private String id;

    @ManyToOne
    private Vehicle vehicle;

    @ManyToOne
    @JoinColumn(name="user_id")
    private User rider;

    @Column
    private Date date;
}
