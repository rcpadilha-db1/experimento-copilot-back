import { Controller, Post, Body, Get, Param, Delete } from '@nestjs/common';
import { CreateRideDto } from 'src/dto/create-ride-dto';
import { Ride } from 'src/entity/ride.entity';
import { RideService } from 'src/service/ride.service';

@Controller('rides')
export class RideController {
  constructor(private readonly rideService: RideService) {}

  @Post()
  async createRide(@Body() createRideDto: CreateRideDto): Promise<Ride> {
    return this.rideService.createRide(createRideDto);
  }

  @Get('user/:userId')
  async findAllByUser(@Param('userId') userId: string): Promise<Ride[]> {
    return this.rideService.findAllByUser(userId);
  }

  @Delete(':id')
  async removeRide(@Param('id') id: string): Promise<void> {
    return this.rideService.removeRide(id);
  }
}
