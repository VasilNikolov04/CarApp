using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;
using CarApp.Extensions;
using CarApp.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Index([FromQuery]AllCarsQueryModel model)
        {
            var cars = await carListingService.GetAllCarListingsAsync(
                model.BrandId,
                model.ModelId,
                model.PriceLimit,
                model.Sorting, 
                model.CurrentPage, 
                model.CarsPerPage);

            model.Brands = await utilityService.GetBrandsAsync();
            model.Models = await utilityService.GetModelsAsync();
            model.PriceList = utilityService.GetPriceDropdown();


            model.TotalCarsCount = cars.TotalListingsCount;
            model.CarListings = cars.CarListings;
            return View(model);
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


