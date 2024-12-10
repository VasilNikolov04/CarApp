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
        private readonly Mock<IRepository<CarListing, int>> carListingRepositoryMock;
        private readonly Mock<IRepository<Car, int>> carRepositoryMock;
        private readonly Mock<UserManager<ApplicationUser>> userManagerMock;
        private readonly UserService userService;

        public UserServiceTests()
        {
            carListingRepositoryMock = new Mock<IRepository<CarListing, int>>();
            carRepositoryMock = new Mock<IRepository<Car, int>>();
            userManagerMock = MockUserManager();

            userService = new UserService(carListingRepositoryMock.Object, carRepositoryMock.Object, userManagerMock.Object);
        }

        private Mock<UserManager<ApplicationUser>> MockUserManager()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
        }

        [Test]
        public async Task DeleteCarListingAsync_ValidListingAndUser_DeletesSuccessfully()
        {
            var userId = "validUserId";
            var carListingId = 1;

            var carListing = new CarListing { Id = carListingId, SellerId = userId, IsDeleted = false };
            var user = new ApplicationUser { Id = userId };

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
            userManagerMock.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(new List<string>());

            carListingRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new List<CarListing> { carListing }.AsQueryable().BuildMock());

            carRepositoryMock.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await userService.DeleteCarListingAsync(new CarListingDeleteViewModel { Id = carListingId }, userId);

            // Assert
            Assert.True(result);
            Assert.True(carListing.IsDeleted);
            carRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task EditCarListingAsync_ValidListing_UpdatesSuccessfully()
        {
            // Arrange
            var userId = "validUserId";
            var carListingId = 1;

            var car = new Car { CarListing = new CarListing { Id = carListingId, SellerId = "seller1" } };
            var carListing = new CarListing
            {
                Id = carListingId,
                SellerId = userId,
                IsDeleted = false,
                CarImages = new List<CarImage>()
            };

            carRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Car> { car }.AsQueryable().BuildMock());

            carListingRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new List<CarListing> { carListing }.AsQueryable());

            carListingRepositoryMock.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

            var model = new CarListingEditViewModel
            {
                Id = carListingId,
                Description = "Updated description",
                Price = 12345,
                Milleage = 5000,
                Whp = 200,
                CarImages = new List<CarImageViewModel>()
            };

            // Act
            var result = await userService.EditCarListingAsync(model, userId);

            // Assert
            Assert.True(result);
            Assert.That(carListing.Description, Is.EqualTo("Updated description"));
            Assert.That(carListing.Price, Is.EqualTo(12345));
            Assert.That(car.Whp, Is.EqualTo(200));
            carListingRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task GetAllUserCarListingsAsync_ReturnsCorrectListings()
        {
            var userId = "userId";

            var carListings = new List<CarListing>
            {
                new CarListing { Id = 1, SellerId = userId, IsDeleted = false },
                new CarListing { Id = 2, SellerId = userId, IsDeleted = true }
            };

            carListingRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(carListings.AsQueryable().BuildMock());

            var result = await userService.GetAllUserCarListingsAsync(userId);
            Assert.That(result.Count(),Is.EqualTo(1));
            Assert.That(result.First().Id, Is.EqualTo(1));
        }

        [Test]
        public async Task GetCarListingForDeleteAsync_ValidListing_ReturnsModel()
        {
            // Arrange
            var userId = "userId";
            var carListingId = 1;

            var carListing = new CarListing
            {
                Id = carListingId,
                SellerId = userId,
                IsDeleted = false,
                Car = new Car { Model = new CarModel { CarBrand = new CarBrand { BrandName = "Brand" }, ModelName = "Model" } }
            };

            carListingRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new List<CarListing> { carListing }.AsQueryable());

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(new ApplicationUser { Id = userId });
            userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(new List<string>());

            // Act
            var result = await userService.GetCarListingForDeleteAsync(carListingId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(carListingId));
        }

        [Test]
        public async Task GetCarListingForEditAsync_ValidListing_ReturnsModel()
        {
            // Arrange
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

            carListingRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new List<CarListing> { carListing }.AsQueryable());

            // Act
            var result = await userService.GetCarListingForEditAsync(carListingId);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(carListingId));
            Assert.That(result.Description, Is.EqualTo("Description"));
        }
    }
}
