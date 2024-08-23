import { Controller, Post, Body, Get, Param, Delete } from '@nestjs/common';
import { CreateVehicleDto } from 'src/dto/create-vehicle.dto';
import { Vehicle } from 'src/entity/vehicle.entity';
import { VehicleService } from 'src/service/vehicle.service';

@Controller('vehicles')
export class VehicleController {
  constructor(private readonly vehicleService: VehicleService) {}

  @Post()
  async createVehicle(@Body() createVehicleDto: CreateVehicleDto): Promise<Vehicle> {
    return this.vehicleService.createVehicle(createVehicleDto);
  }

  @Get()
  async findAll(): Promise<Vehicle[]> {
    return this.vehicleService.findAll();
  }

  @Get(':id')
  async findOne(@Param('id') id: string): Promise<Vehicle> {
    return this.vehicleService.findOne(id);
  }

  @Delete(':id')
  async remove(@Param('id') id: string): Promise<void> {
    return this.vehicleService.remove(id);
  }
}
