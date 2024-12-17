using CarApp.Core.Services;
using CarApp.Core.ViewModels.Admin.UserManagement;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
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
    public class AdminServiceTests
    {
        private Mock<UserManager<ApplicationUser>> userManagerMock;
        private Mock<RoleManager<IdentityRole>> roleManagerMock;
        private Mock<IRepository<CarBrand, int>> brandRepositoryMock;
        private Mock<IRepository<Report, int>> reportRepositoryMock;
        private Mock<IRepository<CarModel, int>> modelRepositoryMock;
        private Mock<IRepository<CarListing, int>> carListingRepositoryMock;
        private AdminService adminService;

        [SetUp]
        public void SetUp()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
            roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStoreMock.Object, null, null, null, null);

            brandRepositoryMock = new Mock<IRepository<CarBrand, int>>();
            reportRepositoryMock = new Mock<IRepository<Report, int>>();
            modelRepositoryMock = new Mock<IRepository<CarModel, int>>();
            carListingRepositoryMock = new Mock<IRepository<CarListing, int>>();

            adminService = new AdminService(
                userManagerMock.Object,
                roleManagerMock.Object,
                brandRepositoryMock.Object,
                modelRepositoryMock.Object,
                carListingRepositoryMock.Object,
                reportRepositoryMock.Object);
        }

        [Test]
        public async Task AssignUserToRoleAsyncSuccess()
        {
            var userId = "123";
            var roleName = "Admin";

            var user = new ApplicationUser { Id = userId };

            userManagerMock
                .Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync(user);

            roleManagerMock
                .Setup(rm => rm.RoleExistsAsync(roleName))
                .ReturnsAsync(true);

            userManagerMock
                .Setup(um => um.IsInRoleAsync(user, roleName))
                .ReturnsAsync(false);

            userManagerMock
                .Setup(um => um.AddToRoleAsync(user, roleName))
                .ReturnsAsync(IdentityResult.Success);

            var result = await adminService.AssignUserToRoleAsync(userId, roleName);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task AssignUserToRoleAsyncUserDoesNotExist()
        {
            var userId = "123";
            var roleName = "Admin";

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser)null);

            var result = await adminService.AssignUserToRoleAsync(userId, roleName);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeleteUserAsynSuccess()
        {
            var userId = "123";
            var user = new ApplicationUser { Id = userId };
            var reports = new List<Report> { new Report { SellerId = userId }, new Report { SellerId = userId } };

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
            reportRepositoryMock.Setup(r => r.GetAllAttached()).Returns(reports.AsQueryable().BuildMock());
            userManagerMock.Setup(um => um.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

            var result = await adminService.DeleteUserAsync(new DeleteUserViewModel { UserId = userId });

            Assert.That(result, Is.True);
            reportRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Report>()), Times.Exactly(reports.Count));
        }

        [Test]
        public async Task DeleteUserAsyncUserDoesNotExist()
        {
            var userId = "123";

            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser)null);

            var result = await adminService.DeleteUserAsync(new DeleteUserViewModel { UserId = userId });

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task EditBrandNameAsyncSuccess()
        {
            var brandId = 1;
            var newBrandName = "Nissan";
            var brand = new CarBrand { Id = brandId, BrandName = "Nissane" };

            brandRepositoryMock.Setup(br => br.GetAllAttached())
                .Returns(new List<CarBrand> { brand }.AsQueryable().BuildMock());
            brandRepositoryMock.Setup(br => br.UpdateAsync(brand)).ReturnsAsync(true);

            var result = await adminService.EditBrandNameAsync(brandId, newBrandName);

            Assert.That(result, Is.True);
            Assert.That(brand.BrandName, Is.EqualTo(newBrandName));
        }

        [Test]
        public async Task EditBrandNameAsyncFalseBrand()
        {
            var brandId = 1;
            var newBrandName = "Nissan";

            brandRepositoryMock.Setup(br => br.GetAllAttached())
                .Returns(new List<CarBrand>().AsQueryable().BuildMock());

            var result = await adminService.EditBrandNameAsync(brandId, newBrandName);

            Assert.That(result, Is.False);
        }
    }
}
