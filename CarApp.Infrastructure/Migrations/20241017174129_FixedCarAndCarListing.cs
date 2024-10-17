using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedCarAndCarListing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_CarLocation_LocationId",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_LocationId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Mileage",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Car");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CarListing",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "CarListing",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mileage",
                table: "CarListing",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CarListing",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_CarListing_LocationId",
                table: "CarListing",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarListing_CarLocation_LocationId",
                table: "CarListing",
                column: "LocationId",
                principalTable: "CarLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarListing_CarLocation_LocationId",
                table: "CarListing");

            migrationBuilder.DropIndex(
                name: "IX_CarListing_LocationId",
                table: "CarListing");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CarListing");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "CarListing");

            migrationBuilder.DropColumn(
                name: "Mileage",
                table: "CarListing");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CarListing");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Car",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mileage",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Car",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Car_LocationId",
                table: "Car",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarLocation_LocationId",
                table: "Car",
                column: "LocationId",
                principalTable: "CarLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
