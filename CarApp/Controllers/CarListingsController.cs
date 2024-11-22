using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;
using CarApp.Extensions;
using CarApp.Infrastructure.Data;
using CarApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Controllers
{
    public class CarListingsController : Controller
    {
        private readonly CarDbContext context;
        private readonly ICarListingService carListingService;
        private readonly IUtilityService utilityService;
        public CarListingsController(CarDbContext _context, ICarListingService _carListingService,
            IUtilityService _utilityService)
        {
            context = _context;
            carListingService = _carListingService;
            utilityService = _utilityService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]AllCarsQueryModel viewModel)
        {
            var cars = await carListingService.GetAllCarListingsAsync(
                viewModel.Brand,
                viewModel.Model,
                viewModel.PriceLimit,
                viewModel.Sorting,
                viewModel.CurrentPage,
                viewModel.CarsPerPage);

            viewModel.Brands = await utilityService.GetBrandsAsync();
            viewModel.Models = await utilityService.GetModelsAsync();
            viewModel.PriceList = utilityService.GetPriceDropdown();


            viewModel.TotalCarsCount = cars.TotalListingsCount;
            viewModel.CarListings = cars.CarListings;
            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Add()
        {
            var model = new CarViewModel();
            model = await utilityService.PopulateDropdownsAsync();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(CarViewModel model)
        {
            string userId = User.GetUserId()!;
            if (ModelState.IsValid == false)
            {
                model = await utilityService.PopulateDropdownsAsync();
                return View(model);
            }

            await carListingService.AddCarListingAsync(model, userId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            CarDetailsViewModel? carlisting = await carListingService.CarListingDetails(id);

            if(carlisting == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(carlisting);
        }

    }
}


