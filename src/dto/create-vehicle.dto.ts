import { IsString, IsNumber } from 'class-validator';

export class CreateVehicleDto {
  @IsString()
  plate: string;

  @IsNumber()
  capacity: number;

  @IsString()
  ownerId: string; 
}
