import { BadRequestException } from '@nestjs/common';

export class BaseController {
  async checkIfExists(serviceMethod: () => Promise<any>, errorMessage: string) {
    const entity = await serviceMethod();
    if (!entity) {
      throw new BadRequestException(errorMessage);
    }
    return entity;
  }
}
