import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { UserModule } from './module/user.module';
import { TypeOrmModule } from '@nestjs/typeorm';
import { User } from './entity/user.entity';
import { Vehicle } from './entity/vehicle.entity';
import { VehicleModule } from './module/vehicle.module';
import { Ride } from './entity/ride.entity';
import { RideModule } from './module/ride.module';

@Module({
  imports: [TypeOrmModule.forRoot({
    type: 'postgres',
    host: 'localhost', 
    port: 5432, 
    username: 'user', 
    password: 'password', 
    database: 'carona_db', 
    entities: [User,Vehicle,Ride], 
    synchronize: true, 
  }),UserModule,VehicleModule,RideModule],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}
