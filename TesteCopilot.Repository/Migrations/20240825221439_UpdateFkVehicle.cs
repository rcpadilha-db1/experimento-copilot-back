using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteCopilot.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFkVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Users_OwenerId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<int>(
                name: "OwenerId",
                table: "Vehicles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Users_OwenerId",
                table: "Vehicles",
                column: "OwenerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Users_OwenerId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<int>(
                name: "OwenerId",
                table: "Vehicles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Users_OwenerId",
                table: "Vehicles",
                column: "OwenerId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
