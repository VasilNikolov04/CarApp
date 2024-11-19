using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LocationTablesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "CarLocations");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "CarLocations");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "CarLocations",
                newName: "RegionName");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "CarListings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CarLocationCities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarLocationCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarLocationCities_CarLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "CarLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarListings_CityId",
                table: "CarListings",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CarLocationCities_LocationId",
                table: "CarLocationCities",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarListings_CarLocationCities_CityId",
                table: "CarListings",
                column: "CityId",
                principalTable: "CarLocationCities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarListings_CarLocationCities_CityId",
                table: "CarListings");

            migrationBuilder.DropTable(
                name: "CarLocationCities");

            migrationBuilder.DropIndex(
                name: "IX_CarListings_CityId",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "CarListings");

            migrationBuilder.RenameColumn(
                name: "RegionName",
                table: "CarLocations",
                newName: "Country");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "CarLocations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "CarLocations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
