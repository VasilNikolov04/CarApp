using CarApp.Core.Services;
using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;
using CarApp.Core.ViewModels.RefinedSearch;
using Microsoft.AspNetCore.Mvc;

namespace CarApp.Controllers
{
    public class RefinedSearchController : Controller
    {
        private readonly IUtilityService utilityService;
        private readonly IRefinedSearchService refinedSearchService;

        public RefinedSearchController(IUtilityService _utilityService, IRefinedSearchService _refinedSearchService)
        {
            utilityService = _utilityService;
            refinedSearchService = _refinedSearchService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] RefinedSearchViewModel viewModel)
        {

            var cars = await refinedSearchService.GetAllCarListingsAsync(
                viewModel.Brand,
                viewModel.Model,
                viewModel.Fuel, viewModel.Gear, viewModel.CarBody,
                viewModel.Drivetrain,
                viewModel.MinPrice, viewModel.MaxPrice,
                viewModel.MinYear, viewModel.MaxYear,
                viewModel.MinWhp, viewModel.MaxWhp,
                viewModel.MinEngineDisplacement, viewModel.MaxEngineDisplacement,
                viewModel.Mileage,
                viewModel.CurrentPage,
                viewModel.CarsPerPage,
                viewModel.Sorting);

            viewModel = await utilityService.PopulateAllDropdownsAsync(viewModel);
            viewModel.MileageList = utilityService.GetMileageDropdown();


            viewModel.TotalCarsCount = cars.TotalListingsCount;
            viewModel.CarListings = cars.CarListings;
            return View(viewModel);
        }
    }
}
