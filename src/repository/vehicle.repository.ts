import { Repository } from 'typeorm';
import { Injectable } from '@nestjs/common';
import { Vehicle } from 'src/entity/vehicle.entity';

@Injectable()
export class VehicleRepository extends Repository<Vehicle> {
}