import { Repository } from 'typeorm';
import { Injectable } from '@nestjs/common';
import { Ride } from 'src/entity/ride.entity';

@Injectable()
export class RideRepository extends Repository<Ride> {
}
