import { UserModel } from 'src/models/user.model';
import { Entity, Column, PrimaryGeneratedColumn, OneToMany } from 'typeorm';
import { Vehicle } from './vehicle.entity';
import { Ride } from './ride.entity';

@Entity()
export class User {
  @PrimaryGeneratedColumn()
  id: number;

  @Column({ type: 'varchar', length: 200})
  name: string;

  @Column({ type: 'varchar', length: 200, unique: true })
  email: string;

  @OneToMany(() => Vehicle, (vehicle) => vehicle.owner)
  vehicles: Vehicle[];

  @OneToMany(() => Ride, (ride) => ride.rider)
  rides: Ride[];

  toModel(): UserModel {
    const user = new UserModel();
    user.id = this.id;
    user.name = this.name;
    user.email = this.email;
    return user;
  }
}