using CarApp.Core.Enumerations;
using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels.CarListing;
using CarApp.Infrastructure.Constants.Enum;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly IRepository<CarListing, int> carListingRepository;
        private readonly IRepository<Report, int> reportRepository;
        private readonly IUtilityService utilityService;

        public ReportService(IRepository<CarListing, int> _carListingRepository, 
            IRepository<Report, int> _reportRepository, IUtilityService _utilityService)
        {
            carListingRepository = _carListingRepository;
            reportRepository = _reportRepository;
            utilityService = _utilityService;
        }

        public async Task AddReportAsync(ReportListingViewModel model, string userId)
        {
            var report = new Report
            {
                ListingId = model.ListingId,
                ReporterId = userId,
                SellerId = model.SellerId,
                ReportReason = (ReportReason)Enum.Parse(typeof(ReportReason), model.SelectedReason),
                Comment = model.Comment,
                ReportedAt = DateTime.Now
            };

            await reportRepository.AddAsync(report);
        }

        public async Task<bool> ApproveCarListingAsyn(int carListingId)
        {
            var reports = await reportRepository
                .GetAllAttached()
                .Where(r => r.ListingId == carListingId)
                .ToListAsync();

            foreach (var report in reports)
            {
                await reportRepository.DeleteAsync(report);
            }
            return true;
            
        }

        public async Task<IEnumerable<AllReportedListingViewModel>> GetAllReportedCarListingsAsync()
        {
            var reportedListings = await reportRepository
                .GetAllAttached()
                .Where(r => r.CarListing.IsDeleted == false)
                .Include(r => r.CarListing)
                .Include(r => r.Reporter)
                .ToListAsync();

            IEnumerable<AllReportedListingViewModel> models = reportedListings
                .GroupBy(r => r.ListingId)
                .Select(r => new AllReportedListingViewModel
                {
                    CarListingId = r.Key,
                    CarImage = r.First().CarListing.MainImageUrl,
                    ReportReason = r.Select(rr => rr.ReportReason)
                                .Distinct()
                                .ToList(),
                    Comment = r.Select(c => c.Comment)
                           .Where(c => !string.IsNullOrWhiteSpace(c))
                           .ToList(),
                    CommentAuthors = r.Select(rep => rep.Reporter.UserName)
                                .ToList(),
                    ReportedAt = r.Min(ra => ra.ReportedAt.ToString("dd/MM (HH:mm)")) ?? string.Empty
                })
                .OrderByDescending(r => r.ReportedAt)
                .ToList();   

            return models;
        }

        public async Task<ReportListingViewModel?> GetCarListingForReportAsync(int carListingId)
        {
            var model = await carListingRepository
                .GetAllAttached()
                .Where(cl => cl.Id == carListingId && cl.IsDeleted == false)
                .Select(cl => new ReportListingViewModel()
                {
                    ListingId = cl.Id,
                    SellerId = cl.SellerId,
                    SellerFullName = $"{cl.Seller.FirstName} {cl.Seller.LastName}",
                    SellerEmail = cl.Seller.Email ?? string.Empty,
                    CarListingTitle = $"{cl.Car.Model.CarBrand.BrandName} {cl.Car.Model.ModelName} {cl.Car.Trim}"
                })
                .FirstOrDefaultAsync();

            return model;
        }

    }
}
