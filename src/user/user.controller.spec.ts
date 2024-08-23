import { Test, TestingModule } from '@nestjs/testing';
import { UserController } from './user.controller';
import { UserService } from './user.service';
import { CreateUserDto } from './create-user.dto';
import { BadRequestException } from '@nestjs/common';
import { User } from './user.entity'; // Ensure User entity is imported

describe('UserController', () => {
  let controller: UserController;
  let service: UserService;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [UserController],
      providers: [
        {
          provide: UserService,
          useValue: {
            createUser: jest.fn(),
            deleteUser: jest.fn(),
            findUserById: jest.fn(),
          },
        },
      ],
    }).compile();

    controller = module.get<UserController>(UserController);
    service = module.get<UserService>(UserService);
  });

  it('should be defined', () => {
    expect(controller).toBeDefined();
  });

  describe('createUser', () => {
    it('should create a user', async () => {
      const createUserDto: CreateUserDto = {
        name: 'John Doe',
        email: 'john.doe@example.com',
      };
      jest.spyOn(service, 'createUser').mockResolvedValue({ id: 1 } as User);

      const result = await controller.createUser(createUserDto);
      expect(result).toEqual({ id: 1 });
    });

    it('should throw BadRequestException if user creation fails', async () => {
      const createUserDto: CreateUserDto = {
        name: 'John Doe',
        email: 'john.doe@example.com',
      };
      jest
        .spyOn(service, 'createUser')
        .mockRejectedValue(new Error('User creation failed'));

      await expect(controller.createUser(createUserDto)).rejects.toThrow(
        BadRequestException,
      );
    });
  });

  describe('deleteUser', () => {
    it('should delete a user', async () => {
      jest.spyOn(service, 'findUserById').mockResolvedValue({ id: 1 } as User);
      jest.spyOn(service, 'deleteUser').mockResolvedValue(undefined);

      await expect(controller.deleteUser(1)).resolves.toBeUndefined();
    });

    it('should throw BadRequestException if user not found', async () => {
      jest.spyOn(service, 'findUserById').mockResolvedValue(null);

      await expect(controller.deleteUser(1)).rejects.toThrow(
        BadRequestException,
      );
    });
  });

  describe('findUserById', () => {
    it('should return a user', async () => {
      const user: User = {
        id: 1,
        name: 'John Doe',
        email: 'john.doe@example.com',
        rides: [],
        vehicles: [],
      };
      jest.spyOn(service, 'findUserById').mockResolvedValue(user);

      const result = await controller.getUserById(1);
      expect(result).toEqual(user);
    });

    it('should throw BadRequestException if user not found', async () => {
      jest.spyOn(service, 'findUserById').mockResolvedValue(null);

      await expect(controller.getUserById(1)).rejects.toThrow(
        BadRequestException,
      );
    });
  });
});
