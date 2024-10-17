using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCarListingModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarBrand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBrand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarFuelType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuelName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarFuelType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarTransmission",
                columns: table => new
                {
                    TransmissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransmissionAbbreviation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTransmission", x => x.TransmissionId);
                });

            migrationBuilder.CreateTable(
                name: "CarModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarModel_CarBrand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "CarBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    Whp = table.Column<int>(type: "int", nullable: false),
                    FuelId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    TransmissionID = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Car_AspNetUsers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_CarCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CarCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_CarFuelType_FuelId",
                        column: x => x.FuelId,
                        principalTable: "CarFuelType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_CarLocation_LocationId",
                        column: x => x.LocationId,
                        principalTable: "CarLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_CarModel_ModelId",
                        column: x => x.ModelId,
                        principalTable: "CarModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_CarTransmission_TransmissionID",
                        column: x => x.TransmissionID,
                        principalTable: "CarTransmission",
                        principalColumn: "TransmissionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarListing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarListing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarListing_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CarListing_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CarListingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarImage_CarListing_CarListingId",
                        column: x => x.CarListingId,
                        principalTable: "CarListing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favourite",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarListingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourite", x => new { x.UserId, x.CarListingId });
                    table.ForeignKey(
                        name: "FK_Favourite_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Favourite_CarListing_CarListingId",
                        column: x => x.CarListingId,
                        principalTable: "CarListing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_CategoryId",
                table: "Car",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_FuelId",
                table: "Car",
                column: "FuelId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_LocationId",
                table: "Car",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_ModelId",
                table: "Car",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_SellerId",
                table: "Car",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_TransmissionID",
                table: "Car",
                column: "TransmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_CarImage_CarListingId",
                table: "CarImage",
                column: "CarListingId");

            migrationBuilder.CreateIndex(
                name: "IX_CarListing_ApplicationUserId",
                table: "CarListing",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CarListing_CarId",
                table: "CarListing",
                column: "CarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarModel_BrandId",
                table: "CarModel",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourite_CarListingId",
                table: "Favourite",
                column: "CarListingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarImage");

            migrationBuilder.DropTable(
                name: "Favourite");

            migrationBuilder.DropTable(
                name: "CarListing");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "CarCategory");

            migrationBuilder.DropTable(
                name: "CarFuelType");

            migrationBuilder.DropTable(
                name: "CarLocation");

            migrationBuilder.DropTable(
                name: "CarModel");

            migrationBuilder.DropTable(
                name: "CarTransmission");

            migrationBuilder.DropTable(
                name: "CarBrand");
        }
    }
}
