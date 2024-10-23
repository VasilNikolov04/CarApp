using CarApp.Core.ViewModels;
using CarApp.Infrastructure.Data;
using CarApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarApp.Controllers
{
    public class CarListingController : Controller
    {
        private readonly CarDbContext context;
        public CarListingController(CarDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CarInfoViewModel> model = await context.CarListings
                .Select(cl => new CarInfoViewModel()
                {
                    Brand = cl.Car.Model.CarBrand.BrandName,
                    Model = cl.Car.Model.ModelName,
                    Price = cl.Price,
                    FuelType = cl.Car.Fuel.FuelName,
                    GearType = cl.Car.Gear.GearName ?? string.Empty,
                    ImageUrl = cl.MainImageUrl,
                    whp = cl.Car.Whp
                })
                .AsNoTracking()
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Add()
        {
            var model = new CarViewModel
            {
                Brands = await GetBrandsAsync(),
                FuelTypes = await GetFuelTypesAsync(),
                Models = await GetModelsAsync(),
                Gears = await GetTransmissionAsync(),
                Drivetrains = await GetDrivetrainAsync(),
                CarBodyTypes = await GetBodyTypesAsync()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CarViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                model.Brands = await GetBrandsAsync();
                model.FuelTypes = await GetFuelTypesAsync();
                model.Models = await GetModelsAsync();
                model.Gears = await GetTransmissionAsync();
                model.Drivetrains = await GetDrivetrainAsync();
                model.CarBodyTypes = await GetBodyTypesAsync();
                return View(model);
            }
            if (model.Year > DateTime.Now.Year || model.Year < 1930)
            {
                ModelState.AddModelError(nameof(model.Year), "Invalid date");
                model.Brands = await GetBrandsAsync();
                model.FuelTypes = await GetFuelTypesAsync();
                model.Models = await GetModelsAsync();
                model.Gears = await GetTransmissionAsync();
                model.Drivetrains = await GetDrivetrainAsync();
                model.CarBodyTypes = await GetBodyTypesAsync();
                return View(model);
            }

            Car newCar = new Car()
            {
                ModelId = model.ModelId,
                Year = model.Year,
                Whp = model.Whp,
                CarBodyId = model.CarBodyId,
                FuelId = model.FuelId,
                DrivetrainId = model.DrivetrainId,
                GearId = model.GearId
            };

            await context.Cars.AddAsync(newCar);
            await context.SaveChangesAsync();

            var newCarListing = new CarListing
            {
                CarId = newCar.Id,
                Price = model.Price,
                SellerId = GetCurrentUserId() ?? string.Empty
            };

            if (model.CarImages != null && model.CarImages.Count > 0)
            {
                foreach (var image in model.CarImages)
                {
                    if (image.Length > 0)
                    {
                        var fileName = Path.GetFileName(image.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        newCarListing.CarImages.Add(new CarImage { ImageUrl = fileName });
                    }
                }
            }

            if (newCarListing.CarImages.Any())
            {
                newCarListing.MainImageUrl = newCarListing.CarImages.First().ImageUrl;
            }
            await context.CarListings.AddAsync(newCarListing);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<List<CarFuelType>> GetFuelTypesAsync()
        {
            return await context.CarFuelTypes.ToListAsync();
        }
        public async Task<List<CarModel>> GetModelsAsync()
        {
            return await context.CarModels
                .ToListAsync();
        }
        public async Task<List<CarBrand>> GetBrandsAsync()
        {
            return await context.CarBrands.ToListAsync();
        }
        public async Task<List<CarGear>> GetTransmissionAsync()
        {
            return await context.CarGears.ToListAsync();
        }
        public async Task<List<CarDrivetrain>> GetDrivetrainAsync()
        {
            return await context.CarDrivetrains.ToListAsync();
        }
        public async Task<List<CarBodyType>> GetBodyTypesAsync()
        {
            return await context.CarBodyTypes.ToListAsync();
        }
        public string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}


