using CarApp.Core.Services.Contracts;
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
        private readonly ICarListingService carListingService;

        public HomeController(ILogger<HomeController> logger, 
            IRepository<CarListing, int> _carListingRepository,
            UserManager<ApplicationUser> _userManager,
            ICarListingService _carListingService)
        {
            _logger = logger;
            carListingRepository = _carListingRepository;
            userManager = _userManager;
            carListingService = _carListingService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await carListingService.GetHomePageDataAsync();

            return View(model);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {

            if (statusCode == 404)
            {
                return View("Error404");
            }

            if (statusCode == 500)
            {
                return View("Error500");
            }
            return View();
        }
    }
}
