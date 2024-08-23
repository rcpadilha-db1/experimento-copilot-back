package com.dgs.copilot.model;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import lombok.Data;
import org.hibernate.annotations.UuidGenerator;

@Data
@Entity
public class User {

    @Id
    @UuidGenerator
    private String id;

    @Column
    private String name;

    @Column
    private String email;
}
