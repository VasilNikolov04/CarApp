using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedCarListingToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_AspNetUsers_SellerId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_CarListing_AspNetUsers_ApplicationUserId",
                table: "CarListing");

            migrationBuilder.DropIndex(
                name: "IX_CarListing_ApplicationUserId",
                table: "CarListing");

            migrationBuilder.DropIndex(
                name: "IX_Car_SellerId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "CarListing");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Car");

            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "CarListing",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CarListing_SellerId",
                table: "CarListing",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarListing_AspNetUsers_SellerId",
                table: "CarListing",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarListing_AspNetUsers_SellerId",
                table: "CarListing");

            migrationBuilder.DropIndex(
                name: "IX_CarListing_SellerId",
                table: "CarListing");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "CarListing");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "CarListing",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "Car",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CarListing_ApplicationUserId",
                table: "CarListing",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_SellerId",
                table: "Car",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_AspNetUsers_SellerId",
                table: "Car",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarListing_AspNetUsers_ApplicationUserId",
                table: "CarListing",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
