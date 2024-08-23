import {
  Controller,
  Post,
  Get,
  Param,
  Body,
  Delete,
  BadRequestException,
  NotFoundException,
  UsePipes,
  ValidationPipe,
} from '@nestjs/common';
import { RideService } from './ride.service';
import { CreateRideDto } from './create-ride.dto';
import { BaseController } from '../common/base.controller';

@Controller('ride')
export class RideController extends BaseController {
  constructor(private readonly rideService: RideService) {
    super();
  }

  @Post()
  @UsePipes(new ValidationPipe({ whitelist: true, forbidNonWhitelisted: true }))
  async createRide(@Body() createRideDto: CreateRideDto) {
    const { userId, vehicleId, date } = createRideDto;

    await this.checkIfExists(
      () => this.rideService.findUserById(userId),
      'Invalid user',
    );
    await this.checkIfExists(
      () => this.rideService.findVehicleById(vehicleId),
      'Invalid vehicle',
    );

    const ridesOnDate = await this.rideService.countRidesByVehicleAndDate(
      vehicleId,
      date,
    );
    if (
      ridesOnDate >=
      (await this.rideService.findVehicleById(vehicleId)).capacity
    ) {
      throw new BadRequestException(
        'Vehicle has no available capacity for the selected day',
      );
    }

    const userRidesOnDate = await this.rideService.countRidesByUserAndDate(
      userId,
      date,
    );
    if (userRidesOnDate > 0) {
      throw new BadRequestException(
        'User already has a ride on the selected day',
      );
    }

    return this.rideService.createRide(createRideDto);
  }

  @Delete(':id')
  async deleteRide(@Param('id') id: number) {
    await this.checkIfExists(
      () => this.rideService.findRideById(id),
      'Ride not found',
    );
    return this.rideService.deleteRide(id);
  }

  @Get('user/:userId')
  async findRidesByUserId(@Param('userId') userId: number) {
    const user = await this.rideService.findUserById(userId);
    if (!user) {
      throw new NotFoundException('User not found');
    }
    return this.rideService.findRidesByUserId(userId);
  }
}
