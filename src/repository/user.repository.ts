import { Injectable } from '@nestjs/common';
import { Repository } from 'typeorm';
import { InjectRepository } from '@nestjs/typeorm';
import { User } from 'src/entity/user.entity';

@Injectable()
export class UserRepository {
  constructor(
    @InjectRepository(User)
    private readonly repository: Repository<User>,
  ) {}

  async findAll(): Promise<User[]> {
    return this.repository.find();
  }

  async findOneById(id: string): Promise<User | undefined> {
    return this.repository.findOne({ where: { id } });
  }

  async findOneByEmail(email: string): Promise<User | undefined> {
    return this.repository.findOne({ where: { email } });
  }

  async create(user: User): Promise<User> {
    return this.repository.save(user);
  }

  async delete(id: string): Promise<void> {
    await this.repository.delete(id);
  }
}
