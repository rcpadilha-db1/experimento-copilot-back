import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';
import { User } from '../user/user.entity';
import { Vehicle } from '../vehicle/vehicle.entity';

@Entity()
export class Ride {
  @PrimaryGeneratedColumn()
  id: number;

  @Column()
  userId: number;

  @Column()
  vehicleId: number;

  @Column()
  date: string;

  @ManyToOne(() => User, (user) => user.rides)
  user: User;

  @ManyToOne(() => Vehicle, (vehicle) => vehicle.rides)
  vehicle: Vehicle;
}
