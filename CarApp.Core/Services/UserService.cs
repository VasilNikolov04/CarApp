using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;
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
                .FirstOrDefaultAsync(c => c.CarListing.Id == model.Id);

            CarListing? carListing = await carListingRepository
                .GetAllAttached()
                .Include(cl => cl.CarImages)
                .FirstOrDefaultAsync(cl => cl.Id == model.Id);

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
            carListing.Price = model.Price;


            var imageIdsFromModel = model.CarImages.Select(i => i.Id).ToList();
            carListing.CarImages = carListing.CarImages
                .Where(i => imageIdsFromModel.Contains(i.Id))
                .ToList();
            foreach (var image in carListing.CarImages)
            {
                var updatedImage = model.CarImages.FirstOrDefault(i => i.Id == image.Id);
                if (updatedImage != null)
                {
                    image.Order = updatedImage.Order;
                }
            }

            if (model.NewCarImages.Any() && model.NewCarImages.Count > 0)
            {
                int displayIndex = 0;
                if (model.CarImages.Any() && model.CarImages.Count > 1)
                {
                    displayIndex = carListing.CarImages.Max(img => img.Order);
                }

                foreach (var image in model.NewCarImages)
                {
                    if (image.Length > 0)
                    {
                        var fileName = Path.GetFileName(image.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        carListing.CarImages.Add(new CarImage 
                        { 
                            ImageUrl = fileName,
                            Order = displayIndex,
                            CarListingId = carListing.Id
                        });
                        displayIndex++;
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
                    Year = cl.Car.Year,
                    Price = cl.Price.ToString("C", new System.Globalization.CultureInfo("fr-FR")),
                    FuelType = cl.Car.Fuel.FuelName,
                    GearType = cl.Car.Gear != null ? cl.Car.Gear.GearName : string.Empty,
                    ImageUrl = cl.CarImages.Where(ci => ci.Order == 0).FirstOrDefault().ImageUrl,
                    Whp = cl.Car.Whp,
                    Trim = cl.Car.Trim,
                    EngineDisplacement = cl.Car.EngineDisplacement,
                    LocationRegion = cl.City.CarLocationRegion.RegionName,
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
                        Model = cl.Car.Model.ModelName,
                        SellerEmail = cl.Seller.Email ?? string.Empty,
                        SellerFullName = $"{cl.Seller.FirstName} {cl.Seller.LastName}",
                        Image = cl.CarImages.Where(ci => ci.Order == 0).FirstOrDefault().ImageUrl
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
                        CarImages = cl.CarImages
                        .Select(im => new CarImageViewModel
                        {
                            Id = im.Id,
                            ImageUrl = im.ImageUrl,
                            CarListingId = im.CarListingId,
                            Order = im.Order
                        })
                        .OrderBy(im => im.Order)
                        .ToList(),
                        Milleage = cl.Car.Mileage,
                        Price = cl.Price,
                        IsDeleted = cl.IsDeleted
                    })
                    .FirstOrDefaultAsync();

            return model;
        }
    }
}
