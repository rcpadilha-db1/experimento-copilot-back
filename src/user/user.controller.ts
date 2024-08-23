import {
  Controller,
  Post,
  Get,
  Param,
  Body,
  Put,
  Delete,
  BadRequestException,
} from '@nestjs/common';
import { UserService } from './user.service';
import { CreateUserDto } from './create-user.dto';
import { BaseController } from '../common/base.controller';
import { UpdateUserDto } from './update-user.dto';

@Controller('user')
export class UserController extends BaseController {
  constructor(private readonly userService: UserService) {
    super();
  }

  @Post()
  async createUser(@Body() createUserDto: CreateUserDto) {
    try {
      return await this.userService.createUser(createUserDto);
    } catch (error) {
      throw new BadRequestException('User creation failed');
    }
  }

  @Get()
  async getUsers() {
    return this.userService.findAllUsers();
  }

  @Get(':id')
  async getUserById(@Param('id') id: number) {
    return this.checkIfExists(
      () => this.userService.findUserById(id),
      'User not found',
    );
  }

  @Put(':id')
  async updateUser(
    @Param('id') id: number,
    @Body() updateUserDto: UpdateUserDto,
  ) {
    await this.checkIfExists(
      () => this.userService.findUserById(id),
      'User not found',
    );
    return this.userService.updateUser(id, updateUserDto);
  }

  @Delete(':id')
  async deleteUser(@Param('id') id: number) {
    await this.checkIfExists(
      () => this.userService.findUserById(id),
      'User not found',
    );
    return this.userService.deleteUser(id);
  }
}
