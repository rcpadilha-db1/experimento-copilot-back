import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { User } from './entitys/user.entity';
import { DeleteResult, FindOptionsWhere, Repository } from 'typeorm';
import { Ride } from './entitys/ride.entity';
import { Vehicle } from './entitys/vehicle.entity';

@Injectable()
export class AppService {
  constructor(
    @InjectRepository(User)
    private userRepository: Repository<User>,
    @InjectRepository(Ride)
    private rideRepository: Repository<Ride>,
    @InjectRepository(Vehicle)
    private vehicleRepository: Repository<Vehicle>,
  ) { }

  findAll(): Promise<User[]> {
    return this.userRepository.find();
  }

  findOne(id: number): Promise<User | null> {
    return this.userRepository.findOneBy({ id });
  }

  async saveUser(user: User): Promise<User> {
    // TODO - VALIDAÇÕES
    return this.userRepository.save(user);
  }

  async getRides(userId: number): Promise<Ride[]> {
    const whereConditions: FindOptionsWhere<Ride>[] = [
      { rider: { id: userId } },
    ];

    // TODO - VALIDAÇÕES
    return this.rideRepository.findBy(whereConditions);
  }

  async createRide(ride: Ride): Promise<Ride> {
    // TODO - VALIDAÇÕES
    return this.rideRepository.save(ride);
  }

  async removeRide(rideId: number): Promise<DeleteResult> {
    return this.rideRepository.delete(rideId);
  }

  async createVehicle(vehicle: Vehicle): Promise<Vehicle> {
    // TODO - VALIDAÇÕES
    return this.vehicleRepository.save(vehicle);
  }
}
