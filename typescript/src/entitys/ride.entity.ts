import { Entity, Column, PrimaryGeneratedColumn, JoinColumn, ManyToOne, PrimaryColumn } from 'typeorm';
import { Vehicle } from './vehicle.entity';
import { User } from './user.entity';
import { RideModel } from 'src/models/ride.model';

@Entity()
export class Ride {
    @PrimaryGeneratedColumn()
    id: number;

    @ManyToOne(() => Vehicle, (e) => e.id, { cascade: true })
    @JoinColumn({ name: "vehicle_id" })
    vehicle: Vehicle;

    @ManyToOne(() => User, (e) => e.id, { cascade: true })
    @JoinColumn({ name: "rider_id" })
    rider: User;

    @Column("date")
    date: Date;

    toModel(): RideModel {
        const ride = new RideModel();
        ride.id = this.id;
        ride.vehicle = this.vehicle.toModel();
        ride.rider = this.rider.toModel();
        ride.date = this.date;
        return
    }
}