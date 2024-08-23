import { IsNotEmpty, IsString, IsDateString } from 'class-validator';

export class CreateRideDto {
  @IsNotEmpty()
  @IsString()
  vehicleId: string;

  @IsNotEmpty()
  @IsString()
  riderId: string;

  @IsNotEmpty()
  @IsDateString()  
  date: string; 
}