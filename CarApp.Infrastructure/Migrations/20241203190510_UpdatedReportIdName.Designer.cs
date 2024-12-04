﻿// <auto-generated />
using System;
using CarApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarApp.Infrastructure.Migrations
{
    [DbContext(typeof(CarDbContext))]
    [Migration("20241203190510_UpdatedReportIdName")]
    partial class UpdatedReportIdName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarBodyId")
                        .HasColumnType("int");

                    b.Property<int>("DrivetrainId")
                        .HasColumnType("int");

                    b.Property<int>("EngineDisplacement")
                        .HasColumnType("int");

                    b.Property<int>("FuelId")
                        .HasColumnType("int");

                    b.Property<int>("GearId")
                        .HasColumnType("int");

                    b.Property<int>("Mileage")
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("Trim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Whp")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarBodyId");

                    b.HasIndex("DrivetrainId");

                    b.HasIndex("FuelId");

                    b.HasIndex("GearId");

                    b.HasIndex("ModelId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarBodyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("CarBodyTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Convertible"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Coupe"
                        },
                        new
                        {
                            Id = 3,
                            Name = "SUV"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Sedan"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Van"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Hatchback"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Station Wagon"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Pickup Truck"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Compact"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("CarBrands");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarDrivetrain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DrivetrainName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("CarDrivetrains");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DrivetrainName = "Rear-Wheel Drive (RWD)"
                        },
                        new
                        {
                            Id = 2,
                            DrivetrainName = "Front-Wheel Drive (FWD)"
                        },
                        new
                        {
                            Id = 3,
                            DrivetrainName = "All-Wheel Drive (AWD)"
                        },
                        new
                        {
                            Id = 4,
                            DrivetrainName = "Four-Wheel Drive (4x4)"
                        });
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarFuelType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FuelName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("CarFuelTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FuelName = "Hybrid(Electric/Gasoline)"
                        },
                        new
                        {
                            Id = 2,
                            FuelName = "Hybrid(Electric/Diesel)"
                        },
                        new
                        {
                            Id = 3,
                            FuelName = "Gasoline"
                        },
                        new
                        {
                            Id = 4,
                            FuelName = "Diesel"
                        },
                        new
                        {
                            Id = 5,
                            FuelName = "Electric"
                        },
                        new
                        {
                            Id = 6,
                            FuelName = "Ethanol"
                        },
                        new
                        {
                            Id = 7,
                            FuelName = "Hydrogen"
                        },
                        new
                        {
                            Id = 8,
                            FuelName = "Other"
                        });
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarGear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("GearName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("CarGears");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GearName = "Manual"
                        },
                        new
                        {
                            Id = 2,
                            GearName = "Automatic"
                        },
                        new
                        {
                            Id = 3,
                            GearName = "Semi-Automatic"
                        });
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarListingId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CarListingId");

                    b.ToTable("CarImages");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarListing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("MainImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SellerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CarId")
                        .IsUnique();

                    b.HasIndex("CityId");

                    b.HasIndex("SellerId");

                    b.ToTable("CarListings");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("CarLocations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RegionName = "Out of Bulgaria"
                        },
                        new
                        {
                            Id = 2,
                            RegionName = "Blagoevgrad"
                        },
                        new
                        {
                            Id = 3,
                            RegionName = "Sofia"
                        },
                        new
                        {
                            Id = 4,
                            RegionName = "Plovdiv"
                        });
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarLocationCity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("CarLocationCities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityName = "USA",
                            LocationId = 1
                        },
                        new
                        {
                            Id = 2,
                            CityName = "Germany",
                            LocationId = 1
                        },
                        new
                        {
                            Id = 3,
                            CityName = "Italy",
                            LocationId = 1
                        },
                        new
                        {
                            Id = 4,
                            CityName = "Japan",
                            LocationId = 1
                        },
                        new
                        {
                            Id = 5,
                            CityName = "Blagoevgrad",
                            LocationId = 2
                        },
                        new
                        {
                            Id = 6,
                            CityName = "Bansko",
                            LocationId = 2
                        },
                        new
                        {
                            Id = 7,
                            CityName = "Sofia",
                            LocationId = 3
                        },
                        new
                        {
                            Id = 8,
                            CityName = "Botevgrad",
                            LocationId = 3
                        },
                        new
                        {
                            Id = 9,
                            CityName = "Plovdiv",
                            LocationId = 4
                        },
                        new
                        {
                            Id = 10,
                            CityName = "Asenovgrad",
                            LocationId = 4
                        },
                        new
                        {
                            Id = 11,
                            CityName = "UK",
                            LocationId = 1
                        });
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("CarModels");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.Favourite", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CarListingId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "CarListingId");

                    b.HasIndex("CarListingId");

                    b.ToTable("Favourites");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("ListingId")
                        .HasColumnType("int");

                    b.Property<int>("ReportReason")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReportedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReporterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SellerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ListingId");

                    b.HasIndex("ReporterId");

                    b.HasIndex("SellerId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.Car", b =>
                {
                    b.HasOne("CarApp.Infrastructure.Data.Models.CarBodyType", "CarBodyType")
                        .WithMany("Cars")
                        .HasForeignKey("CarBodyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarApp.Infrastructure.Data.Models.CarDrivetrain", "Drivetrain")
                        .WithMany("Cars")
                        .HasForeignKey("DrivetrainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarApp.Infrastructure.Data.Models.CarFuelType", "Fuel")
                        .WithMany("Cars")
                        .HasForeignKey("FuelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarApp.Infrastructure.Data.Models.CarGear", "Gear")
                        .WithMany("Cars")
                        .HasForeignKey("GearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarApp.Infrastructure.Data.Models.CarModel", "Model")
                        .WithMany("Cars")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarBodyType");

                    b.Navigation("Drivetrain");

                    b.Navigation("Fuel");

                    b.Navigation("Gear");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarImage", b =>
                {
                    b.HasOne("CarApp.Infrastructure.Data.Models.CarListing", "CarListing")
                        .WithMany("CarImages")
                        .HasForeignKey("CarListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarListing");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarListing", b =>
                {
                    b.HasOne("CarApp.Infrastructure.Data.Models.Car", "Car")
                        .WithOne("CarListing")
                        .HasForeignKey("CarApp.Infrastructure.Data.Models.CarListing", "CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarApp.Infrastructure.Data.Models.CarLocationCity", "City")
                        .WithMany("CarListings")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarApp.Infrastructure.Data.Models.ApplicationUser", "Seller")
                        .WithMany("CarListings")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("City");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarLocationCity", b =>
                {
                    b.HasOne("CarApp.Infrastructure.Data.Models.CarLocation", "CarLocation")
                        .WithMany("LocationCities")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarLocation");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarModel", b =>
                {
                    b.HasOne("CarApp.Infrastructure.Data.Models.CarBrand", "CarBrand")
                        .WithMany("CarModels")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarBrand");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.Favourite", b =>
                {
                    b.HasOne("CarApp.Infrastructure.Data.Models.CarListing", "CarListing")
                        .WithMany("Favourites")
                        .HasForeignKey("CarListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarApp.Infrastructure.Data.Models.ApplicationUser", "User")
                        .WithMany("Favourites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CarListing");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.Report", b =>
                {
                    b.HasOne("CarApp.Infrastructure.Data.Models.CarListing", "CarListing")
                        .WithMany("ReportedListings")
                        .HasForeignKey("ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarApp.Infrastructure.Data.Models.ApplicationUser", "Reporter")
                        .WithMany()
                        .HasForeignKey("ReporterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarApp.Infrastructure.Data.Models.ApplicationUser", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarListing");

                    b.Navigation("Reporter");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CarApp.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CarApp.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarApp.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CarApp.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("CarListings");

                    b.Navigation("Favourites");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.Car", b =>
                {
                    b.Navigation("CarListing")
                        .IsRequired();
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarBodyType", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarBrand", b =>
                {
                    b.Navigation("CarModels");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarDrivetrain", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarFuelType", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarGear", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarListing", b =>
                {
                    b.Navigation("CarImages");

                    b.Navigation("Favourites");

                    b.Navigation("ReportedListings");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarLocation", b =>
                {
                    b.Navigation("LocationCities");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarLocationCity", b =>
                {
                    b.Navigation("CarListings");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarModel", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
