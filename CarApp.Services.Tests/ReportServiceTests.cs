using CarApp.Core.Services;
using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;
using CarApp.Infrastructure.Constants.Enum;
using CarApp.Infrastructure.Data;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using MockQueryable;
using MockQueryable.Moq;
using Moq;

namespace CarApp.Services.Tests
{
    [TestFixture]
    public class AddReportAsyncTests
    {
        private Mock<IRepository<CarListing, int>> carListingRepositoryMock;
        private Mock<IRepository<Report, int>> reportRepositoryMock;
        private ReportService reportService;

        [SetUp]
        public void SetUp()
        {
            carListingRepositoryMock = new Mock<IRepository<CarListing, int>>();
            reportRepositoryMock = new Mock<IRepository<Report, int>>();
            reportService = new ReportService(carListingRepositoryMock.Object, reportRepositoryMock.Object);
        }

        [Test]
        public async Task AddReportAsyncValidInput()
        {
            var model = new ReportListingViewModel
            {
                ListingId = 1,
                SellerId = "seller123",
                SelectedReason = "Spam",
                Comment = "This is spam."
            };
            var userId = "user123";

            reportRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Report>()))
                .Returns(Task.CompletedTask);

            await reportService.AddReportAsync(model, userId);

            reportRepositoryMock.Verify(r => r.AddAsync(It.Is<Report>(
                report => report.ListingId == model.ListingId &&
                          report.ReporterId == userId &&
                          report.SellerId == model.SellerId &&
                          report.ReportReason == ReportReason.Spam &&
                          report.Comment == model.Comment)), Times.Once);

        }
        [Test]
        public async Task ApproveCarListingAsyncValidId()
        {
            int carListingId = 1;
            var mockReports = new List<Report>
            {
                new Report { ListingId = carListingId, ReporterId = "user1", SellerId = "seller1" },
                new Report { ListingId = carListingId, ReporterId = "user2", SellerId = "seller1" }
            };

            reportRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(mockReports.AsQueryable().BuildMock());

            reportRepositoryMock
                .Setup(r => r.DeleteAsync(It.IsAny<Report>()))
                .Returns(Task.FromResult(true));

            var reportService = new ReportService(carListingRepositoryMock.Object, reportRepositoryMock.Object);

            var result = await reportService.ApproveCarListingAsync(carListingId);

            Assert.That(result, Is.True);

            reportRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Report>()), Times.Exactly(mockReports.Count));
        }

        [Test]
        public async Task GetAllReportedCarListingsAsync()
        {
            var reports = new List<Report>
        {
            new Report
            {
                ListingId = 1,
                ReportReason = ReportReason.Spam,
                Comment = "Spam Comment",
                Reporter = new ApplicationUser { UserName = "reporter1" },
                ReportedAt = DateTime.Now,
                CarListing = new CarListing
                {
                    SellerId = "sellerId",
                    CarImages = new List<CarImage> { new CarImage { ImageUrl = "image1.jpg" } },
                    IsDeleted = false
                }
            }
        };

            reportRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(reports.AsQueryable().BuildMock());

            var result = await reportService.GetAllReportedCarListingsAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
            var firstReport = result.First();
            Assert.Multiple(() =>
            {
                Assert.That(firstReport.CarListingId, Is.EqualTo(1));
                Assert.That(firstReport.ReportReason, Has.Member(ReportReason.Spam));
                Assert.That(firstReport.Comment, Has.Member("Spam Comment"));
                Assert.That(firstReport.CommentAuthors, Has.Member("reporter1"));
            });
        }

        [Test]
        public async Task GetCarListingForReportAsyncValidId()
        {
            int carListingId = 1;
            var carListings = new List<CarListing>
        {
            new CarListing
            {
                Id = carListingId,
                SellerId = "sellerId",
                Seller = new ApplicationUser 
                { 
                    FirstName = "Tom", 
                    LastName = "Cruz", 
                    Email = "tomccruz@gmail.com" 
                },
                Car = new Car
                {
                    Model = new CarModel
                    {
                        ModelName = "370 Z",
                        CarBrand = new CarBrand { BrandName = "Nissan" }
                    },
                    Trim = "Base"
                },
                IsDeleted = false
            }
        };

            carListingRepositoryMock
                .Setup(cl => cl.GetAllAttached())
                .Returns(carListings.AsQueryable().BuildMock());

            var result = await reportService.GetCarListingForReportAsync(carListingId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ListingId, Is.EqualTo(carListingId));
            Assert.That(result.SellerId, Is.EqualTo("sellerId"));
            Assert.That(result.SellerFullName, Is.EqualTo("Tom Cruz"));
            Assert.That(result.SellerEmail, Is.EqualTo("tomccruz@gmail.com"));
            Assert.That(result.CarListingTitle, Is.EqualTo("Nissan 370 Z Base"));
        }
    }
}
