using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seededCarGears : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarTransmissions_TransmissionID",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarTransmissions");

            migrationBuilder.RenameColumn(
                name: "TransmissionID",
                table: "Cars",
                newName: "GearId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_TransmissionID",
                table: "Cars",
                newName: "IX_Cars_GearId");

            migrationBuilder.CreateTable(
                name: "CarGears",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GearName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarGears", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CarGears",
                columns: new[] { "Id", "GearName" },
                values: new object[,]
                {
                    { 1, "Manual" },
                    { 2, "Automatic" },
                    { 3, "Semi-Automatic" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarGears_GearId",
                table: "Cars",
                column: "GearId",
                principalTable: "CarGears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarGears_GearId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarGears");

            migrationBuilder.RenameColumn(
                name: "GearId",
                table: "Cars",
                newName: "TransmissionID");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_GearId",
                table: "Cars",
                newName: "IX_Cars_TransmissionID");

            migrationBuilder.CreateTable(
                name: "CarTransmissions",
                columns: table => new
                {
                    TransmissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransmissionAbbreviation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTransmissions", x => x.TransmissionId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarTransmissions_TransmissionID",
                table: "Cars",
                column: "TransmissionID",
                principalTable: "CarTransmissions",
                principalColumn: "TransmissionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
