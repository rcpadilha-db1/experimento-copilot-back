import { Entity, Column, PrimaryGeneratedColumn, JoinColumn, ManyToOne, PrimaryColumn } from 'typeorm';
import { User } from './user.entity';
import { VehicleModel } from 'src/models/vehicle.model';

@Entity()
export class Vehicle {
    @PrimaryGeneratedColumn()
    id: number;

    @Column({ type: 'varchar', length: 20, unique: true })
    plate: string;

    @Column({ type: 'int' })
    capacity: number;

    @ManyToOne(() => User, (e) => e.id, { cascade: true })
    @JoinColumn({ name: "owner_id" })
    owner: User;

    toModel(): VehicleModel {
        const vehicle = new VehicleModel();
        vehicle.id = this.id;
        vehicle.plate = this.plate;
        vehicle.capacity = this.capacity;
        vehicle.owner = this.owner.toModel();
        return vehicle
    }
}