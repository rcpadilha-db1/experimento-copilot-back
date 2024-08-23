import { Injectable, NotFoundException } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Vehicle } from './vehicle.entity';
import { User } from '../user/user.entity';
import { CreateVehicleDto } from './create-vehicle.dto';
import { UpdateVehicleDto } from './update-vehicle.dto';

@Injectable()
export class VehicleService {
  constructor(
    @InjectRepository(Vehicle)
    private readonly vehicleRepository: Repository<Vehicle>,
    @InjectRepository(User)
    private readonly userRepository: Repository<User>,
  ) {}

  async createVehicle(createVehicleDto: CreateVehicleDto): Promise<Vehicle> {
    const vehicle = this.vehicleRepository.create(createVehicleDto);
    return this.vehicleRepository.save(vehicle);
  }

  async findAllVehicles(): Promise<Vehicle[]> {
    return this.vehicleRepository.find();
  }

  async findVehicleById(id: number): Promise<Vehicle> {
    return this.vehicleRepository.findOne({ where: { id } });
  }

  async updateVehicle(
    id: number,
    updateVehicleDto: UpdateVehicleDto,
  ): Promise<Vehicle> {
    const vehicle = await this.findVehicleById(id);
    if (!vehicle) {
      throw new NotFoundException('Vehicle not found');
    }
    Object.assign(vehicle, updateVehicleDto);
    return this.vehicleRepository.save(vehicle);
  }

  async deleteVehicle(id: number): Promise<void> {
    const vehicle = await this.findVehicleById(id);
    if (!vehicle) {
      throw new NotFoundException('Vehicle not found');
    }
    await this.vehicleRepository.delete(id);
  }

  async findUserById(id: number): Promise<User> {
    return this.userRepository.findOne({ where: { id } });
  }
}
