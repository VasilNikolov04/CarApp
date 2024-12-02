using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static CarApp.Infrastructure.Constants.ApplicationConstants;

namespace CarApp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<CarListing, int> carListingRepository;
        private readonly IRepository<Car, int> carRepository;
        private readonly UserManager<ApplicationUser> userManager;
        public UserService(IRepository<CarListing, int> _carListingRepository,
             IRepository<Car, int> _carRepository, UserManager<ApplicationUser> _userManager)
        {
            carListingRepository = _carListingRepository;
            carRepository = _carRepository;
            userManager = _userManager;
        }

        public async Task<bool> DeleteCarListingAsync(CarListingDeleteViewModel model, string? userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            var userRoles = await userManager.GetRolesAsync(user);

            CarListing? carListing = await carListingRepository
                .GetAllAttached()
                .Where(cl => cl.Id == model.Id && cl.IsDeleted == false 
                && (cl.SellerId == userId || userRoles.Contains(AdminRoleName)))
                .FirstOrDefaultAsync();

            if (carListing == null)
            {
                return false;
            }

            carListing.IsDeleted = true;
            await carRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditCarListingAsync(CarListingEditViewModel model, string? userId)
        {
            
            if (model == null || model.IsDeleted == true)
            {
                return false;
            }
            Car? car = await carRepository
                .GetAllAttached()
                .Where(c => c.CarListing.Id == model.Id)
                .FirstOrDefaultAsync();
            CarListing? carListing = await carListingRepository
                .GetByIdAsync(model.Id);
            if (car == null || carListing == null)
            {
                return false;
            }

            if (carListing.SellerId != userId)
            {
                return false;
            }

            car.Whp = model.Whp;
            car.Mileage = model.Milleage;
            carListing.Description = model.Description;
            carListing.CarImages = model.CarImages;
            carListing.Price = model.Price;

            if (model.NewCarImages != null && model.NewCarImages.Count > 0)
            {
                foreach (var image in model.NewCarImages)
                {
                    if (image != null)
                    {
                        var fileName = Path.GetFileName(image.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        carListing.CarImages.Add(new CarImage { ImageUrl = fileName });
                    }
                }
            }
            await carListingRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CarInfoViewModel>> GetAllUserCarListingsAsync(string? userId)
        {
            var model = await carListingRepository
                .GetAllAttached()
                .Where(cl => cl.SellerId == userId && cl.IsDeleted == false)
                .Select(cl => new CarInfoViewModel()
                {
                    Id = cl.Id,
                    Brand = cl.Car.Model.CarBrand.BrandName,
                    Model = cl.Car.Model.ModelName,
                    Price = cl.Price.ToString("C", new System.Globalization.CultureInfo("fr-FR")),
                    FuelType = cl.Car.Fuel.FuelName,
                    GearType = cl.Car.Gear != null ? cl.Car.Gear.GearName : string.Empty,
                    ImageUrl = cl.MainImageUrl,
                    Whp = cl.Car.Whp,
                    Trim = cl.Car.Trim,
                    EngineDisplacement = cl.Car.EngineDisplacement,
                    LocationRegion = cl.City.CarLocation.RegionName,
                    LocationTown = cl.City.CityName,
                    Milleage = cl.Car.Mileage.ToString("N0"),
                    BodyType = cl.Car.CarBodyType.Name,
                    Description = cl.Description ?? string.Empty,
                    SellerId = cl.SellerId


                })
                .AsNoTracking()
                .ToListAsync();

            return model;
        }

        public async Task<CarListingDeleteViewModel?> GetCarListingForDeleteAsync(int id, string? userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            var userRoles = await userManager.GetRolesAsync(user);

            CarListingDeleteViewModel? model = await carListingRepository
                    .GetAllAttached()
                    .Where(cl => cl.Id == id && cl.IsDeleted == false && 
                    (cl.SellerId == userId || userRoles.Contains(AdminRoleName)))
                    .Select(cl => new CarListingDeleteViewModel
                    {
                        Id = cl.Id,
                        Brand = cl.Car.Model.CarBrand.BrandName,
                        Model = cl.Car.Model.ModelName
                    })
                    .FirstOrDefaultAsync();

            return model;
        }

        public async Task<CarListingEditViewModel?> GetCarListingForEditAsync(int id)
        {
            CarListingEditViewModel? model = await carListingRepository
                    .GetAllAttached()
                    .Where(cl => cl.Id == id)
                    .Select(cl => new CarListingEditViewModel()
                    {
                        Id = cl.Id,
                        Description = cl.Description,
                        Whp = cl.Car.Whp,
                        CarImages = cl.CarImages,
                        Milleage = cl.Car.Mileage,
                        Price = cl.Price,
                        IsDeleted = cl.IsDeleted
                    })
                    .FirstOrDefaultAsync();

            return model;
        }
    }
}
