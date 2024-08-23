import { IsString, IsNumber, Min, IsNotEmpty } from 'class-validator';
import { Type } from 'class-transformer';

export class CreateVehicleDto {
  @IsString()
  @IsNotEmpty()
  plate: string;

  @IsNumber()
  @Type(() => Number)
  @IsNotEmpty()
  ownerId: number;

  @IsNumber()
  @Type(() => Number)
  @Min(1)
  @IsNotEmpty()
  capacity: number;
}
