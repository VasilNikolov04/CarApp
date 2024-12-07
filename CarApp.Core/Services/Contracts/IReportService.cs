using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.Services.Contracts
{
    public interface IReportService
    {
        Task AddReportAsync(ReportListingViewModel model, string userId);
        Task<bool> ApproveCarListingAsync(int carListingId);
        Task<IEnumerable<AllReportedListingViewModel>> GetAllReportedCarListingsAsync();
        Task<ReportListingViewModel?> GetCarListingForReportAsync(int carListingId);
    }
}
