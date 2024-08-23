import { Vehicle } from "src/entitys/vehicle.entity";
import { UserModel } from "./user.model";

export class VehicleModel {
    id: number = null;
    plate: string = null;
    capacity: number = null;
    owner: UserModel = null;

    constructor(
        id: number = null,
        plate: string = null,
        capacity: number = null,
        owner: UserModel = null,
    ) {
        this.id = id;
        this.plate = plate;
        this.capacity = capacity;
        this.owner = owner;
    }

    toEntity(): Vehicle {
        const vehicle = new Vehicle();
        vehicle.id = this.id;
        vehicle.plate = this.plate;
        vehicle.capacity = this.capacity;
        vehicle.owner = this.owner.toEntity();
        return vehicle;
    }
}