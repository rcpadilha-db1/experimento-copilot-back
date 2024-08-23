import {
  Entity,
  PrimaryGeneratedColumn,
  Column,
  ManyToOne,
  OneToMany,
} from 'typeorm';
import { User } from '../user/user.entity';
import { Ride } from '../ride/ride.entity';

@Entity()
export class Vehicle {
  @PrimaryGeneratedColumn()
  id: number;

  @Column()
  plate: string;

  @Column('int')
  capacity: number;

  @Column('int')
  ownerId: number;

  @ManyToOne(() => User, (user) => user.vehicles)
  owner: User;

  @OneToMany(() => Ride, (ride) => ride.vehicle)
  rides: Ride[];
}
