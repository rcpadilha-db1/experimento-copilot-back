import { Entity, PrimaryGeneratedColumn, Column, OneToMany } from 'typeorm';
import { Ride } from '../ride/ride.entity';
import { Vehicle } from '../vehicle/vehicle.entity';

@Entity()
export class User {
  @PrimaryGeneratedColumn()
  id: number;

  @Column()
  name: string;

  @Column()
  email: string;

  @OneToMany(() => Ride, (ride) => ride.user)
  rides: Ride[];

  @OneToMany(() => Vehicle, (vehicle) => vehicle.owner)
  vehicles: Vehicle[];
}
