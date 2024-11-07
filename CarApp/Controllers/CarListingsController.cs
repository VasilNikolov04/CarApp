using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Extensions;
using CarApp.Infrastructure.Data;
using CarApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;

namespace CarApp.Controllers
{
    public class CarListingsController : Controller
    {
        private readonly CarDbContext context;
        private readonly ICarListingService carListingService;
        public CarListingsController(CarDbContext _context, ICarListingService _carListingService)
        {
            context = _context;
            carListingService = _carListingService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CarInfoViewModel> model 
                = await carListingService.GetAllCarListingsAsync();

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Add()
        {
            var model = new CarViewModel();
            model = await carListingService.PopulateDropdownsAsync();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CarViewModel model)
        {
            string userId = User.GetUserId()!;
            if (ModelState.IsValid == false)
            {
                model = await carListingService.PopulateDropdownsAsync();
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


