import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { RideController } from 'src/controller/ride.controller';
import { Ride } from 'src/entity/ride.entity';
import { RideRepository } from 'src/repository/ride.repository';
import { RideService } from 'src/service/ride.service';
import { UserModule } from './user.module';
import { VehicleModule } from './vehicle.module';
import { VehicleRepository } from 'src/repository/vehicle.repository';
import { User } from 'src/entity/user.entity';
import { Vehicle } from 'src/entity/vehicle.entity';

@Module({
  imports: [
    TypeOrmModule.forFeature([Ride, Vehicle, User]),
    VehicleModule,
    UserModule,
  ],
  controllers: [RideController],
  providers: [RideService, RideRepository, VehicleRepository],
})
export class RideModule {}
