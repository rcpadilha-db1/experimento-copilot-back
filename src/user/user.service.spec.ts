import { Test, TestingModule } from '@nestjs/testing';
import { UserService } from './user.service';
import { getRepositoryToken } from '@nestjs/typeorm';
import { User } from './user.entity';
import { Repository } from 'typeorm';
import { NotFoundException } from '@nestjs/common';

describe('UserService', () => {
  let service: UserService;
  let userRepository: Repository<User>;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [
        UserService,
        {
          provide: getRepositoryToken(User),
          useValue: {
            create: jest.fn(),
            save: jest.fn(),
            findOne: jest.fn(),
            find: jest.fn(),
            update: jest.fn(),
            delete: jest.fn(),
          },
        },
      ],
    }).compile();

    service = module.get<UserService>(UserService);
    userRepository = module.get<Repository<User>>(getRepositoryToken(User));
  });

  it('should be defined', () => {
    expect(service).toBeDefined();
  });

  describe('createUser', () => {
    it('should create a user', async () => {
      const createUserDto = { name: 'John Doe', email: 'john.doe@example.com' };
      const user = { id: 1, ...createUserDto } as User;
      jest.spyOn(userRepository, 'create').mockReturnValue(user);
      jest.spyOn(userRepository, 'save').mockResolvedValue(user);

      expect(await service.createUser(createUserDto)).toEqual(user);
    });

    it('should throw an error if user creation fails', async () => {
      const createUserDto = { name: 'John Doe', email: 'john.doe@example.com' };
      jest.spyOn(userRepository, 'create').mockReturnValue({} as User);
      jest
        .spyOn(userRepository, 'save')
        .mockRejectedValue(new Error('User creation failed'));

      await expect(service.createUser(createUserDto)).rejects.toThrow(
        'User creation failed',
      );
    });
  });

  describe('findUserById', () => {
    it('should return a user by id', async () => {
      const user = {
        id: 1,
        name: 'John Doe',
        email: 'john.doe@example.com',
      } as User;
      jest.spyOn(userRepository, 'findOne').mockResolvedValue(user);

      expect(await service.findUserById(1)).toEqual(user);
    });

    it('should return null if user not found', async () => {
      jest.spyOn(userRepository, 'findOne').mockResolvedValue(null);

      expect(await service.findUserById(1)).toBeNull();
    });
  });

  describe('findAllUsers', () => {
    it('should return an array of users', async () => {
      const users = [
        { id: 1, name: 'John Doe', email: 'john.doe@example.com' },
        { id: 2, name: 'Jane Doe', email: 'jane.doe@example.com' },
      ] as User[];
      jest.spyOn(userRepository, 'find').mockResolvedValue(users);

      expect(await service.findAllUsers()).toEqual(users);
    });
  });

  describe('updateUser', () => {
    it('should update a user', async () => {
      const updateUserDto = { name: 'John Doe', email: 'john.doe@example.com' };
      const user = { id: 1, ...updateUserDto } as User;
      jest.spyOn(userRepository, 'findOne').mockResolvedValue(user);
      jest.spyOn(userRepository, 'save').mockResolvedValue(user);

      expect(await service.updateUser(1, updateUserDto)).toEqual(user);
    });

    it('should throw an error if user not found', async () => {
      const updateUserDto = { name: 'John Doe', email: 'john.doe@example.com' };
      jest.spyOn(userRepository, 'findOne').mockResolvedValue(null);

      await expect(service.updateUser(1, updateUserDto)).rejects.toThrow(
        NotFoundException,
      );
    });
  });

  describe('deleteUser', () => {
    it('should delete a user', async () => {
      const user = {
        id: 1,
        name: 'John Doe',
        email: 'john.doe@example.com',
      } as User;
      jest.spyOn(userRepository, 'findOne').mockResolvedValue(user);
      jest.spyOn(userRepository, 'delete').mockResolvedValue(undefined);

      await expect(service.deleteUser(1)).resolves.toBeUndefined();
    });

    it('should throw an error if user not found', async () => {
      jest.spyOn(userRepository, 'findOne').mockResolvedValue(null);

      await expect(service.deleteUser(1)).rejects.toThrow(NotFoundException);
    });
  });
});
