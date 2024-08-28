package com.experimento.copilot.entity;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.io.Serializable;
import java.util.List;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Entity(name = "VEHICLE")
public class Vehicle implements Serializable {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "PLATE")
    private String plate;

    @Column(name = "CAPACITY")
    private Integer capacity;

    @ManyToOne(targetEntity = User.class, fetch = FetchType.LAZY)
    @JoinColumn(name = "USER_ID", insertable = false, updatable = false)
    private User owner;

    @OneToMany(mappedBy = "vehicle", fetch = FetchType.LAZY, targetEntity = Ride.class)
    private List<Ride> rides;


}
