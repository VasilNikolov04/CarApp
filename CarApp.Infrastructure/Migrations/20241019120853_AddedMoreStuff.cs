using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreStuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarFuelType_FuelId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarFuelType",
                table: "CarFuelType");

            migrationBuilder.RenameTable(
                name: "CarFuelType",
                newName: "CarFuelTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarFuelTypes",
                table: "CarFuelTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarFuelTypes_FuelId",
                table: "Cars",
                column: "FuelId",
                principalTable: "CarFuelTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarFuelTypes_FuelId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarFuelTypes",
                table: "CarFuelTypes");

            migrationBuilder.RenameTable(
                name: "CarFuelTypes",
                newName: "CarFuelType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarFuelType",
                table: "CarFuelType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarFuelType_FuelId",
                table: "Cars",
                column: "FuelId",
                principalTable: "CarFuelType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
