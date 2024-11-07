using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Infrastructure.Data;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CarApp.Core.Services
{
    public class CarListingService : ICarListingService
    {
        private readonly IRepository<CarListing,int> carListingRepository;
        private readonly IRepository<Car, int> carRepository;
        private readonly CarDbContext context;

        public CarListingService(IRepository<CarListing, int> _carListingRepository,
            IRepository<Car, int> _carRepository, CarDbContext _context)
        {
            carListingRepository = _carListingRepository;
            carRepository = _carRepository;
            context = _context;
        }

        public async Task AddCarListingAsync(CarViewModel model, string? userId)
        {

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

            await carRepository.AddAsync(newCar);

            var newCarListing = new CarListing
            {
                CarId = newCar.Id,
                Price = model.Price,
                DatePosted = DateTime.Now,
                Description = model.Description ?? string.Empty,
                SellerId = userId ?? string.Empty
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
            await carListingRepository.AddAsync(newCarListing);
        }

        public async Task<IEnumerable<CarInfoViewModel>> GetAllCarListingsAsync()
        {
            IEnumerable<CarInfoViewModel> model = await carListingRepository
                .GetAllAttached()
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
                    SellerId = cl.SellerId.ToString()
                })
                .AsNoTracking()
                .ToListAsync();

            return model;
        }

        public async Task<CarDetailsViewModel?> CarListingDetails(int listingId)
        {
            CarDetailsViewModel? model = await carListingRepository
                    .GetAllAttached()
                    .Where(car => car.Id == listingId && car.IsDeleted == false)
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

            return model;
        }

        public async Task<CarViewModel> PopulateDropdownsAsync()
        {

            var model = new CarViewModel
            {
                Brands = await GetBrandsAsync(),
                FuelTypes = await GetFuelTypesAsync(),
                Models = await GetModelsAsync(),
                Gears = await GetGearsAsync(),
                Drivetrains = await GetDrivetrainAsync(),
                CarBodyTypes = await GetBodyTypesAsync()
            };

            return model;
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
        public async Task<List<CarGear>> GetGearsAsync()
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
    }
}
