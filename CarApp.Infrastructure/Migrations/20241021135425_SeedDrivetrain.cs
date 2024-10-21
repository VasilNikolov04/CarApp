using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDrivetrain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CarDrivetrains",
                newName: "DrivetrainId");

            migrationBuilder.InsertData(
                table: "CarDrivetrains",
                columns: new[] { "DrivetrainId", "DrivetrainName" },
                values: new object[,]
                {
                    { 1, "Rear-Wheel Drive (RWD)" },
                    { 2, "Front-Wheel Drive (FWD)" },
                    { 3, "All-Wheel Drive (AWD)" },
                    { 4, "Four-Wheel Drive (4x4)" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarDrivetrains",
                keyColumn: "DrivetrainId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarDrivetrains",
                keyColumn: "DrivetrainId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CarDrivetrains",
                keyColumn: "DrivetrainId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CarDrivetrains",
                keyColumn: "DrivetrainId",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "DrivetrainId",
                table: "CarDrivetrains",
                newName: "Id");
        }
    }
}
