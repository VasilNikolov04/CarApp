using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDrivetrainIdName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DrivetrainId",
                table: "CarDrivetrains",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CarDrivetrains",
                newName: "DrivetrainId");
        }
    }
}
