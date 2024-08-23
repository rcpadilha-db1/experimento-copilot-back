import { Injectable, NotFoundException, BadRequestException } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { CreateRideDto } from 'src/dto/create-ride-dto';
import { Ride } from 'src/entity/ride.entity';
import { User } from 'src/entity/user.entity';
import { Vehicle } from 'src/entity/vehicle.entity';

@Injectable()
export class RideService {
  constructor(
    @InjectRepository(Ride)
    private readonly rideRepository: Repository<Ride>,
    @InjectRepository(Vehicle)
    private readonly vehicleRepository: Repository<Vehicle>,
    @InjectRepository(User)
    private readonly userRepository: Repository<User>,
  ) {}

  async createRide(createRideDto: CreateRideDto): Promise<Ride> {
    const { vehicleId, riderId, date } = createRideDto;

    const rideDate = new Date(date);

    const vehicle = await this.vehicleRepository.findOneBy({ id: vehicleId });
    const rider = await this.userRepository.findOneBy({ id: riderId });

    if (!vehicle) {
      throw new NotFoundException('Vehicle not found');
    }

    if (!rider) {
      throw new NotFoundException('Rider not found');
    }

    const existingRide = await this.rideRepository.findOne({
      where: {
        rider: { id: riderId },
        date: rideDate,
      },
    });

    if (existingRide) {
      throw new BadRequestException('User already has a ride scheduled for this day');
    }

    const ride = this.rideRepository.create({
      vehicle,
      rider,
      date: rideDate,
    });

    return this.rideRepository.save(ride);
  }

  async findAllByUser(userId: string): Promise<Ride[]> {
    return this.rideRepository.find({
      where: { rider: { id: userId } },
      relations: ['vehicle'],
    });
  }

  async removeRide(id: string): Promise<void> {
    const result = await this.rideRepository.delete(id);

    if (result.affected === 0) {
      throw new NotFoundException('Ride not found');
    }
  }
}
