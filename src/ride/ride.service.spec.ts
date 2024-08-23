import { Test, TestingModule } from '@nestjs/testing';
import { RideService } from './ride.service';
import { getRepositoryToken } from '@nestjs/typeorm';
import { Ride } from './ride.entity';
import { User } from '../user/user.entity';
import { Vehicle } from '../vehicle/vehicle.entity';
import { Repository } from 'typeorm';
import { NotFoundException } from '@nestjs/common';

describe('RideService', () => {
  let service: RideService;
  let rideRepository: Repository<Ride>;
  let userRepository: Repository<User>;
  let vehicleRepository: Repository<Vehicle>;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [
        RideService,
        {
          provide: getRepositoryToken(Ride),
          useValue: {
            create: jest.fn(),
            save: jest.fn(),
            findOne: jest.fn(),
            find: jest.fn(),
            delete: jest.fn(),
          },
        },
        {
          provide: getRepositoryToken(User),
          useValue: {
            findOne: jest.fn(),
          },
        },
        {
          provide: getRepositoryToken(Vehicle),
          useValue: {
            findOne: jest.fn(),
          },
        },
      ],
    }).compile();

    service = module.get<RideService>(RideService);
    rideRepository = module.get<Repository<Ride>>(getRepositoryToken(Ride));
    userRepository = module.get<Repository<User>>(getRepositoryToken(User));
    vehicleRepository = module.get<Repository<Vehicle>>(
      getRepositoryToken(Vehicle),
    );
  });

  it('should be defined', () => {
    expect(service).toBeDefined();
  });

  const createMockRide = (
    id: number,
    userId: number,
    vehicleId: number,
    date: string,
  ): Ride => {
    return {
      id,
      userId,
      vehicleId,
      date,
      user: { id: userId } as User,
      vehicle: { id: vehicleId } as Vehicle,
    } as Ride;
  };

  describe('createRide', () => {
    it('should create a ride', async () => {
      const createRideDto = { userId: 1, vehicleId: 1, date: '2023-10-01' };
      const ride = createMockRide(1, 1, 1, '2023-10-01');
      jest.spyOn(rideRepository, 'create').mockReturnValue(ride);
      jest.spyOn(rideRepository, 'save').mockResolvedValue(ride);

      expect(await service.createRide(createRideDto)).toEqual(ride);
    });

    it('should throw an error if ride creation fails', async () => {
      const createRideDto = { userId: 1, vehicleId: 1, date: '2023-10-01' };
      jest.spyOn(rideRepository, 'create').mockReturnValue({} as Ride);
      jest
        .spyOn(rideRepository, 'save')
        .mockRejectedValue(new Error('Ride creation failed'));

      await expect(service.createRide(createRideDto)).rejects.toThrow(
        'Ride creation failed',
      );
    });
  });

  describe('findRideById', () => {
    it('should return a ride by id', async () => {
      const ride = createMockRide(1, 1, 1, '2023-10-01');
      jest.spyOn(rideRepository, 'findOne').mockResolvedValue(ride);

      expect(await service.findRideById(1)).toEqual(ride);
    });

    it('should return null if ride not found', async () => {
      jest.spyOn(rideRepository, 'findOne').mockResolvedValue(null);

      expect(await service.findRideById(1)).toBeNull();
    });
  });

  describe('deleteRide', () => {
    it('should delete a ride', async () => {
      const ride = createMockRide(1, 1, 1, '2023-10-01');
      jest.spyOn(rideRepository, 'findOne').mockResolvedValue(ride);
      jest.spyOn(rideRepository, 'delete').mockResolvedValue(undefined);

      await expect(service.deleteRide(1)).resolves.toBeUndefined();
    });

    it('should throw an error if ride not found', async () => {
      jest.spyOn(rideRepository, 'findOne').mockResolvedValue(null);

      await expect(service.deleteRide(1)).rejects.toThrow(NotFoundException);
    });
  });

  describe('findUserById', () => {
    it('should return a user by id', async () => {
      const user = { id: 1 } as User;
      jest.spyOn(userRepository, 'findOne').mockResolvedValue(user);

      expect(await service.findUserById(1)).toEqual(user);
    });

    it('should return null if user not found', async () => {
      jest.spyOn(userRepository, 'findOne').mockResolvedValue(null);

      expect(await service.findUserById(1)).toBeNull();
    });
  });

  describe('findVehicleById', () => {
    it('should return a vehicle by id', async () => {
      const vehicle = { id: 1 } as Vehicle;
      jest.spyOn(vehicleRepository, 'findOne').mockResolvedValue(vehicle);

      expect(await service.findVehicleById(1)).toEqual(vehicle);
    });

    it('should return null if vehicle not found', async () => {
      jest.spyOn(vehicleRepository, 'findOne').mockResolvedValue(null);

      expect(await service.findVehicleById(1)).toBeNull();
    });
  });
});
