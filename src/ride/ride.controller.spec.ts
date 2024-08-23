import { Test, TestingModule } from '@nestjs/testing';
import { RideController } from './ride.controller';
import { RideService } from './ride.service';
import { CreateRideDto } from './create-ride.dto';
import { BadRequestException, NotFoundException } from '@nestjs/common';

describe('RideController', () => {
  let controller: RideController;
  let service: RideService;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [RideController],
      providers: [
        {
          provide: RideService,
          useValue: {
            findUserById: jest.fn(),
            findVehicleById: jest.fn(),
            countRidesByVehicleAndDate: jest.fn(),
            countRidesByUserAndDate: jest.fn(),
            createRide: jest.fn(),
            findRideById: jest.fn(),
            deleteRide: jest.fn(),
            findRidesByUserId: jest.fn(),
          },
        },
      ],
    }).compile();

    controller = module.get<RideController>(RideController);
    service = module.get<RideService>(RideService);
  });

  it('should be defined', () => {
    expect(controller).toBeDefined();
  });

  describe('createRide', () => {
    it('should create a ride', async () => {
      const createRideDto: CreateRideDto = {
        userId: 1,
        vehicleId: 1,
        date: '2024-08-23',
      };
      jest.spyOn(service, 'findUserById').mockResolvedValue({ id: 1 } as any);
      jest
        .spyOn(service, 'findVehicleById')
        .mockResolvedValue({ id: 1, capacity: 4 } as any);
      jest.spyOn(service, 'countRidesByVehicleAndDate').mockResolvedValue(0);
      jest.spyOn(service, 'countRidesByUserAndDate').mockResolvedValue(0);
      jest.spyOn(service, 'createRide').mockResolvedValue({ id: 1 } as any);

      const result = await controller.createRide(createRideDto);
      expect(result).toEqual({ id: 1 });
    });

    it('should throw BadRequestException if vehicle has no available capacity', async () => {
      const createRideDto: CreateRideDto = {
        userId: 1,
        vehicleId: 1,
        date: '2024-08-23',
      };
      jest.spyOn(service, 'findUserById').mockResolvedValue({ id: 1 } as any);
      jest
        .spyOn(service, 'findVehicleById')
        .mockResolvedValue({ id: 1, capacity: 4 } as any);
      jest.spyOn(service, 'countRidesByVehicleAndDate').mockResolvedValue(4);

      await expect(controller.createRide(createRideDto)).rejects.toThrow(
        BadRequestException,
      );
    });

    it('should throw BadRequestException if user already has a ride on the selected day', async () => {
      const createRideDto: CreateRideDto = {
        userId: 1,
        vehicleId: 1,
        date: '2024-08-23',
      };
      jest.spyOn(service, 'findUserById').mockResolvedValue({ id: 1 } as any);
      jest
        .spyOn(service, 'findVehicleById')
        .mockResolvedValue({ id: 1, capacity: 4 } as any);
      jest.spyOn(service, 'countRidesByVehicleAndDate').mockResolvedValue(0);
      jest.spyOn(service, 'countRidesByUserAndDate').mockResolvedValue(1);

      await expect(controller.createRide(createRideDto)).rejects.toThrow(
        BadRequestException,
      );
    });
  });

  describe('deleteRide', () => {
    it('should delete a ride', async () => {
      jest.spyOn(service, 'findRideById').mockResolvedValue({ id: 1 } as any);
      jest.spyOn(service, 'deleteRide').mockResolvedValue(undefined);

      await expect(controller.deleteRide(1)).resolves.toBeUndefined();
    });

    it('should throw BadRequestException if ride not found', async () => {
      jest.spyOn(service, 'findRideById').mockResolvedValue(null);

      await expect(controller.deleteRide(1)).rejects.toThrow(
        BadRequestException,
      );
    });
  });

  describe('findRidesByUserId', () => {
    it('should return rides for a user', async () => {
      const rides = [
        {
          date: '2024-08-23',
          vehiclePlate: 'ABC123',
          vehicleOwnerName: 'John Doe',
        },
      ];
      jest.spyOn(service, 'findUserById').mockResolvedValue({ id: 1 } as any);
      jest.spyOn(service, 'findRidesByUserId').mockResolvedValue(rides);

      const result = await controller.findRidesByUserId(1);
      expect(result).toEqual(rides);
    });

    it('should throw NotFoundException if user not found', async () => {
      jest.spyOn(service, 'findUserById').mockResolvedValue(null);

      await expect(controller.findRidesByUserId(1)).rejects.toThrow(
        NotFoundException,
      );
    });
  });
});
