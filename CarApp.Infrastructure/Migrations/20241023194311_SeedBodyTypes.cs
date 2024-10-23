using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedBodyTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarCategories_CategoryId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarCategories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Cars",
                newName: "CarBodyId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_CategoryId",
                table: "Cars",
                newName: "IX_Cars_CarBodyId");

            migrationBuilder.CreateTable(
                name: "CarBodyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBodyTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CarBodyTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Convertible" },
                    { 2, "Coupe" },
                    { 3, "SUV" },
                    { 4, "Sedan" },
                    { 5, "Van" },
                    { 6, "Hatchback" },
                    { 7, "Station Wagon" },
                    { 8, "Pickup Truck" },
                    { 9, "Compact" },
                    { 10, "Other" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarBodyTypes_CarBodyId",
                table: "Cars",
                column: "CarBodyId",
                principalTable: "CarBodyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarBodyTypes_CarBodyId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarBodyTypes");

            migrationBuilder.RenameColumn(
                name: "CarBodyId",
                table: "Cars",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_CarBodyId",
                table: "Cars",
                newName: "IX_Cars_CategoryId");

            migrationBuilder.CreateTable(
                name: "CarCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCategories", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarCategories_CategoryId",
                table: "Cars",
                column: "CategoryId",
                principalTable: "CarCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
