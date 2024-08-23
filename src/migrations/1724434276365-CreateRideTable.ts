import {
  MigrationInterface,
  QueryRunner,
  Table,
  TableForeignKey,
} from 'typeorm';

export class CreateRideTable1724434276365 implements MigrationInterface {
  public async up(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.createTable(
      new Table({
        name: 'ride',
        columns: [
          {
            name: 'id',
            type: 'int',
            isPrimary: true,
            isGenerated: true,
            generationStrategy: 'increment',
          },
          {
            name: 'vehicleId',
            type: 'int',
          },
          {
            name: 'userId',
            type: 'int',
          },
          {
            name: 'date',
            type: 'timestamp',
          },
        ],
      }),
      true,
    );

    await queryRunner.createForeignKey(
      'ride',
      new TableForeignKey({
        columnNames: ['vehicleId'],
        referencedColumnNames: ['id'],
        referencedTableName: 'vehicle',
        onDelete: 'CASCADE',
      }),
    );

    await queryRunner.createForeignKey(
      'ride',
      new TableForeignKey({
        columnNames: ['userId'],
        referencedColumnNames: ['id'],
        referencedTableName: 'user',
        onDelete: 'CASCADE',
      }),
    );
  }

  public async down(queryRunner: QueryRunner): Promise<void> {
    const table = await queryRunner.getTable('ride');
    const vehicleForeignKey = table.foreignKeys.find(
      (fk) => fk.columnNames.indexOf('vehicleId') !== -1,
    );
    const riderForeignKey = table.foreignKeys.find(
      (fk) => fk.columnNames.indexOf('userId') !== -1,
    );
    await queryRunner.dropForeignKey('ride', vehicleForeignKey);
    await queryRunner.dropForeignKey('ride', riderForeignKey);
    await queryRunner.dropTable('ride');
  }
}
