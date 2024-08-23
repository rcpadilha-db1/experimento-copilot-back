import { Body, Controller, forwardRef, Get, Inject, Param, Post } from '@nestjs/common';
import { AppService } from './app.service';
import { UserModel } from './models/user.model';
import { User } from './entitys/user.entity';
import { RideModel } from './models/ride.model';
import { VehicleModel } from './models/vehicle.model';

@Controller()
export class AppController {
  constructor(
    @Inject(forwardRef(() => AppService)) private readonly appService: AppService
  ) { }

  @Get()
  healthCheck(): any {
    return {
      'version': '1.0.0',
      'name': 'Versão Typescript',
      'status': 'OK'
    };
  }

  @Post("/user")
  createUser(@Body() body: UserModel): Promise<User> {
    return this.appService.saveUser(body.toEntity());
  }

  @Get("/user")
  getUsers(): Promise<User[]> {
    return this.appService.findAll();
  }

  @Get("/user/{id}")
  getUser(@Param("id") id: number): Promise<User> {
    return this.appService.findOne(id);
  }

  @Post("/vehicle")
  createVehicle(@Body() body: VehicleModel): any {
    return this.appService.createVehicle(body.toEntity())
  }

  @Post("/ride")
  createRide(@Body() body: RideModel): any {
    return this.appService.createRide(body.toEntity())
  }

  @Get("ride/{userId}")
  getRiders(@Param("userId") userId: number): Promise<RideModel[]> {
    return this.appService.getRides(userId).then((data) => data.map((e) => e.toModel()));
  }
}