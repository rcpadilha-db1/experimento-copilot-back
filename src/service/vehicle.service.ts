import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { CreateVehicleDto } from 'src/dto/create-vehicle.dto';
import { Vehicle } from 'src/entity/vehicle.entity';
import { Repository } from 'typeorm';

@Injectable()
export class VehicleService {
  constructor(
    @InjectRepository(Vehicle)
    private readonly vehicleRepository: Repository<Vehicle>,
  ) {}

  async createVehicle(createVehicleDto: CreateVehicleDto): Promise<Vehicle> {
    const vehicle = this.vehicleRepository.create(createVehicleDto);
    return this.vehicleRepository.save(vehicle);
  }

  async findAll(): Promise<Vehicle[]> {
    return this.vehicleRepository.find();
  }

  async findOne(id: string): Promise<Vehicle> {
    return this.vehicleRepository.findOneBy({ id });
  }

  async remove(id: string): Promise<void> {
    await this.vehicleRepository.delete(id);
  }
}
