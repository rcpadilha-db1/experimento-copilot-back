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
@Entity(name = "USER")
public class User implements Serializable {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "NAME")
    private String name;

    @Column(name = "EMAIL")
    private String email;

    @OneToMany(mappedBy = "rider", fetch = FetchType.LAZY, targetEntity = Ride.class)
    private List<Ride> rides;

    @OneToMany(mappedBy = "owner", fetch = FetchType.LAZY, targetEntity = Vehicle.class)
    private List<Vehicle> vehicles;

}
