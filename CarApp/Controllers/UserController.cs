using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;
using CarApp.Extensions;
using CarApp.Infrastructure.Constants.Enum;
using CarApp.Infrastructure.Data;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace CarApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly CarDbContext context;
        private readonly IUserService userService;
        private readonly IRepository<CarListing, int> carListingRepository;
        private readonly IReportService reportService;
        private readonly IRepository<CarImage, int> imageRepository;
        private readonly IFavouritesService favouritesService;
        public UserController(CarDbContext _context, IUserService _userService, 
            IRepository<CarListing, int> _carListingRepository, IFavouritesService _favouritesService,
            IRepository<CarImage, int> _imageRepository, IReportService _reportService)
        {
            context = _context;
            userService = _userService;
            carListingRepository = _carListingRepository;
            favouritesService = _favouritesService;
            imageRepository = _imageRepository;
            reportService = _reportService;
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
        [ValidateAntiForgeryToken]
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

        public async Task<IActionResult> RemoveImage(int carId, int imageId)
        {
            var car = await carListingRepository
                .GetAllAttached()
                .Include(c => c.CarImages)
                .Where(c => c.Id == carId)
                .FirstOrDefaultAsync();

            if (car == null)
            {
                return Json(new { success = false, message = "Car not found" });
            }

            var image = car.CarImages.FirstOrDefault(i => i.Id == imageId);
            if (image != null)
            {
                car.CarImages.Remove(image);
                await carListingRepository.UpdateAsync(car);

                var images = car.CarImages.Select(i => new { i.Id, i.ImageUrl }).ToList();

                return Json(new { success = true, images});
            }

            return Json(new { success = false, message = "Image not found" });
        }

        [HttpPost]
        //TO FIX ASAP
        public async Task<IActionResult> UpdateImageOrder([FromBody] UpdateImageOrderModel request)
        {
            if (request == null || request.OrderedImageIds == null || !request.OrderedImageIds.Any())
            {
                return Json(new { success = false, message = "Invalid request" });
            }

            // Fetch the car and its images
            var car = context.CarListings.Include(c => c.CarImages)
                                      .FirstOrDefault(c => c.Id == request.CarId);

            if (car == null)
            {
                return Json(new { success = false, message = "Car not found" });
            }

            // Get the images in the current order
            var imageList = car.CarImages.ToList();

            // Reorder the images based on the OrderedImageIds
            var reorderedImages = request.OrderedImageIds
                .Select(id => imageList.FirstOrDefault(img => img.Id == id))
                .Where(img => img != null)
                .ToList();

            // Update the car's main image (first image in the reordered list)
            if (reorderedImages.Any())
            {
                car.MainImageUrl = reorderedImages.First().ImageUrl;
            }

            // Reassign the CarImages to maintain the correct order in the database
            car.CarImages = reorderedImages;

            // Save changes to the car's image order
            await context.SaveChangesAsync();

            return Json(new { success = true });
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
            var referer = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(referer) )
            {
                return Redirect(referer);
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
            var referer = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
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

            var referer = Request.Headers["Referer"].ToString();

            // If Referer is not empty, redirect to the previous page
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }

            return RedirectToAction(nameof(Favourites));
        }

        [HttpGet]
        public async Task<IActionResult> Report(int carListingId)
        {
            ReportListingViewModel? model = await reportService.GetCarListingForReportAsync(carListingId);
            if (model == null)
            {
                return RedirectToAction(nameof(UserListings));
            }

            model.Reasons = Enum.GetValues(typeof(ReportReason)).Cast<ReportReason>().ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Report(ReportListingViewModel model)
        {
            var userId = User?.GetUserId();
            if(userId == null)
            {
                return View(model);
            }
            if(ModelState.IsValid)
            {
                await reportService.AddReportAsync(model, userId);

                return RedirectToAction("Index", "CarListings");
            }
            model.Reasons = Enum.GetValues(typeof(ReportReason)).Cast<ReportReason>().ToList();
            return View(model);
        }


        public class UpdateImageOrderModel
        {
            public int CarId { get; set; }
            public List<int> OrderedImageIds { get; set; }
        }

    }
}
