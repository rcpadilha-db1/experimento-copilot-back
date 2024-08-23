import { Test, TestingModule } from '@nestjs/testing';
import { Repository, DeleteResult } from 'typeorm';
import { getRepositoryToken } from '@nestjs/typeorm';
import { VehicleService } from '../../src/service/vehicle.service';
import { Vehicle } from '../../src/entity/vehicle.entity';

describe('VehicleService', () => {
  let service: VehicleService;
  let repository: Repository<Vehicle>;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [
        VehicleService,
        {
          provide: getRepositoryToken(Vehicle),
          useValue: {
            create: jest.fn(), 
            save: jest.fn(),
            find: jest.fn(),
            findOneBy: jest.fn(),
            delete: jest.fn(),
          },
        },
      ],
    }).compile();

    service = module.get<VehicleService>(VehicleService);
    repository = module.get<Repository<Vehicle>>(getRepositoryToken(Vehicle));
  });

  it('should be defined', () => {
    expect(service).toBeDefined();
  });

  describe('createVehicle', () => {
    it('should create and save a vehicle', async () => {
      const createVehicleDto = { capacity: 1, plate: 'ABC1232', ownerId: '1' }; 
      const vehicle = new Vehicle();
      Object.assign(vehicle, createVehicleDto);

      jest.spyOn(repository, 'create').mockReturnValue(vehicle);
      jest.spyOn(repository, 'save').mockResolvedValue(vehicle);

      expect(await service.createVehicle(createVehicleDto)).toEqual(vehicle);
    });
  });

  describe('findAll', () => {
    it('should return an array of vehicles', async () => {
      const vehicleArray = [
        new Vehicle(),
        new Vehicle(),
      ];

      jest.spyOn(repository, 'find').mockResolvedValue(vehicleArray);

      expect(await service.findAll()).toEqual(vehicleArray);
    });
  });

  describe('findOne', () => {
    it('should return a single vehicle', async () => {
      const vehicle = new Vehicle();
      vehicle.id = '1';

      jest.spyOn(repository, 'findOneBy').mockResolvedValue(vehicle);

      expect(await service.findOne('1')).toEqual(vehicle);
    });
  });

  describe('remove', () => {
    it('should remove a vehicle', async () => {
      const deleteResult: DeleteResult = {
        affected: 1,
        raw: [], 
      };

      jest.spyOn(repository, 'delete').mockResolvedValue(deleteResult);

      await expect(service.remove('1')).resolves.not.toThrow();
    });
  });
});
