using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LocationDemoSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CarLocations",
                columns: new[] { "Id", "RegionName" },
                values: new object[,]
                {
                    { 1, "Out of Bulgaria" },
                    { 2, "Blagoevgrad" },
                    { 3, "Sofia" },
                    { 4, "Plovdiv" }
                });

            migrationBuilder.InsertData(
                table: "CarLocationCities",
                columns: new[] { "Id", "CityName", "LocationId" },
                values: new object[,]
                {
                    { 1, "USA", 1 },
                    { 2, "Germany", 1 },
                    { 3, "Italy", 1 },
                    { 4, "Japan", 1 },
                    { 5, "Blagoevgrad", 2 },
                    { 6, "Bansko", 2 },
                    { 7, "Sofia", 3 },
                    { 8, "Botevgrad", 3 },
                    { 9, "Plovdiv", 4 },
                    { 10, "Asenovgrad", 4 },
                    { 11, "UK", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarLocationCities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarLocationCities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CarLocationCities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CarLocationCities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CarLocationCities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CarLocationCities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CarLocationCities",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CarLocationCities",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CarLocationCities",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CarLocationCities",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CarLocationCities",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CarLocations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarLocations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CarLocations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CarLocations",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
