import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { VehicleController } from 'src/controller/vehicle.controller';
import { Vehicle } from 'src/entity/vehicle.entity';
import { VehicleRepository } from 'src/repository/vehicle.repository';
import { VehicleService } from 'src/service/vehicle.service';

@Module({
  imports: [TypeOrmModule.forFeature([Vehicle])],
  controllers: [VehicleController],
  providers: [VehicleService, VehicleRepository],
  exports: [VehicleService,VehicleRepository],
})
export class VehicleModule {}