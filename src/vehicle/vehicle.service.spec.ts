import { Test, TestingModule } from '@nestjs/testing';
import { VehicleService } from './vehicle.service';
import { getRepositoryToken } from '@nestjs/typeorm';
import { Vehicle } from './vehicle.entity';
import { User } from '../user/user.entity';
import { Repository } from 'typeorm';
import { NotFoundException } from '@nestjs/common';

describe('VehicleService', () => {
  let service: VehicleService;
  let vehicleRepository: Repository<Vehicle>;
  let userRepository: Repository<User>;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [
        VehicleService,
        {
          provide: getRepositoryToken(Vehicle),
          useValue: {
            create: jest.fn(),
            save: jest.fn(),
            findOne: jest.fn(),
            find: jest.fn(),
            update: jest.fn(),
            delete: jest.fn(),
          },
        },
        {
          provide: getRepositoryToken(User),
          useValue: {
            findOne: jest.fn(),
          },
        },
      ],
    }).compile();

    service = module.get<VehicleService>(VehicleService);
    vehicleRepository = module.get<Repository<Vehicle>>(
      getRepositoryToken(Vehicle),
    );
    userRepository = module.get<Repository<User>>(getRepositoryToken(User));
  });

  it('should be defined', () => {
    expect(service).toBeDefined();
  });

  const createMockVehicle = (
    id: number,
    plate: string,
    ownerId: number,
    capacity: number,
  ): Vehicle => {
    return {
      id,
      plate,
      capacity,
      owner: { id: ownerId } as User,
      rides: [],
    } as Vehicle;
  };

  describe('createVehicle', () => {
    it('should create a vehicle', async () => {
      const createVehicleDto = { plate: 'ABC123', ownerId: 1, capacity: 4 };
      const vehicle = createMockVehicle(1, 'ABC123', 1, 4);
      jest.spyOn(vehicleRepository, 'create').mockReturnValue(vehicle);
      jest.spyOn(vehicleRepository, 'save').mockResolvedValue(vehicle);

      expect(await service.createVehicle(createVehicleDto)).toEqual(vehicle);
    });

    it('should throw an error if vehicle creation fails', async () => {
      const createVehicleDto = { plate: 'ABC123', ownerId: 1, capacity: 4 };
      jest.spyOn(vehicleRepository, 'create').mockReturnValue({} as Vehicle);
      jest
        .spyOn(vehicleRepository, 'save')
        .mockRejectedValue(new Error('Vehicle creation failed'));

      await expect(service.createVehicle(createVehicleDto)).rejects.toThrow(
        'Vehicle creation failed',
      );
    });
  });

  describe('findAllVehicles', () => {
    it('should return an array of vehicles', async () => {
      const vehicles = [
        createMockVehicle(1, 'ABC123', 1, 4),
        createMockVehicle(2, 'XYZ789', 2, 5),
      ];
      jest.spyOn(vehicleRepository, 'find').mockResolvedValue(vehicles);

      expect(await service.findAllVehicles()).toEqual(vehicles);
    });
  });

  describe('findVehicleById', () => {
    it('should return a vehicle by id', async () => {
      const vehicle = createMockVehicle(1, 'ABC123', 1, 4);
      jest.spyOn(vehicleRepository, 'findOne').mockResolvedValue(vehicle);

      expect(await service.findVehicleById(1)).toEqual(vehicle);
    });

    it('should return null if vehicle not found', async () => {
      jest.spyOn(vehicleRepository, 'findOne').mockResolvedValue(null);

      expect(await service.findVehicleById(1)).toBeNull();
    });
  });

  describe('updateVehicle', () => {
    it('should update a vehicle', async () => {
      const updateVehicleDto = { plate: 'ABC123', ownerId: 1, capacity: 4 };
      const vehicle = createMockVehicle(1, 'ABC123', 1, 4);
      jest.spyOn(vehicleRepository, 'findOne').mockResolvedValue(vehicle);
      jest.spyOn(vehicleRepository, 'save').mockResolvedValue(vehicle);

      expect(await service.updateVehicle(1, updateVehicleDto)).toEqual(vehicle);
    });

    it('should throw an error if vehicle not found', async () => {
      const updateVehicleDto = { plate: 'ABC123', ownerId: 1, capacity: 4 };
      jest.spyOn(vehicleRepository, 'findOne').mockResolvedValue(null);

      await expect(service.updateVehicle(1, updateVehicleDto)).rejects.toThrow(
        NotFoundException,
      );
    });
  });

  describe('deleteVehicle', () => {
    it('should delete a vehicle', async () => {
      const vehicle = createMockVehicle(1, 'ABC123', 1, 4);
      jest.spyOn(vehicleRepository, 'findOne').mockResolvedValue(vehicle);
      jest.spyOn(vehicleRepository, 'delete').mockResolvedValue(undefined);

      await expect(service.deleteVehicle(1)).resolves.toBeUndefined();
    });

    it('should throw an error if vehicle not found', async () => {
      jest.spyOn(vehicleRepository, 'findOne').mockResolvedValue(null);

      await expect(service.deleteVehicle(1)).rejects.toThrow(NotFoundException);
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
});
