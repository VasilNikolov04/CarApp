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
    [Migration("20241023194311_SeedBodyTypes")]
    partial class SeedBodyTypes
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
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
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

                    b.Property<int>("FuelId")
                        .HasColumnType("int");

                    b.Property<int>("GearId")
                        .HasColumnType("int");

                    b.Property<int>("Mileage")
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandName = "Seat"
                        },
                        new
                        {
                            Id = 2,
                            BrandName = "Renault"
                        },
                        new
                        {
                            Id = 3,
                            BrandName = "Peugeot"
                        },
                        new
                        {
                            Id = 4,
                            BrandName = "Dacia"
                        },
                        new
                        {
                            Id = 5,
                            BrandName = "Citroën"
                        },
                        new
                        {
                            Id = 6,
                            BrandName = "Opel"
                        },
                        new
                        {
                            Id = 7,
                            BrandName = "Alfa Romeo"
                        },
                        new
                        {
                            Id = 8,
                            BrandName = "Škoda"
                        },
                        new
                        {
                            Id = 9,
                            BrandName = "Chevrolet"
                        },
                        new
                        {
                            Id = 10,
                            BrandName = "Porsche"
                        },
                        new
                        {
                            Id = 11,
                            BrandName = "Honda"
                        },
                        new
                        {
                            Id = 12,
                            BrandName = "Subaru"
                        },
                        new
                        {
                            Id = 13,
                            BrandName = "Mazda"
                        },
                        new
                        {
                            Id = 14,
                            BrandName = "Mitsubishi"
                        },
                        new
                        {
                            Id = 15,
                            BrandName = "Lexus"
                        },
                        new
                        {
                            Id = 16,
                            BrandName = "Toyota"
                        },
                        new
                        {
                            Id = 17,
                            BrandName = "BMW"
                        },
                        new
                        {
                            Id = 18,
                            BrandName = "Volkswagen"
                        },
                        new
                        {
                            Id = 19,
                            BrandName = "Suzuki"
                        },
                        new
                        {
                            Id = 20,
                            BrandName = "Mercedes-Benz"
                        },
                        new
                        {
                            Id = 21,
                            BrandName = "Saab"
                        },
                        new
                        {
                            Id = 22,
                            BrandName = "Audi"
                        },
                        new
                        {
                            Id = 23,
                            BrandName = "Kia"
                        },
                        new
                        {
                            Id = 24,
                            BrandName = "Land Rover"
                        },
                        new
                        {
                            Id = 25,
                            BrandName = "Dodge"
                        },
                        new
                        {
                            Id = 26,
                            BrandName = "Chrysler"
                        },
                        new
                        {
                            Id = 27,
                            BrandName = "Ford"
                        },
                        new
                        {
                            Id = 28,
                            BrandName = "Hummer"
                        },
                        new
                        {
                            Id = 29,
                            BrandName = "Hyundai"
                        },
                        new
                        {
                            Id = 30,
                            BrandName = "Infiniti"
                        },
                        new
                        {
                            Id = 31,
                            BrandName = "Jaguar"
                        },
                        new
                        {
                            Id = 32,
                            BrandName = "Jeep"
                        },
                        new
                        {
                            Id = 33,
                            BrandName = "Nissan"
                        },
                        new
                        {
                            Id = 34,
                            BrandName = "Volvo"
                        },
                        new
                        {
                            Id = 35,
                            BrandName = "Daewoo"
                        },
                        new
                        {
                            Id = 36,
                            BrandName = "Fiat"
                        },
                        new
                        {
                            Id = 37,
                            BrandName = "MINI"
                        },
                        new
                        {
                            Id = 38,
                            BrandName = "Rover"
                        },
                        new
                        {
                            Id = 39,
                            BrandName = "Smart"
                        });
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarDrivetrain", b =>
                {
                    b.Property<int>("DrivetrainId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DrivetrainId"));

                    b.Property<string>("DrivetrainName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("DrivetrainId");

                    b.ToTable("CarDrivetrains");

                    b.HasData(
                        new
                        {
                            DrivetrainId = 1,
                            DrivetrainName = "Rear-Wheel Drive (RWD)"
                        },
                        new
                        {
                            DrivetrainId = 2,
                            DrivetrainName = "Front-Wheel Drive (FWD)"
                        },
                        new
                        {
                            DrivetrainId = 3,
                            DrivetrainName = "All-Wheel Drive (AWD)"
                        },
                        new
                        {
                            DrivetrainId = 4,
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

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

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

                    b.HasIndex("SellerId");

                    b.ToTable("CarListings");
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Region")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("CarLocations");
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

                    b.HasOne("CarApp.Infrastructure.Data.Models.ApplicationUser", "Seller")
                        .WithMany("CarListings")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Seller");
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarListing");

                    b.Navigation("User");
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
                });

            modelBuilder.Entity("CarApp.Infrastructure.Data.Models.CarModel", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
