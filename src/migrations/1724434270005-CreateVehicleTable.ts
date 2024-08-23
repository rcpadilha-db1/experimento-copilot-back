import {
  MigrationInterface,
  QueryRunner,
  Table,
  TableForeignKey,
} from 'typeorm';

export class CreateVehicleTable1724434270005 implements MigrationInterface {
  public async up(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.createTable(
      new Table({
        name: 'vehicle',
        columns: [
          {
            name: 'id',
            type: 'int',
            isPrimary: true,
            isGenerated: true,
            generationStrategy: 'increment',
          },
          {
            name: 'plate',
            type: 'varchar',
          },
          {
            name: 'capacity',
            type: 'int',
          },
          {
            name: 'ownerId',
            type: 'int',
          },
        ],
      }),
      true,
    );

    await queryRunner.createForeignKey(
      'vehicle',
      new TableForeignKey({
        columnNames: ['ownerId'],
        referencedColumnNames: ['id'],
        referencedTableName: 'user',
        onDelete: 'CASCADE',
      }),
    );
  }

  public async down(queryRunner: QueryRunner): Promise<void> {
    const table = await queryRunner.getTable('vehicle');
    const foreignKey = table.foreignKeys.find(
      (fk) => fk.columnNames.indexOf('ownerId') !== -1,
    );
    await queryRunner.dropForeignKey('vehicle', foreignKey);
    await queryRunner.dropTable('vehicle');
  }
}
