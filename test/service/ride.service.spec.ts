import { Test, TestingModule } from '@nestjs/testing';
import { Repository, DeleteResult } from 'typeorm';
import { getRepositoryToken } from '@nestjs/typeorm';
import { RideService } from '../../src/service/ride.service';
import { Ride } from '../../src/entity/ride.entity';
import { Vehicle } from '../../src/entity/vehicle.entity';
import { User } from '../../src/entity/user.entity';
import { CreateRideDto } from '../../src/dto/create-ride-dto';
import { NotFoundException, BadRequestException } from '@nestjs/common';

describe('RideService', () => {
  let service: RideService;
  let rideRepository: Repository<Ride>;
  let vehicleRepository: Repository<Vehicle>;
  let userRepository: Repository<User>;

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
          provide: getRepositoryToken(Vehicle),
          useValue: {
            findOneBy: jest.fn(),
          },
        },
        {
          provide: getRepositoryToken(User),
          useValue: {
            findOneBy: jest.fn(),
          },
        },
      ],
    }).compile();

    service = module.get<RideService>(RideService);
    rideRepository = module.get<Repository<Ride>>(getRepositoryToken(Ride));
    vehicleRepository = module.get<Repository<Vehicle>>(getRepositoryToken(Vehicle));
    userRepository = module.get<Repository<User>>(getRepositoryToken(User));
  });

  it('should be defined', () => {
    expect(service).toBeDefined();
  });

  describe('createRide', () => {
    it('should create and save a ride', async () => {
      const createRideDto: CreateRideDto = {
        vehicleId: '1',
        riderId: '1',
        date: '2024-08-24T10:00:00Z',
      };
      const vehicle = new Vehicle();
      const rider = new User();
      const ride = new Ride();
      Object.assign(ride, createRideDto);

      jest.spyOn(vehicleRepository, 'findOneBy').mockResolvedValue(vehicle);
      jest.spyOn(userRepository, 'findOneBy').mockResolvedValue(rider);
      jest.spyOn(rideRepository, 'findOne').mockResolvedValue(null);
      jest.spyOn(rideRepository, 'create').mockReturnValue(ride);
      jest.spyOn(rideRepository, 'save').mockResolvedValue(ride);

      expect(await service.createRide(createRideDto)).toEqual(ride);
    });

    it('should throw NotFoundException if vehicle is not found', async () => {
      const createRideDto: CreateRideDto = {
        vehicleId: '1',
        riderId: '1',
        date: '2024-08-24T10:00:00Z',
      };

      jest.spyOn(vehicleRepository, 'findOneBy').mockResolvedValue(null);

      await expect(service.createRide(createRideDto)).rejects.toThrow(NotFoundException);
    });

    it('should throw NotFoundException if rider is not found', async () => {
      const createRideDto: CreateRideDto = {
        vehicleId: '1',
        riderId: '1',
        date: '2024-08-24T10:00:00Z',
      };
      const vehicle = new Vehicle();

      jest.spyOn(vehicleRepository, 'findOneBy').mockResolvedValue(vehicle);
      jest.spyOn(userRepository, 'findOneBy').mockResolvedValue(null);

      await expect(service.createRide(createRideDto)).rejects.toThrow(NotFoundException);
    });

    it('should throw BadRequestException if ride already exists', async () => {
      const createRideDto: CreateRideDto = {
        vehicleId: '1',
        riderId: '1',
        date: '2024-08-24T10:00:00Z',
      };
      const vehicle = new Vehicle();
      const rider = new User();
      const existingRide = new Ride();

      jest.spyOn(vehicleRepository, 'findOneBy').mockResolvedValue(vehicle);
      jest.spyOn(userRepository, 'findOneBy').mockResolvedValue(rider);
      jest.spyOn(rideRepository, 'findOne').mockResolvedValue(existingRide);

      await expect(service.createRide(createRideDto)).rejects.toThrow(BadRequestException);
    });
  });

  describe('findAllByUser', () => {
    it('should return an array of rides', async () => {
      const rideArray = [
        new Ride(),
        new Ride(),
      ];
      jest.spyOn(rideRepository, 'find').mockResolvedValue(rideArray);

      expect(await service.findAllByUser('1')).toEqual(rideArray);
    });
  });

  describe('removeRide', () => {
    it('should remove a ride', async () => {
      const deleteResult: DeleteResult = {
        affected: 1,
        raw: [],
      };

      jest.spyOn(rideRepository, 'delete').mockResolvedValue(deleteResult);

      await expect(service.removeRide('1')).resolves.not.toThrow();
    });

    it('should throw NotFoundException if ride is not found', async () => {
      const deleteResult: DeleteResult = {
        affected: 0,
        raw: [],
      };

      jest.spyOn(rideRepository, 'delete').mockResolvedValue(deleteResult);

      await expect(service.removeRide('1')).rejects.toThrow(NotFoundException);
    });
  });
});
