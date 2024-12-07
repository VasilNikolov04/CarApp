using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels.CarListing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CarApp.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly IReportService reportService;
        public HomeController(IReportService _reportService)
        {
            reportService = _reportService;
        }
        public IActionResult AdminPanel()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ForReview()
        {
            IEnumerable<AllReportedListingViewModel> models 
                = await reportService.GetAllReportedCarListingsAsync();

            return View(models);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int carListingId)
        {
            bool result = await reportService.ApproveCarListingAsync(carListingId);
            if(result == false)
            {
                return RedirectToAction(nameof(ForReview));
            }
            return RedirectToAction(nameof(ForReview));
        }
    }
}
