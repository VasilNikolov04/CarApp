using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddYearAndDrivetrain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarListings_CarLocation_LocationId",
                table: "CarListings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarLocation",
                table: "CarLocation");

            migrationBuilder.RenameTable(
                name: "CarLocation",
                newName: "CarLocations");

            migrationBuilder.AddColumn<int>(
                name: "DrivetrainId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarLocations",
                table: "CarLocations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CarDrivetrains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrivetrainName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDrivetrains", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DrivetrainId",
                table: "Cars",
                column: "DrivetrainId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarListings_CarLocations_LocationId",
                table: "CarListings",
                column: "LocationId",
                principalTable: "CarLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarDrivetrains_DrivetrainId",
                table: "Cars",
                column: "DrivetrainId",
                principalTable: "CarDrivetrains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarListings_CarLocations_LocationId",
                table: "CarListings");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarDrivetrains_DrivetrainId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarDrivetrains");

            migrationBuilder.DropIndex(
                name: "IX_Cars_DrivetrainId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarLocations",
                table: "CarLocations");

            migrationBuilder.DropColumn(
                name: "DrivetrainId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "CarLocations",
                newName: "CarLocation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarLocation",
                table: "CarLocation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarListings_CarLocation_LocationId",
                table: "CarListings",
                column: "LocationId",
                principalTable: "CarLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
