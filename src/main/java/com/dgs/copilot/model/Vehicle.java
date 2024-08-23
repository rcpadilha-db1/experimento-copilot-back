package com.dgs.copilot.model;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import lombok.Data;
import org.hibernate.annotations.UuidGenerator;

@Data
@Entity
public class Vehicle {

    @Id
    @UuidGenerator
    private String id;

    @Column
    private String plate;

    @Column
    private Integer capacity;

    @ManyToOne
    @JoinColumn(name="user_id")
    private User owner;
}
