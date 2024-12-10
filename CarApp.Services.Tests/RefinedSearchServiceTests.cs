using CarApp.Core.Enumerations;
using CarApp.Core.Services;
using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels.CarListing;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using MockQueryable;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Services.Tests
{
    [TestFixture]
    public class RefinedSearchServiceTests
    {
        private IList<CarListing> carListingsData = new List<CarListing>()
        {
            new CarListing
            {
                Id = 1,
                Car = new Car
                {
                    Model = new CarModel
                    {
                        ModelName = "370 Z",
                         CarBrand = new CarBrand { BrandName = "Nissan" }
                    },
                    CarBodyType = new CarBodyType { Name = "Coupe" },
                    Trim = "P300",
                    EngineDisplacement = 2000,
                    Whp = 296,
                    Mileage = 15000,
                    Year = 2021,
                    Fuel = new CarFuelType { FuelName = "Electric" },
                    Gear = new CarGear { GearName = "Automatic" },
                    Drivetrain = new CarDrivetrain { DrivetrainName = "FWD" },
                },
                Description = "I’m selling my 2020 Toyota Corolla XSE, a car I’ve owned for about a year. I’ve had such a good experience with this vehicle—it's been incredibly reliable, fuel-efficient, and smooth to drive, but I’ve recently upgraded to something a little larger to accommodate my growing family.\r\n\r\nThis Corolla is the XSE trim, which means it comes with a sportier appearance and all the tech you could ask for in a compact car. The 1.8L engine provides plenty of power for city driving, yet it’s still very fuel-efficient, making it perfect for daily commuting. The car has a sleek, modern design with LED headlights, a stunning black interior with leather-trimmed seats, and a user-friendly infotainment system with Apple CarPlay and Android Auto.\r\n\r\nI’ve kept the car in pristine condition and have regularly serviced it at Toyota dealerships. It’s never been involved in any accidents and is still under warranty. The car also comes with great features like lane assist, pre-collision system, and a rearview camera for added safety.\r\n\r\nI’m selling it only because I need a larger vehicle now that my family is expanding, but this Corolla has been a fantastic car that I’ve enjoyed driving. It's perfect for someone looking for an affordable, reliable, and safe vehicle.\r\n\r\nPrice is firm, no negotiations. I’ve priced it competitively for its age and condition.",
                Price = 22000,
                IsDeleted = false,
                CarImages = new List<CarImage>
                {
                    new CarImage { Order = 0, ImageUrl = "url1" },
                    new CarImage { Order = 1, ImageUrl = "url2" },
                },
                City = new CarLocationCity
                {
                    CityName = "Los Angeles",
                    CarLocationRegion = new CarLocationRegion { RegionName = "West Coast" }
                },
                DatePosted = new DateTime(2024, 12, 06),
                SellerId = "18a28b5b-5064-4fd9-ac36-b394b4e24e04"
            },
            new CarListing
            {
                Id = 2,
                Car = new Car
                {
                    Model = new CarModel
                    {
                        ModelName = "Mustang",
                         CarBrand = new CarBrand { BrandName = "Ford" }
                    },
                    CarBodyType = new CarBodyType { Name = "Coupe" },
                    Trim = "P300",
                    EngineDisplacement = 2000,
                    Whp = 296,
                    Mileage = 15000,
                    Year = 2021,
                    Fuel = new CarFuelType { FuelName = "Electric" },
                    Gear = new CarGear { GearName = "Manual" },
                    Drivetrain = new CarDrivetrain { DrivetrainName = "FWD" },
                },
                Description = "I’m putting up for sale my 2021 Honda Civic Sport Touring, a car that I’ve thoroughly enjoyed over the past year. I’ve had to make the tough decision to sell it due to an upcoming move, and I can’t take the car with me. It’s been a great daily driver, and it’s in excellent condition, having been kept in a garage and well-maintained.\r\n\r\nThis Civic is the Sport Touring trim, which means it comes with everything you’d want in a compact car. It features a 1.5L turbocharged engine that provides an exciting amount of power while still being incredibly fuel-efficient. The car is finished in a beautiful crystal black pearl exterior, and the interior is a comfortable mix of leather and high-quality materials.\r\n\r\nI’ve added a few custom touches, including a set of high-performance tires and a blacked-out grille that adds a sportier look to the car. The car comes with features like a large touchscreen display, Apple CarPlay, a premium sound system, and Honda Sensing, which includes adaptive cruise control, lane-keeping assist, and more.\r\n\r\nI’ve kept the car well-maintained with regular service, and it’s still under the original warranty. It’s an excellent choice for anyone looking for a reliable, fun-to-drive, and tech-packed compact car.\r\n\r\nI’m firm on the price—no negotiations. This is a well-priced, high-condition car, and I’m only selling it because of the move.",
                Price = 25000,
                IsDeleted = false,
                CarImages = new List<CarImage>
                {
                    new CarImage { Order = 0, ImageUrl = "url1" },
                    new CarImage { Order = 1, ImageUrl = "url2" },
                },
                City = new CarLocationCity
                {
                    CityName = "Los Angeles",
                    CarLocationRegion = new CarLocationRegion { RegionName = "West Coast" }
                },
                DatePosted = new DateTime(2024, 12, 06),
                SellerId = "18a28b5b-5064-4fd9-ac36-b394b4e24e04"
            },
            new CarListing
            {
                Id = 3,
                Car = new Car
                {
                    Model = new CarModel
                    {
                         ModelName = "Model Y",
                         CarBrand = new CarBrand { BrandName = "Tesla" }
                    },
                    CarBodyType = new CarBodyType { Name = "Coupe" },
                    Trim = "P300",
                    EngineDisplacement = 2000,
                    Whp = 296,
                    Mileage = 15000,
                    Year = 2021,
                    Fuel = new CarFuelType { FuelName = "Electric" },
                    Gear = new CarGear { GearName = "Manual" },
                    Drivetrain = new CarDrivetrain { DrivetrainName = "FWD" },
                },
                Description = "Selling my personal 2018 Ford Mustang GT. I’ve had this car for almost 3 years now, and it’s been a blast to drive, but it’s time for me to part ways. I’m moving to a more family-oriented car, and sadly, this beauty can’t be my daily driver anymore. The car has been well-maintained with full service history and has never been involved in any accidents.\r\n\r\nThis Mustang is the GT model with the iconic 5.0L V8 engine that packs a punch with 450 horsepower, making every drive exhilarating. I’ve installed a few aftermarket parts to make it even more fun to drive, including a cold air intake and a performance exhaust system that gives it that unmistakable muscle car growl. The suspension has been upgraded with coilovers to give it a more aggressive stance and better handling.\r\n\r\nThe interior is in excellent condition with leather seats, a modern infotainment system, and a premium sound system. It also has some great tech like parking sensors and a rearview camera. I added a set of custom alloy wheels and high-performance tires to complete the sporty look and feel.\r\n\r\nI’m selling this car because it’s just not practical for my current needs, but trust me, it’s a joy to drive. You’ll love the power, the sound, and the attention it gets everywhere you go!\r\n\r\nPrice is firm—no negotiations. This is a fair price for such a great car in excellent condition. Feel free to contact me if you’re serious.",
                Price = 35000,
                IsDeleted = false,
                CarImages = new List<CarImage>
                {
                    new CarImage { Order = 0, ImageUrl = "url1" },
                    new CarImage { Order = 1, ImageUrl = "url2" },
                },
                City = new CarLocationCity
                {
                    CityName = "Los Angeles",
                    CarLocationRegion = new CarLocationRegion { RegionName = "West Coast" }
                },
                DatePosted = new DateTime(2024, 12, 06),
                SellerId = "b453f312-6ef6-4e2c-a02b-3b5cb21066b5"
            }
        };

        private Mock<IRepository<CarListing, int>> carListingRepositoryMock;
        private Mock<IRepository<Car, int>> carRepositoryMock;
        private RefinedSearchService refinedSearchService;

        [SetUp]
        public void Setup()
        {
            carListingRepositoryMock = new Mock<IRepository<CarListing, int>>();
            carRepositoryMock = new Mock<IRepository<Car, int>>();
            refinedSearchService = new RefinedSearchService(carListingRepositoryMock.Object, carRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllCarListingsAsyncSuccess()
        {

            IQueryable<CarListing> carListingsMockQueryable = carListingsData.AsQueryable().BuildMock();

            carListingRepositoryMock
                .Setup(cl => cl.GetAllAsReadOnly())
                .Returns(carListingsMockQueryable);

            var result = await refinedSearchService.GetAllCarListingsAsync(null, null);

            Assert.That(result.CarListings.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllCarListingsAsyncFiltersByBrandSuccess()
        {
            IQueryable<CarListing> carListingsMockQueryable = carListingsData.AsQueryable().BuildMock();

            carListingRepositoryMock
                .Setup(cl => cl.GetAllAsReadOnly())
                .Returns(carListingsMockQueryable);

            var result = await refinedSearchService.GetAllCarListingsAsync("Nissan", null);

            Assert.That(result.CarListings.Count(), Is.EqualTo(1));
            Assert.That(result.CarListings.First().Brand, Is.EqualTo("Nissan"));
        }

        [Test]
        public async Task GetAllCarListingsAsyncSortingByPriceAscendingSuccess()
        {
            IQueryable<CarListing> carListingsMockQueryable = carListingsData.AsQueryable().BuildMock();

            carListingRepositoryMock
                .Setup(cl => cl.GetAllAsReadOnly())
                .Returns(carListingsMockQueryable);

            var result = await refinedSearchService
                .GetAllCarListingsAsync(null, null, sorting: CarListingSorting.PriceAscending);

            Assert.That(result.CarListings.First().Price, Is.EqualTo("22 000,00 €"));
        }

        [Test]
        public async Task GetAllCarListingsAsyncPagination()
        {
            IQueryable<CarListing> carListingsMockQueryable = carListingsData.AsQueryable().BuildMock();

            carListingRepositoryMock
                .Setup(cl => cl.GetAllAsReadOnly())
                .Returns(carListingsMockQueryable);

            var result = await refinedSearchService.GetAllCarListingsAsync(null, null, currentPage: 2, listingsPerPage: 1);

            Assert.That(result.CarListings.Count(), Is.EqualTo(1));
            Assert.That(result.CarListings.First().Id, Is.EqualTo(1));
        }
    }
}
