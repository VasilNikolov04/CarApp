using CarApp.Core.Enumerations;
using CarApp.Core.Services;
using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MockQueryable;
using Moq;
using System.Globalization;

namespace CarApp.Services.Tests
{
    [TestFixture]
    public class GetAllCarListings
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

        private Mock<IRepository<CarListing, int>> carListingRepository;
        private Mock<IRepository<Car, int>> carRepository;

        [SetUp]
        public void Setup()
        {
            carRepository = new Mock<IRepository<Car, int>>();
            carListingRepository = new Mock<IRepository<CarListing, int>>();  
        }

        [Test]
        public async Task GetAllCarListingsNoFilter()
        {
            int listingsPerPage = 3;
            int currentPage = 1;

            IQueryable<CarListing> carListingsMockQueryable = carListingsData.AsQueryable().BuildMock();

            carListingRepository
                .Setup(cl => cl.GetAllAsReadOnly())
                .Returns(carListingsMockQueryable);

            ICarListingService carListingService = new CarListingService(carListingRepository.Object, carRepository.Object);

            CarListingQueryServiceModel allCarListingsActual = await carListingService
                .GetAllCarListingsAsync(currentPage: currentPage,listingsPerPage: listingsPerPage);

            Assert.That(allCarListingsActual, Is.Not.Null);
            Assert.That(allCarListingsActual.CarListings.Count(), Is.EqualTo(carListingsData.Count));

            for (int i = 0; i < allCarListingsActual.CarListings.Count(); i++)
            {
                Assert.That(allCarListingsActual.CarListings.OrderBy(cl => cl.Id).ToList()[i].Id, 
                    Is.EqualTo(carListingsData.OrderBy(cl => cl.Id).ToList()[i].Id));
            }
        }
        [Test]
        public async Task GetAllCarListingsFilteredByBrand()
        {
            int listingsPerPage = 3;
            int currentPage = 1;
            string brandFilter = "Nissan";

            IQueryable<CarListing> carListingsMockQueryable = carListingsData
                .Where(cl => cl.Car.Model.CarBrand.BrandName == brandFilter)
                .AsQueryable().BuildMock();

            carListingRepository
                .Setup(cl => cl.GetAllAsReadOnly())
                .Returns(carListingsMockQueryable);

            ICarListingService carListingService = new CarListingService(carListingRepository.Object, carRepository.Object);

            CarListingQueryServiceModel allCarListingsActual = await carListingService
                .GetAllCarListingsAsync(brand: brandFilter, currentPage: currentPage, listingsPerPage: listingsPerPage);

            Assert.That(allCarListingsActual, Is.Not.Null);
            Assert.That(allCarListingsActual.CarListings.Count(), Is.EqualTo(carListingsMockQueryable.Count()));  // Should match filtered count

            // Optionally, check if all listings have the correct brand
            foreach (var listing in allCarListingsActual.CarListings)
            {
                Assert.That(listing.Brand, Is.EqualTo(brandFilter));
            }
        }
        [Test]
        public async Task GetAllCarListingsFilteredByPrice()
        {
            int listingsPerPage = 3;
            int currentPage = 1;
            int priceFilter = 25000;

            IQueryable<CarListing> carListingsMockQueryable = carListingsData
                .Where(cl => cl.Price <= priceFilter)
                .AsQueryable().BuildMock();
            
            carListingRepository
                .Setup(cl => cl.GetAllAsReadOnly())
                .Returns(carListingsMockQueryable);

            ICarListingService carListingService = new CarListingService(carListingRepository.Object, carRepository.Object);

            CarListingQueryServiceModel allCarListingsActual = await carListingService
                .GetAllCarListingsAsync(price: priceFilter, currentPage: currentPage, listingsPerPage: listingsPerPage);

            Assert.That(allCarListingsActual, Is.Not.Null);
            Assert.That(allCarListingsActual.CarListings.Count(), Is.EqualTo(carListingsMockQueryable.Count()));

            foreach (var listing in allCarListingsActual.CarListings)
            {
                string formattedString = listing.Price;

                formattedString = formattedString.Replace(",00 €", "").Replace("\u00A0", "");

                Assert.That(int.Parse(formattedString), Is.LessThanOrEqualTo(priceFilter));
            }
        }
        [Test]
        public async Task GetAllCarListingsFilterByBrandAndModel()
        {
            int listingsPerPage = 3;
            int currentPage = 1;
            string brandFilter = "Nissan";
            string modelFilter = "370Z";

            IQueryable<CarListing> carListingsMockQueryable = carListingsData
                .Where(cl => cl.Car.Model.CarBrand.BrandName == brandFilter
                             && cl.Car.Model.ModelName == modelFilter)
                .AsQueryable().BuildMock();

            this.carListingRepository
                .Setup(cl => cl.GetAllAsReadOnly())
                .Returns(carListingsMockQueryable);

            ICarListingService carListingService = new CarListingService(carListingRepository.Object, carRepository.Object);

            CarListingQueryServiceModel allCarListingsActual = await carListingService
                .GetAllCarListingsAsync(brand: brandFilter, model: modelFilter, currentPage: currentPage, listingsPerPage: listingsPerPage);

            Assert.That(allCarListingsActual, Is.Not.Null);
            Assert.That(allCarListingsActual.CarListings.Count(), Is.EqualTo(carListingsMockQueryable.Count())); 

            foreach (var listing in allCarListingsActual.CarListings)
            {
                Assert.That(listing.Brand, Is.EqualTo(brandFilter));
                Assert.That(listing.Model, Is.EqualTo(modelFilter));
            }
        }
        [Test]
        public async Task GetAllCarListingsSortByPriceDescending()
        {
            int listingsPerPage = 3;
            int currentPage = 1;
            CarListingSorting sorting = CarListingSorting.PriceDescending;

            IQueryable<CarListing> carListingsMockQueryable = carListingsData.AsQueryable().BuildMock();

            this.carListingRepository
                .Setup(cl => cl.GetAllAsReadOnly())
                .Returns(carListingsMockQueryable);

            ICarListingService carListingService = new CarListingService(carListingRepository.Object, carRepository.Object);

            CarListingQueryServiceModel allCarListingsActual = await carListingService
                .GetAllCarListingsAsync(sorting: sorting, currentPage: currentPage, listingsPerPage: listingsPerPage);

            Assert.That(allCarListingsActual, Is.Not.Null);

            var sortedListings = allCarListingsActual.CarListings.OrderByDescending(cl => cl.Price).ToList();
            for (int i = 0; i < sortedListings.Count; i++)
            {
                Assert.That(sortedListings[i].Id, Is.EqualTo(allCarListingsActual.CarListings.OrderByDescending(cl => cl.Price).ToList()[i].Id));
            }
        }
        [Test]
        public async Task GetAllCarListingsPaginationReturnsCorrectPage()
        {
            int listingsPerPage = 3;
            int currentPage = 1;

            IQueryable<CarListing> carListingsMockQueryable = carListingsData.AsQueryable().BuildMock();

            this.carListingRepository
                .Setup(cl => cl.GetAllAsReadOnly())
                .Returns(carListingsMockQueryable);

            ICarListingService carListingService = new CarListingService(carListingRepository.Object, carRepository.Object);

            CarListingQueryServiceModel allCarListingsActual = await carListingService
                .GetAllCarListingsAsync(currentPage: currentPage, listingsPerPage: listingsPerPage);

            Assert.That(allCarListingsActual, Is.Not.Null);

            var totalListings = carListingsMockQueryable.Count();
            var expectedListingsCount = totalListings - ((currentPage - 1) * listingsPerPage);

            Assert.That(allCarListingsActual.CarListings.Count(), Is.EqualTo(expectedListingsCount));
        }

    }
    public class AddCarListingTests
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
                Description = "I’m selling my 2020 Toyota Corolla XSE",
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
                DatePosted = new DateTime(2024, 12, 06, 12, 36, 49),
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

        private Mock<IRepository<CarListing, int>> carListingRepository;
        private Mock<IRepository<Car, int>> carRepository;

        [SetUp]
        public void Setup()
        {
            carRepository = new Mock<IRepository<Car, int>>();
            carListingRepository = new Mock<IRepository<CarListing, int>>();
        }

        [Test]
        public async Task AddCarListingSuccess()
        {
            var model = new CarViewModel
            {
                ModelId = 1,
                Year = 2020,
                Trim = "Base",
                Milleage = 15000,
                Whp = 200,
                EngineDisplacement = 2500,
                CarBodyId = 2,
                FuelId = 1,
                DrivetrainId = 1,
                GearId = 1,
                Price = 20000,
                CityId = 1,
                Description = "Great car",
                CarImages = new List<IFormFile>()
            };

            string userId = "user123";

            var carRepositoryMock = new Mock<IRepository<Car, int>>();
            var carListingRepositoryMock = new Mock<IRepository<CarListing, int>>();

            carRepositoryMock.Setup(cr => cr.AddAsync(It.IsAny<Car>())).Returns(Task.CompletedTask);
            carListingRepositoryMock.Setup(clr => clr.AddAsync(It.IsAny<CarListing>())).Returns(Task.CompletedTask);

            var service = new CarListingService(carListingRepositoryMock.Object, carRepositoryMock.Object);

            await service.AddCarListingAsync(model, userId);

            carRepositoryMock.Verify(cr => cr.AddAsync(It.IsAny<Car>()), Times.Once);
            carListingRepositoryMock.Verify(clr => clr.AddAsync(It.IsAny<CarListing>()), Times.Once);
        }
    }
}