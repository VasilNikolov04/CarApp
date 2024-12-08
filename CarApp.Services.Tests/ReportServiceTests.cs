using CarApp.Core.Services;
using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;
using CarApp.Infrastructure.Constants.Enum;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using MockQueryable;
using Moq;

namespace CarApp.Services.Tests
{
    [TestFixture]
    public class AddReportAsyncTests
    {
        private IList<CarListing> carListingsData;

        private Mock<IRepository<CarListing, int>> carListingRepository;
        private Mock<IRepository<Car, int>> carRepository;
        private Mock<IRepository<Report, int>> reportRepository;

        [SetUp]
        public void Setup()
        {
            carRepository = new Mock<IRepository<Car, int>>();
            carListingRepository = new Mock<IRepository<CarListing, int>>();
            reportRepository = new Mock<IRepository<Report, int>>();

            carListingsData = new List<CarListing>
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
                    Trim = "Base",
                    EngineDisplacement = 2000,
                    Whp = 296,
                    Mileage = 15000,
                    Year = 2021,
                    Fuel = new CarFuelType { FuelName = "Electric" },
                    Gear = new CarGear { GearName = "Automatic" }
                },
                Description = "Great car",
                Price = 22000,
                IsDeleted = false,
                CarImages = new List<CarImage>
                {
                    new CarImage { Order = 0, ImageUrl = "url1" },
                    new CarImage { Order = 1, ImageUrl = "url2" }
                },
                City = new CarLocationCity
                {
                    CityName = "Los Angeles",
                    CarLocationRegion = new CarLocationRegion { RegionName = "West Coast" }
                },
                DatePosted = DateTime.UtcNow,
                SellerId = "sellerId",
                Seller = new ApplicationUser
                {
                    FirstName = "Timmy",
                    LastName = "Turner",
                    Email = "timmy@abv.bg",
                    PhoneNumber = "123456789"
                }
            },
            new CarListing
            {
                Id = 2,
                SellerId = "sellerId2",
                IsDeleted = true
            }
        };


        }

        //[Test]
        //public async Task AddReportAsync_ValidModelAndUserId_ShouldAddReport()
        //{
        //    // Arrange
        //    string userId = "user1";
        //    var model = new ReportListingViewModel
        //    {
        //        ListingId = 100,
        //        SellerId = "seller1",
        //        SelectedReason = "Spam",
        //        Comment = "This is a test report"
        //    };

        //    var reportsData = new List<Report>();

        //    var reportRepositoryMock = new Mock<IRepository<Report, int>>();

        //    IQueryable<Report> reportMockQueryable = reportsData.AsQueryable().BuildMock();

        //    this.carListingRepository
        //        .Setup(cl => cl.GetAllAttached())
        //        .Returns(carListingsMockQueryable);

        //    ICarListingService carListingService = new CarListingService(carListingRepository.Object, carRepository.Object);

        //    CarDetailsViewModel allCarListingsActual = await carListingService.CarListingDetails(listingId);


        //    reportRepositoryMock
        //        .Setup(r => r.AddAsync(It.IsAny<Report>()))
        //        .Callback((Report report) => reportsData.Add(report))
        //        .Returns(Task.CompletedTask);

        //    var service = new ReportingService(reportRepositoryMock.Object);

        //    await service.AddReportAsync(model, userId);

        //    Assert.That(reportsData.Count, Is.EqualTo(1));
        //    var addedReport = reportsData.First();
        //    Assert.Multiple(() =>
        //    {
        //        Assert.That(addedReport.ListingId, Is.EqualTo(model.ListingId));
        //        Assert.That(addedReport.ReporterId, Is.EqualTo(userId));
        //        Assert.That(addedReport.SellerId, Is.EqualTo(model.SellerId));
        //        Assert.That(addedReport.ReportReason, Is.EqualTo(ReportReason.Spam));
        //        Assert.That(addedReport.Comment, Is.EqualTo(model.Comment));
        //        Assert.That(addedReport.ReportedAt, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(1))); // Allow slight time difference
        //    });
        //}
    }
}
