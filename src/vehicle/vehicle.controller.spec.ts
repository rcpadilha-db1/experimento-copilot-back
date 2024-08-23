import { Test, TestingModule } from '@nestjs/testing';
import { VehicleController } from './vehicle.controller';
import { VehicleService } from './vehicle.service';
import { CreateVehicleDto } from './create-vehicle.dto';
import { BadRequestException } from '@nestjs/common';
import { User } from '../user/user.entity';

describe('VehicleController', () => {
  let controller: VehicleController;
  let service: VehicleService;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [VehicleController],
      providers: [
        {
          provide: VehicleService,
          useValue: {
            createVehicle: jest.fn(),
            deleteVehicle: jest.fn(),
            findVehicleById: jest.fn(),
            findUserById: jest.fn(),
          },
        },
      ],
    }).compile();

    controller = module.get<VehicleController>(VehicleController);
    service = module.get<VehicleService>(VehicleService);
  });

  it('should be defined', () => {
    expect(controller).toBeDefined();
  });

  describe('createVehicle', () => {
    it('should create a vehicle', async () => {
      const createVehicleDto: CreateVehicleDto = {
        plate: 'ABC123',
        capacity: 4,
        ownerId: 1,
      };
      jest.spyOn(service, 'createVehicle').mockResolvedValue({ id: 1 } as any);
      jest.spyOn(service, 'findUserById').mockResolvedValue({ id: 1 } as any);

      const result = await controller.createVehicle(createVehicleDto);
      expect(result).toEqual({ id: 1 });
    });

    it('should throw BadRequestException if vehicle creation fails', async () => {
      const createVehicleDto: CreateVehicleDto = {
        plate: 'ABC123',
        capacity: 4,
        ownerId: 1,
      };
      jest
        .spyOn(service, 'createVehicle')
        .mockRejectedValue(new Error('Vehicle creation failed'));
      jest.spyOn(service, 'findUserById').mockResolvedValue({ id: 1 } as any);

      await expect(controller.createVehicle(createVehicleDto)).rejects.toThrow(
        BadRequestException,
      );
    });
  });

  describe('deleteVehicle', () => {
    it('should delete a vehicle', async () => {
      jest
        .spyOn(service, 'findVehicleById')
        .mockResolvedValue({ id: 1, ownerId: 1 } as any);
      jest.spyOn(service, 'deleteVehicle').mockResolvedValue(undefined);

      await expect(controller.deleteVehicle(1)).resolves.toBeUndefined();
    });

    it('should throw BadRequestException if vehicle not found', async () => {
      jest.spyOn(service, 'findVehicleById').mockResolvedValue(null);

      await expect(controller.deleteVehicle(1)).rejects.toThrow(
        BadRequestException,
      );
    });
  });

  describe('findVehicleById', () => {
    it('should return a vehicle', async () => {
      const user: User = {
        id: 1,
        name: 'John Doe',
        email: 'john.doe@example.com',
        rides: [],
        vehicles: [],
      };
      const vehicle = {
        id: 1,
        plate: 'ABC123',
        capacity: 4,
        ownerId: 1,
        owner: user,
        rides: [],
      };
      jest.spyOn(service, 'findVehicleById').mockResolvedValue(vehicle);

      const result = await controller.findVehicleById(1);
      expect(result).toEqual(vehicle);
    });

    it('should throw BadRequestException if vehicle not found', async () => {
      jest.spyOn(service, 'findVehicleById').mockResolvedValue(null);

      await expect(controller.findVehicleById(1)).rejects.toThrow(
        BadRequestException,
      );
    });
  });
});
