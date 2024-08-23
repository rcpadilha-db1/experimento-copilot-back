import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { UserModule } from './user/user.module';
import { RideModule } from './ride/ride.module';
import { VehicleModule } from './vehicle/vehicle.module';
import { User } from './user/user.entity';
import { Ride } from './ride/ride.entity';
import { Vehicle } from './vehicle/vehicle.entity';

@Module({
  imports: [
    TypeOrmModule.forRoot({
      type: 'mysql',
      host: 'mysql',
      port: 3306,
      username: 'user',
      password: 'password',
      database: 'app',
      entities: [User, Ride, Vehicle],
      synchronize: false,
    }),
    RideModule,
    UserModule,
    VehicleModule,
  ],
})
export class AppModule {}
