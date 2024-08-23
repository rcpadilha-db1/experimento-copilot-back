import {
  Controller,
  Post,
  Get,
  Param,
  Body,
  Put,
  Delete,
  BadRequestException,
  UsePipes,
  ValidationPipe,
} from '@nestjs/common';
import { VehicleService } from './vehicle.service';
import { CreateVehicleDto } from './create-vehicle.dto';
import { BaseController } from '../common/base.controller';
import { UpdateVehicleDto } from './update-vehicle.dto';

@Controller('vehicle')
export class VehicleController extends BaseController {
  constructor(private readonly vehicleService: VehicleService) {
    super();
  }

  @Post()
  @UsePipes(new ValidationPipe({ whitelist: true, forbidNonWhitelisted: true }))
  async createVehicle(@Body() createVehicleDto: CreateVehicleDto) {
    const { ownerId } = createVehicleDto;
    await this.checkIfExists(
      () => this.vehicleService.findUserById(ownerId),
      'Invalid owner',
    );
    try {
      return await this.vehicleService.createVehicle(createVehicleDto);
    } catch (error) {
      throw new BadRequestException(error);
    }
  }

  @Get()
  async findAllVehicles() {
    return this.vehicleService.findAllVehicles();
  }

  @Get(':id')
  async findVehicleById(@Param('id') id: number) {
    return this.checkIfExists(
      () => this.vehicleService.findVehicleById(id),
      'Vehicle not found',
    );
  }

  @Put(':id')
  @UsePipes(new ValidationPipe({ whitelist: true, forbidNonWhitelisted: true }))
  async updateVehicle(
    @Param('id') id: number,
    @Body() updateVehicleDto: UpdateVehicleDto,
  ) {
    await this.checkIfExists(
      () => this.vehicleService.findVehicleById(id),
      'Vehicle not found',
    );
    return this.vehicleService.updateVehicle(id, updateVehicleDto);
  }

  @Delete(':id')
  async deleteVehicle(@Param('id') id: number) {
    await this.checkIfExists(
      () => this.vehicleService.findVehicleById(id),
      'Vehicle not found',
    );
    return this.vehicleService.deleteVehicle(id);
  }
}
