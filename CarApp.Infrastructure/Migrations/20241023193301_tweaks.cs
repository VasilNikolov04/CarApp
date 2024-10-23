using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tweaks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarListings_CarLocations_LocationId",
                table: "CarListings");

            migrationBuilder.DropIndex(
                name: "IX_CarListings_LocationId",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "CarListings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "CarListings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CarListings_LocationId",
                table: "CarListings",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarListings_CarLocations_LocationId",
                table: "CarListings",
                column: "LocationId",
                principalTable: "CarLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
