using Microsoft.EntityFrameworkCore.Migrations;

namespace Experimento.Data.Seeds;

public class InitialSeed
{
    public static void Seed(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"
                INSERT INTO [User] (Id, Name, Email)
                VALUES 
                ('user1', 'John Doe', 'johndoe@example.com'),
                ('user2', 'Jane Doe', 'janedoe@example.com')
            ");
        
        migrationBuilder.Sql(@"
                INSERT INTO Vehicle (Id, Plate, Capacity, OwnerId)
                VALUES 
                ('vehicle1', 'ABC-1234', 4, 'user1'),
                ('vehicle2', 'XYZ-5678', 4, 'user2')
            ");
        
        migrationBuilder.Sql(@"
                INSERT INTO Ride (Id, Date, RiderId, VehicleId)
                VALUES 
                ('ride1', '2024-08-26', 'user1', 'vehicle1'),
                ('ride2', '2024-08-27', 'user2', 'vehicle2')
            ");
    }
}

