using CarApp.Core.Services;
using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class UserServiceTests
    {
        private Mock<IRepository<CarListing, int>> carListingRepositoryMock;
        private Mock<IRepository<Car, int>> carRepositoryMock;
        private Mock<UserManager<ApplicationUser>> userManagerMock;
        private UserService userService;

        [SetUp]
        public void Setup()
        {
            carListingRepositoryMock = new Mock<IRepository<CarListing, int>>();
            carRepositoryMock = new Mock<IRepository<Car, int>>();
            userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(),
                null, null, null, null, null, null, null, null
            );
            userService = new UserService(carListingRepositoryMock.Object, carRepositoryMock.Object, userManagerMock.Object);
        }

        [Test]
        public async Task DeleteCarListingAsync_ValidUser_CarListingDeleted()
        {
            string userId = "user1";
            var carListing = new CarListing
            {
                Id = 1,
                SellerId = userId,
                IsDeleted = false
            };
            carListingRepositoryMock
                .Setup(repo => repo.GetAllAttached())
                .Returns(new List<CarListing> { carListing }.AsQueryable().BuildMock());

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(new ApplicationUser { Id = userId });
            userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(new List<string>());

            var result = await userService.DeleteCarListingAsync(new CarListingDeleteViewModel { Id = 1 }, userId);

            Assert.IsTrue(result);
            Assert.IsTrue(carListing.IsDeleted);
        }

        [Test]
        public async Task DeleteCarListingAsync_UserIsNotSeller_ReturnsFalse()
        {
            string userId = "user2";
            var carListing = new CarListing
            {
                Id = 1,
                SellerId = "user1", 
                IsDeleted = false
            };
            carListingRepositoryMock
                .Setup(repo => repo.GetAllAttached())
                .Returns(new List<CarListing> { carListing }.AsQueryable().BuildMock());

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(new ApplicationUser { Id = userId });
            userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(new List<string>());

            var result = await userService.DeleteCarListingAsync(new CarListingDeleteViewModel { Id = 1 }, userId);

            Assert.IsFalse(result); 
        }
        [Test]
        public async Task EditCarListingAsync_ValidUser_ListingEdited()
        {
            string userId = "user1";

            var carListing = new CarListing
            {
                Id = 1,
                SellerId = userId,
                IsDeleted = false,
                Car = new Car
                {
                    Whp = 200,
                    Mileage = 15000,
                    Model = new CarModel
                    {
                        CarBrand = new CarBrand { BrandName = "Toyota" },
                        ModelName = "Corolla"
                    }
                },
                CarImages = new List<CarImage>
        {
            new CarImage { Id = 1, ImageUrl = "oldImage.jpg", Order = 0 }
        }
            };

            var model = new CarListingEditViewModel
            {
                Id = 1,
                Description = "Updated description",
                Whp = 250,
                Milleage = 16000,
                Price = 18000,
                CarImages = new List<CarImageViewModel>
        {
            new CarImageViewModel { Id = 1, CarListingId=1, ImageUrl = "oldImage.jpg", Order = 0 }
        },
                NewCarImages = new List<IFormFile>() 
            };

            carListingRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new List<CarListing> { carListing }.AsQueryable().BuildMock());
            carRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Car> { carListing.Car }.AsQueryable().BuildMock());

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(new ApplicationUser { Id = userId });
            userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(new List<string>());

            var user = await userManagerMock.Object.FindByIdAsync(userId);
            Assert.IsNotNull(user, "User not found");

            var carListingFromRepo = carListingRepositoryMock.Object.GetAllAttached().FirstOrDefault();
            Assert.IsNotNull(carListingFromRepo, "CarListing not found");

            var result = await userService.EditCarListingAsync(model, userId);

            Assert.IsTrue(result, "The car listing should have been successfully edited.");

            Assert.That(carListing.Car.Whp, Is.EqualTo(250));
            Assert.That(carListing.Car.Mileage, Is.EqualTo(16000));
            Assert.That(carListing.Description, Is.EqualTo("Updated description"));
            Assert.That(carListing.Price, Is.EqualTo(18000));
        }
        [Test]
        public async Task GetAllUserCarListingsAsync_ValidUser_ReturnsCarListings()
        {
            string userId = "user1";
            var carListing = new CarListing
            {
                Id = 1,
                SellerId = userId,
                IsDeleted = false,
                Price = 15000,
                CarImages = new List<CarImage>
                {
                    new CarImage {Id = 1, Order = 0, ImageUrl = "url1" },
                    new CarImage {Id = 2, Order = 1, ImageUrl = "url2" }
                },
                Car = new Car
                {
                    Id = 2,
                    Model = new CarModel
                    {
                        ModelName = "370 Z",
                        CarBrand = new CarBrand { BrandName = "Nissan" }
                    },
                    Fuel = new CarFuelType { FuelName = "Gasoline" },
                    Gear = new CarGear { GearName = "Manual" },
                    Whp = 200,
                    EngineDisplacement = 2000,
                    CarBodyType = new CarBodyType { Name = "Coupe" },
                    Drivetrain = new CarDrivetrain { DrivetrainName = "Rear Wheel Drive" },
                    Mileage = 23423,
                    Trim = "Base",
                    Year = 2020,
                },
                DatePosted = DateTime.Now,
            };

            carListingRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new List<CarListing> { carListing }.AsQueryable().BuildMock());

            var result = await userService.GetAllUserCarListingsAsync(userId);

            Assert.IsNotEmpty(result);
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GetCarListingForDeleteAsync_ValidUser_ReturnsCarListing()
        {
            string userId = "user1";
            var carListing = new CarListing
            {
                Id = 1,
                SellerId = userId,
                IsDeleted = false,
                Car = new Car
                {
                    Model = new CarModel
                    {
                        ModelName = "Model1",
                        CarBrand = new CarBrand { BrandName = "Brand1" }
                    }
                }
            };

            carListingRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new List<CarListing> { carListing }.AsQueryable().BuildMock());

            var result = await userService.GetCarListingForDeleteAsync(1, userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(carListing.Id, result.Id);
        }

        [Test]
        public async Task GetCarListingForEditAsync_ValidListing_ReturnsCarListingForEdit()
        {
            var carListing = new CarListing
            {
                Id = 1,
                SellerId = "sellerId",
                Description = "Car description",
                IsDeleted = false,
                CarImages = new List<CarImage> { new CarImage { Id = 1, ImageUrl = "image.jpg", Order = 0 } }
            };

            carListingRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new List<CarListing> { carListing }.AsQueryable().BuildMock());

            var result = await userService.GetCarListingForEditAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(carListing.Description, result.Description);
        }
    }
}
