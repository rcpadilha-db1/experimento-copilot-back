import { Injectable, NotFoundException } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Ride } from './ride.entity';
import { User } from '../user/user.entity';
import { Vehicle } from '../vehicle/vehicle.entity';
import { CreateRideDto } from './create-ride.dto';

@Injectable()
export class RideService {
  constructor(
    @InjectRepository(Ride) private readonly rideRepository: Repository<Ride>,
    @InjectRepository(User) private readonly userRepository: Repository<User>,
    @InjectRepository(Vehicle)
    private readonly vehicleRepository: Repository<Vehicle>,
  ) {}

  async findRideById(id: number): Promise<Ride | null> {
    return this.rideRepository.findOne({ where: { id } });
  }

  async findUserById(id: number): Promise<User> {
    return this.userRepository.findOne({ where: { id } });
  }

  async findVehicleById(id: number): Promise<Vehicle> {
    return this.vehicleRepository.findOne({ where: { id } });
  }

  async countRidesByVehicleAndDate(
    vehicleId: number,
    date: string,
  ): Promise<number> {
    return this.rideRepository.count({ where: { vehicleId, date } });
  }

  async countRidesByUserAndDate(userId: number, date: string): Promise<number> {
    return this.rideRepository.count({ where: { userId, date } });
  }

  async createRide(createRideDto: CreateRideDto): Promise<Ride> {
    const ride = this.rideRepository.create(createRideDto);
    return this.rideRepository.save(ride);
  }

  async deleteRide(id: number): Promise<void> {
    const ride = await this.findRideById(id);
    if (!ride) {
      throw new NotFoundException('Ride not found');
    }
    await this.rideRepository.delete(id);
  }

  async findRidesByUserId(userId: number): Promise<any[]> {
    const rides = await this.rideRepository.find({
      where: { user: { id: userId } },
      relations: ['vehicle'],
    });

    const rideDetails = await Promise.all(
      rides.map(async (ride) => {
        const vehicle = ride.vehicle;
        if (!vehicle) {
          return {
            date: ride.date,
            vehiclePlate: 'N/A',
            vehicleOwnerName: 'N/A',
          };
        }

        const owner = await this.userRepository.findOne({
          where: { id: vehicle.owner?.id },
        });

        return {
          date: ride.date,
          vehiclePlate: vehicle.plate,
          vehicleOwnerName: owner ? owner.name : 'N/A',
        };
      }),
    );

    return rideDetails;
  }
}
