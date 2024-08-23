import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { RideService } from './ride.service';
import { RideController } from './ride.controller';
import { Ride } from './ride.entity';
import { User } from '../user/user.entity';
import { Vehicle } from '../vehicle/vehicle.entity';

@Module({
  imports: [TypeOrmModule.forFeature([Ride, User, Vehicle])],
  controllers: [RideController],
  providers: [RideService],
})
export class RideModule {}
