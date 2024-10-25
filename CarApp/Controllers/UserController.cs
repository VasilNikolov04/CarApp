using CarApp.Core.ViewModels;
using CarApp.Infrastructure.Data;
using CarApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Security.Claims;

namespace CarApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly CarDbContext context;
        public UserController(CarDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> UserListings()
        {
            var userId = GetCurrentUserId();

            var model = await context.CarListings
                .Where(cl => cl.SellerId == userId)
                .Select(cl => new CarInfoViewModel()
                {
                    id = cl.Id,
                    Brand = cl.Car.Model.CarBrand.BrandName,
                    Model = cl.Car.Model.ModelName,
                    Price = cl.Price,
                    FuelType = cl.Car.Fuel.FuelName,
                    GearType = cl.Car.Gear != null ? cl.Car.Gear.GearName : string.Empty,
                    ImageUrl = cl.MainImageUrl ?? string.Empty,
                    whp = cl.Car.Whp
                })
                .AsNoTracking()
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditListing(int Id)
        {
            CarListing? carLising = await context.CarListings.FindAsync(Id);
            if (carLising != null && carLising.IsDeleted == false)
            {
                var model = await context.CarListings
                    .Where(cl => cl.Id == Id)
                    .Select(cl => new CarListingEditViewModel()
                    {
                        Id = cl.Id,
                        Description = cl.Description,
                        Whp = cl.Car.Whp,
                        CarImages = cl.CarImages,
                        Milleage = cl.Car.Mileage,
                        Price = cl.Price
                    })
                    .FirstOrDefaultAsync();
                if (model != null)
                {
                    return View(model);
                }
            }
            return RedirectToAction(nameof(UserListings));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditListing(CarListingEditViewModel model, int id)
        {
            if (ModelState.IsValid == false)
            {
                return View(model); 
            }

            CarListing? carListing = await context.CarListings.FindAsync(id);

            if(carListing == null || carListing.IsDeleted == true)
            {
                return RedirectToAction(nameof(UserListings));
            }
            Car? car = await context.Cars.Where(c => c.CarListing.Id == id).FirstOrDefaultAsync();
            if (car == null)
            {
                return RedirectToAction(nameof(UserListings));
            }
            string userId = GetCurrentUserId() ?? string.Empty;

            if(carListing.SellerId != userId)
            {
                return RedirectToAction(nameof(UserListings));
            }

            car.Whp = model.Whp;
            car.Mileage = model.Milleage;

            carListing.Description = model.Description;
            carListing.CarImages = model.CarImages;
            carListing.Price = model.Price;

            //if (model.CarImages != null && model.CarImages.Count > 0)
            //{
            //    foreach (var image in model.CarImages)
            //    {
            //        if (image.Length > 0)
            //        {
            //            var fileName = Path.GetFileName(image.FileName);
            //            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            //            using (var stream = new FileStream(filePath, FileMode.Create))
            //            {
            //                await image.CopyToAsync(stream);
            //            }
            //            newCarListing.CarImages.Add(new CarImage { ImageUrl = fileName });
            //        }
            //    }
            //}
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Details),nameof(CarListing), new {id = carListing.Id});
        }
        public string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
