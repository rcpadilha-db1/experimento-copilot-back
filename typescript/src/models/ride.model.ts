import { Ride } from "src/entitys/ride.entity";
import { UserModel } from "./user.model";
import { VehicleModel } from "./vehicle.model";

export class RideModel {
    id: number = null;
    vehicle: VehicleModel = null;
    rider: UserModel = null;
    date: Date = null;

    constructor(
        id: number = null,
        vehicle: VehicleModel = null,
        rider: UserModel = null,
        date: Date = null,
    ) {
        this.id = id;
        this.vehicle = vehicle;
        this.rider = rider;
        this.date = date;
    }

    toEntity(): Ride {
        const ride = new Ride();
        ride.id = this.id;
        ride.vehicle = this.vehicle.toEntity();
        ride.rider = this.rider.toEntity();
        ride.date = this.date;
        return ride;
    }
}