using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteCopilot.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Users_RiderId",
                table: "Rides");

            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Vehicles_VehicleId",
                table: "Rides");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Users_OwenerId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rides",
                table: "Rides");

            migrationBuilder.RenameTable(
                name: "Rides",
                newName: "Riders");

            migrationBuilder.RenameIndex(
                name: "IX_Rides_VehicleId",
                table: "Riders",
                newName: "IX_Riders_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Rides_RiderId",
                table: "Riders",
                newName: "IX_Riders_RiderId");

            migrationBuilder.AlterColumn<string>(
                name: "Plate",
                table: "Vehicles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "OwenerId",
                table: "Vehicles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Riders",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "RiderId",
                table: "Riders",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Riders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VeiculoId",
                table: "Riders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Riders",
                table: "Riders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Riders_Users_RiderId",
                table: "Riders",
                column: "RiderId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Riders_Vehicles_VehicleId",
                table: "Riders",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Users_OwenerId",
                table: "Vehicles",
                column: "OwenerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Riders_Users_RiderId",
                table: "Riders");

            migrationBuilder.DropForeignKey(
                name: "FK_Riders_Vehicles_VehicleId",
                table: "Riders");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Users_OwenerId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Riders",
                table: "Riders");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Riders");

            migrationBuilder.DropColumn(
                name: "VeiculoId",
                table: "Riders");

            migrationBuilder.RenameTable(
                name: "Riders",
                newName: "Rides");

            migrationBuilder.RenameIndex(
                name: "IX_Riders_VehicleId",
                table: "Rides",
                newName: "IX_Rides_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Riders_RiderId",
                table: "Rides",
                newName: "IX_Rides_RiderId");

            migrationBuilder.AlterColumn<string>(
                name: "Plate",
                table: "Vehicles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OwenerId",
                table: "Vehicles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Rides",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RiderId",
                table: "Rides",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rides",
                table: "Rides",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Users_RiderId",
                table: "Rides",
                column: "RiderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Vehicles_VehicleId",
                table: "Rides",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Users_OwenerId",
                table: "Vehicles",
                column: "OwenerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
