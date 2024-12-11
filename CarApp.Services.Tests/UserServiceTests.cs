using CarApp.Core.Enumerations;
using CarApp.Core.Services;
using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic.FileIO;
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
        private IList<CarListing> carListingsData;
        private IList<ApplicationUser> usersData;

        private Mock<IRepository<CarListing, int>> carListingRepository;
        private Mock<IRepository<Car, int>> carRepository;
        private Mock<UserManager<ApplicationUser>> userManager;

        [SetUp]
        public void Setup()
        {
            carListingsData = new List<CarListing>
            {
                new CarListing
        {
            Id = 1,
            SellerId = "user1",
            Description = "Test Car 1",
            Price = 10000,
            IsDeleted = false,
            Car = new Car
            {
                Mileage = 50000,
                Whp = 200,
                Model = new CarModel
                {
                    ModelName = "Mustang",
                    CarBrand = new CarBrand { BrandName = "Ford" }
                },
                Fuel = new CarFuelType { FuelName = "Gasoline" },
                Gear = new CarGear { GearName = "Manual" },
                CarBodyType = new CarBodyType { Name = "Sedan" },
                EngineDisplacement = 2000
            },
            City = new CarLocationCity { CityName = "Plovdiv", 
                CarLocationRegion = new CarLocationRegion { RegionName = "Plovdiv" } },
            CarImages = new List<CarImage>
            {
                new CarImage { Id = 1, ImageUrl = "image1.jpg", Order = 0 },
                new CarImage { Id = 2, ImageUrl = "image2.jpg", Order = 1 }
            }
        },
                new CarListing
        {
            Id = 2,
            SellerId = "user2",
            Description = "Test Car 2",
            Price = 20000,
            IsDeleted = false,
            Car = new Car
            {
                Mileage = 30000,
                Whp = 300,
                Model = new CarModel
                {
                    ModelName = "370 Z",
                    CarBrand = new CarBrand { BrandName = "Nissan" }
                },
                Fuel = new CarFuelType { FuelName = "Gasoline" },
                Gear = new CarGear { GearName = "Automatic" },
                CarBodyType = new CarBodyType { Name = "Coupe" },
                EngineDisplacement = 3700
            },
            City = new CarLocationCity { CityName = "Sofia", 
                CarLocationRegion = new CarLocationRegion { RegionName = "Sofia" } },
            CarImages = new List<CarImage>
            {
                new CarImage { Id = 3, ImageUrl = "image3.jpg", Order = 0 }
            }
        }
            };

            usersData = new List<ApplicationUser>
            {
            new ApplicationUser { Id = "user1", FirstName = "Timmy", LastName = "Turner", Email="timmyturner@abv.bg" },
            new ApplicationUser { Id = "user2", FirstName = "Jim", LastName = "Carrey", Email="j.carrey@abv.bg" }
            };

            carListingRepository = new Mock<IRepository<CarListing, int>>();
            carRepository = new Mock<IRepository<Car, int>>();

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            userManager = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
        }

        [Test]
        public async Task DeleteCarListingAsyncReturnsTrue()
        {
            var userId = "user1";
            var model = new CarListingDeleteViewModel { Id = 1 };

            carListingRepository
                .Setup(r => r.GetAllAttached())
                .Returns(carListingsData.AsQueryable().BuildMock());

            userManager
                .Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync(usersData.First(u => u.Id == userId));

            userManager
                .Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<string>());

            var userService = new UserService(carListingRepository.Object, carRepository.Object, userManager.Object);
            
            var result = await userService.DeleteCarListingAsync(model, userId);

            Assert.That(result, Is.True);
            Assert.That(carListingsData.First(cl => cl.Id == model.Id).IsDeleted, Is.True);
        }
        [Test]
        public async Task DeleteCarListingAsyncReturnsFalse()
        {
            var userId = "user2";
            var model = new CarListingDeleteViewModel { Id = 1 };

            carListingRepository
                .Setup(r => r.GetAllAttached())
                .Returns(carListingsData.AsQueryable().BuildMock());

            userManager
                .Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync(usersData.First(u => u.Id == userId));

            userManager
                .Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<string>());

            var userService = new UserService(carListingRepository.Object, carRepository.Object, userManager.Object);

            var result = await userService.DeleteCarListingAsync(model, userId);

            Assert.That(result, Is.False);
        }
        [Test]
        public async Task DeleteCarListingAsyncDeletesSuccessfully()
        {
            var userId = "validUserId";
            var carListingId = 1;

            var carListing = new CarListing { Id = carListingId, SellerId = userId, IsDeleted = false };
            var user = new ApplicationUser { Id = userId };

            userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
            userManager.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(new List<string>());

            carListingRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<CarListing> { carListing }.AsQueryable().BuildMock());

            carRepository.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);
            
            var userService = new UserService(carListingRepository.Object, carRepository.Object, userManager.Object);


            var result = await userService.DeleteCarListingAsync(new CarListingDeleteViewModel { Id = carListingId }, userId);

            Assert.True(result);
            Assert.True(carListing.IsDeleted);
            carRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
        [Test]
        public async Task EditCarListingAsyncUpdatesSuccessfully()
        {
            var userId = "user1";
            var carListingId = 1;

            var car = new Car { CarListing = new CarListing { Id = carListingId, SellerId = "user1" } };
            var carListing = new CarListing
            {
                Id = carListingId,
                SellerId = userId,
                IsDeleted = false,
                CarImages = new List<CarImage>()
            };

            carRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Car> { car }.AsQueryable().BuildMock());

            carListingRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<CarListing> { carListing }.AsQueryable().BuildMock());

            carListingRepository.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

            var model = new CarListingEditViewModel
            {
                Id = carListingId,
                Description = "Updated description",
                Price = 12345,
                Milleage = 5000,
                Whp = 200,
                CarImages = new List<CarImageViewModel>()
            };

            var userService = new UserService(carListingRepository.Object, carRepository.Object, userManager.Object);

            var result = await userService.EditCarListingAsync(model, userId);

            Assert.True(result);
            Assert.That(carListing.Description, Is.EqualTo("Updated description"));
            Assert.That(carListing.Price, Is.EqualTo(12345));
            Assert.That(car.Whp, Is.EqualTo(200));
            carListingRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task GetAllUserCarListingsAsyncSuccessfully()
        {
            var userId = "user2";

            IQueryable<CarListing> carListingsMockQueryable = carListingsData.AsQueryable().BuildMock();

            carListingRepository.Setup(repo => repo.GetAllAttached())
                .Returns(carListingsMockQueryable.BuildMock());
           
            var userService = new UserService(carListingRepository.Object, carRepository.Object, userManager.Object);

            var result = await userService.GetAllUserCarListingsAsync(userId);
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(1));
                Assert.That(result.First().Id, Is.EqualTo(2));
            });
        }

        [Test]
        public async Task GetCarListingForDeleteAsyncReturnsModel()
        {
            var userId = "user1";
            var carListingId = 1;

            var carListing = new CarListing
            {
                Id = carListingId,
                SellerId = userId,
                IsDeleted = false,
                Car = new Car { 
                    Model = new CarModel { 
                    CarBrand = new CarBrand { 
                        BrandName = "Brand" 
                    }, 
                    ModelName = "Model" } 
                },
                Seller = new ApplicationUser
                {
                    Email = "user1@abv.bg",
                    FirstName = "User",
                    LastName = "Userov"
                },
                CarImages = new List<CarImage>
                {
                    new CarImage{ ImageUrl="image.png"}
                }
            };

            carListingRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<CarListing> { carListing }.AsQueryable().BuildMock());

            userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(new ApplicationUser { Id = userId });
            userManager.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(new List<string>());

            var userService = new UserService(carListingRepository.Object, carRepository.Object, userManager.Object);

            var result = await userService.GetCarListingForDeleteAsync(carListingId, userId);

            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(carListingId));
        }

        [Test]
        public async Task GetCarListingForEditAsyncReturnsModel()
        {
            var carListingId = 1;

            var carListing = new CarListing
            {
                Id = carListingId,
                SellerId = "seller1",
                IsDeleted = false,
                Car = new Car { Whp = 200, Mileage = 5000 },
                Description = "Description",
                Price = 12345,
                CarImages = new List<CarImage> { new CarImage { Id = 1, ImageUrl = "url", Order = 0 } }
            };

            carListingRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<CarListing> { carListing }.AsQueryable().BuildMock());
            
            var userService = new UserService(carListingRepository.Object, carRepository.Object, userManager.Object);

            var result = await userService.GetCarListingForEditAsync(carListingId);

            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(carListingId));
            Assert.That(result.Description, Is.EqualTo("Description"));
        }
    }
}
