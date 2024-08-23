import { Entity, Column, PrimaryGeneratedColumn, ManyToOne } from 'typeorm';
import { Vehicle } from './vehicle.entity';
import { User } from './user.entity';

@Entity()
export class Ride {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @ManyToOne(() => Vehicle)
  vehicle: Vehicle;

  @ManyToOne(() => User)
  rider: User;

  @Column()
  date: Date;
}
