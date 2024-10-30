using CarApp.Core.ViewModels;
using CarApp.Infrastructure.Data;
using CarApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

namespace CarApp.Controllers
{
    public class CarListingsController : Controller
    {
        private readonly CarDbContext context;
        public CarListingsController(CarDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CarInfoViewModel> model = await context.CarListings
                .Where(cl => cl.IsDeleted == false)
                .Select(cl => new CarInfoViewModel()
                {
                    id = cl.Id,
                    Brand = cl.Car.Model.CarBrand.BrandName,
                    Model = cl.Car.Model.ModelName,
                    Price = cl.Price.ToString("C", new System.Globalization.CultureInfo("fr-FR")),
                    FuelType = cl.Car.Fuel.FuelName,
                    GearType = cl.Car.Gear != null ? cl.Car.Gear.GearName : string.Empty,
                    ImageUrl = cl.MainImageUrl ?? string.Empty,
                    whp = cl.Car.Whp,
                    DatePosted = cl.DatePosted.ToString("hh:mm 'on' dd/MM/yy", CultureInfo.InvariantCulture),
                    SellerId = (cl.SellerId == GetCurrentUserId())
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
                Mileage = model.Milleage,
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
                DatePosted = DateTime.Now,
                Description = model.Description ?? string.Empty,
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

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            CarListing? carlisting = await context.CarListings.FindAsync(id);

            if (carlisting != null && carlisting.IsDeleted == false)
            {

                var model = await context.CarListings
                    .Where(car => car.Id == id && car.IsDeleted == false)
                    .Select(cl => new CarDetailsViewModel()
                    {
                        Brand = cl.Car.Model.CarBrand.BrandName,
                        Model = cl.Car.Model.ModelName,
                        Price = cl.Price.ToString("C", new System.Globalization.CultureInfo("Fr-fr")),
                        FuelType = cl.Car.Fuel.FuelName,
                        GearType = cl.Car.Gear != null ? cl.Car.Gear.GearName : string.Empty,
                        BodyType = cl.Car.CarBodyType.Name,
                        Images = cl.CarImages,
                        Whp = cl.Car.Whp,
                        Description = cl.Description,
                        DatePosted = cl.DatePosted.ToString("hh:mm 'on' dd/MM/yy", CultureInfo.InvariantCulture)
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
                
                if(model != null)
                {
                    return View(model);
                }
            }

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


