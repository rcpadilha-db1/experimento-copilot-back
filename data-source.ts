import { DataSource } from 'typeorm';
import { User } from './src/user/user.entity';
import { Ride } from './src/ride/ride.entity';
import { Vehicle } from './src/vehicle/vehicle.entity';

export const AppDataSource = new DataSource({
  type: 'mysql',
  host: 'mysql', // Use 'mysql' here since it's the service name in your Docker Compose
  port: 3306,
  username: 'user',
  password: 'password',
  database: 'app',
  entities: [User, Ride, Vehicle],
  migrations: ['src/migrations/*{.ts,.js}'],
  synchronize: false,
});
