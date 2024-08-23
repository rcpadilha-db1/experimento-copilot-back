import { Test, TestingModule } from '@nestjs/testing';
import { NotFoundException } from '@nestjs/common';
import { CreateUserDto } from 'src/dto/create-user.dto';
import { User } from 'src/entity/user.entity';
import { UserRepository } from 'src/repository/user.repository';
import { UserService } from 'src/service/user.service';


describe('UserService', () => {
  let userService: UserService;
  let userRepository: UserRepository;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [
        UserService,
        {
          provide: UserRepository,
          useValue: {
            findAll: jest.fn(),
            findOneById: jest.fn(),
            create: jest.fn(),
            delete: jest.fn(),
          },
        },
      ],
    }).compile();

    userService = module.get<UserService>(UserService);
    userRepository = module.get<UserRepository>(UserRepository);
  });

  describe('getAllUsers', () => {
    it('should return an array of users', async () => {
      const result: User[] = [{ id: '1', name: 'John Doe', email: 'john@example.com' }];
      jest.spyOn(userRepository, 'findAll').mockResolvedValue(result);

      expect(await userService.getAllUsers()).toBe(result);
    });
  });

  describe('getUserById', () => {
    it('should return a user by id', async () => {
      const result: User = { id: '1', name: 'John Doe', email: 'john@example.com' };
      jest.spyOn(userRepository, 'findOneById').mockResolvedValue(result);

      expect(await userService.getUserById('1')).toBe(result);
    });

    it('should throw a NotFoundException if user not found', async () => {
      jest.spyOn(userRepository, 'findOneById').mockResolvedValue(null);

      await expect(userService.getUserById('1')).rejects.toThrow(
        new NotFoundException('User with ID 1 not found')
      );
    });
  });

  describe('createUser', () => {
    it('should create a user', async () => {
      const createUserDto: CreateUserDto = { name: 'John Doe', email: 'john@example.com' };
      const result: User = { id: '1', ...createUserDto };
      jest.spyOn(userRepository, 'create').mockResolvedValue(result);

      expect(await userService.createUser(createUserDto)).toBe(result);
    });
  });

  describe('deleteUser', () => {
    it('should delete a user', async () => {
      jest.spyOn(userService, 'getUserById').mockResolvedValue({ id: '1' } as User);
      jest.spyOn(userRepository, 'delete').mockResolvedValue(undefined);

      await expect(userService.deleteUser('1')).resolves.not.toThrow();
    });

    it('should throw a NotFoundException if user not found', async () => {
      jest.spyOn(userService, 'getUserById').mockResolvedValue(null);

      await expect(userService.deleteUser('1')).rejects.toThrow(
        new NotFoundException('User with ID 1 not found')
      );
    });
  });
});
