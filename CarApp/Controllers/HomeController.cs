using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.Home;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using CarApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;

namespace CarApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<CarListing, int> carListingRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(ILogger<HomeController> logger, 
            IRepository<CarListing, int> _carListingRepository,
            UserManager<ApplicationUser> _userManager)
        {
            _logger = logger;
            carListingRepository = _carListingRepository;
            userManager = _userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<FeaturedCarsViewModel> latestCars = await carListingRepository
                .GetAllAttached()
                .Where(cl => cl.IsDeleted == false)
                .OrderByDescending(cl => cl.DatePosted)
                .Select(cl => new FeaturedCarsViewModel()
                {
                    Id = cl.Id,
                    Brand = cl.Car.Model.CarBrand.BrandName,
                    Model = cl.Car.Model.ModelName,
                    Trim = cl.Car.Trim ?? string.Empty,
                    Year = cl.Car.Year,
                    Price = cl.Price.ToString("C", new System.Globalization.CultureInfo("Fr-fr")),
                    DatePosted = cl.DatePosted.ToString("hh:mm 'on' dd/MM/yy", CultureInfo.InvariantCulture),
                    LocationRegion = cl.City.CarLocationRegion.RegionName,
                    LocationCity = cl.City.CityName,
                    ImageUrl = cl.CarImages.FirstOrDefault().ImageUrl ?? string.Empty
                })
                .Take(4)
                .ToListAsync();

            HomePageViewModel model = new HomePageViewModel
            {
                LatestCars = latestCars,
                TotalCarsListed = carListingRepository.GetAllAttached().Where(cl => cl.IsDeleted == false).Count(),
                AllUsers = userManager.Users.Count(),
                AllSellers = userManager.Users.Where(u => u.CarListings != null && u.CarListings.Where(cl => cl.IsDeleted == false).Any()).Count()
            };

            return View(model);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if(statusCode == 400)
            {
                return View("Error400");
            }

            if (statusCode == 404)
            {
                return View("Error404");
            }

            if (statusCode == 500)
            {
                return View("Error400");
            }
            return View();
        }
    }
}
