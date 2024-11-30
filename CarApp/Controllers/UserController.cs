using CarApp.Core.Services;
using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Extensions;
using CarApp.Infrastructure.Data;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Collections.Generic;
using System.Security.Claims;

namespace CarApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly CarDbContext context;
        private readonly IUserService userService;
        private readonly IRepository<CarListing, int> carListingRepository;
        private readonly IFavouritesService favouritesService;
        public UserController(CarDbContext _context, IUserService _userService, 
            IRepository<CarListing, int> _carListingRepository, IFavouritesService _favouritesService)
        {
            context = _context;
            userService = _userService;
            carListingRepository = _carListingRepository;
            favouritesService = _favouritesService;
        }

        [HttpGet]
        public async Task<IActionResult> UserListings()
        {
            var userId = User.GetUserId();
            
            IEnumerable<CarInfoViewModel> models = 
                await userService.GetAllUserCarListingsAsync(userId);

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> EditListing(int Id)
        {
            CarListingEditViewModel? model = await userService.GetCarListingForEditAsync(Id);
            if (model == null || model.IsDeleted == true)
            {
                return RedirectToAction(nameof(UserListings));
            }
            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditListing(CarListingEditViewModel model)
        {
            string userId = User.GetUserId()!;
            if (ModelState.IsValid == false)
            {
                return View(model); 
            }
            bool isUpdated = await userService.EditCarListingAsync(model, userId);

            if(isUpdated == false)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error while updating the car listing!");
                return View(model);
            }

            return RedirectToAction(nameof(Details),"CarListings", new {id = model.Id});
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = User.GetUserId()!;
            CarListingDeleteViewModel? model = await userService
                .GetCarListingForDeleteAsync(id, userId);
            if (model == null)
            { 
                return RedirectToAction("Index", "CarListings");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CarListingDeleteViewModel modelToDelete)
        {
            string userId = User.GetUserId()!;

            bool IsDeleted = await userService.DeleteCarListingAsync(modelToDelete, userId);

            if (IsDeleted == false)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error while deleting the car listing!");
                return View(nameof(Delete));
            }

            return RedirectToAction(nameof(UserListings));
        }


        [HttpGet]
        public async Task<IActionResult> Favourites()
        {
            string? userId = User?.GetUserId();
            if (String.IsNullOrWhiteSpace(userId))
            {
                return RedirectToPage("/Identity/Account/Login");
            }
            var models = await favouritesService.GetAllUserFavouritesAsync(userId);

            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavourites(int carListingId)
        {
            string? userId = User?.GetUserId();
            if (String.IsNullOrWhiteSpace(userId))
            {
                return RedirectToPage("/Identity/Account/Login");
            }
            bool result = await favouritesService
                .AddCarListingToFavouritesAsync(carListingId, userId);

            if (result == false)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Favourites));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromFavourites(int carListingId)
        {
            string? userId = User?.GetUserId();
            if (String.IsNullOrWhiteSpace(userId))
            {
                return RedirectToPage("/Identity/Account/Login");
            }
            bool result = await favouritesService
                .RemoveCarListingFromFavouritesAsync(carListingId, userId);

            if (result == false)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Favourites));
        }
    }
}
