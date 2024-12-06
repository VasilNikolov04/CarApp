using CarApp.Core.Enumerations;
using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;
using CarApp.Infrastructure.Data;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CarApp.Core.Services
{
    public class CarListingService : ICarListingService
    {
        private readonly IRepository<CarListing,int> carListingRepository;
        private readonly IRepository<Car, int> carRepository;

        public CarListingService(IRepository<CarListing, int> _carListingRepository,
            IRepository<Car, int> _carRepository)
        {
            carListingRepository = _carListingRepository;
            carRepository = _carRepository;
        }

        public async Task AddCarListingAsync(CarViewModel model, string? userId)
        {

            Car newCar = new Car()
            {
                ModelId = model.ModelId,
                Year = model.Year,
                Trim = model.Trim,
                Mileage = model.Milleage,
                Whp = model.Whp,
                EngineDisplacement = model.EngineDisplacement,
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
                SellerId = userId ?? string.Empty,
                CityId = model.CityId
            };

            if (model.CarImages != null && model.CarImages.Count > 0)
            {
                int imageOrder = 0;
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
                        newCarListing.CarImages.Add(new CarImage 
                        { 
                            ImageUrl = fileName,
                            Order = imageOrder,
                            CarListingId = newCarListing.Id
                        
                        });
                        imageOrder++;
                    }
                }
            }
            await carListingRepository.AddAsync(newCarListing);
        }

        public async Task<CarListingQueryServiceModel> GetAllCarListingsAsync(
            string? brand = null,
            string? model = null,
            int price = 0, 
            CarListingSorting sorting = CarListingSorting.BrandModelYear, 
            int currentPage = 1, 
            int listingsPerPage = 1)
        {
            var carListings = carListingRepository
                .GetAllAsReadOnly()
                .Where(cl => cl.IsDeleted == false);
                

            carListings = sorting switch
            {
                CarListingSorting.PriceDescending => carListings.OrderByDescending(cl => cl.Price),
                CarListingSorting.PriceAscending => carListings.OrderBy(cl => cl.Price),
                CarListingSorting.CarYearDescending => carListings.OrderBy(cl => cl.Car.Year),
                CarListingSorting.CarYearAscending => carListings.OrderByDescending(cl => cl.Car.Year),
                CarListingSorting.DateAddedDescending => carListings.OrderByDescending(cl => cl.DatePosted),
                CarListingSorting.DateAddedAscending => carListings.OrderBy(cl => cl.DatePosted),
                _ => carListings.OrderBy(cl => cl.Car.Model.CarBrand.BrandName)
                .ThenBy(cl => cl.Car.Model.ModelName).ThenBy(cl => cl.Car.Year)
            };

            if (price != 0)
            {
                carListings = carListings
                    .Where(l => l.Price <= price);
            }
            if (brand != null)
            {
                carListings = carListings
                    .Where(l => l.Car.Model.CarBrand.BrandName == brand);

                if (model != null)
                {
                    if (model.Contains("(All)"))
                    {
                        carListings = carListings
                           .Where(l => l.Car.Model.ModelName.StartsWith(model.Substring(0, model.IndexOf("-"))));
                    }
                    else
                    {
                        carListings = carListings
                            .Where(l => l.Car.Model.ModelName == model);
                    }
                }
            }

            var listings = await carListings
            .Skip((currentPage - 1) * listingsPerPage)
            .Take(listingsPerPage)
            .Select(cl => new CarInfoViewModel()
            {
                Id = cl.Id,
                Brand = cl.Car.Model.CarBrand.BrandName,
                Model = cl.Car.Model.ModelName,
                Trim = cl.Car.Trim,
                Price = cl.Price.ToString("C", new System.Globalization.CultureInfo("fr-FR")),
                FuelType = cl.Car.Fuel.FuelName,
                Year = cl.Car.Year,
                GearType = cl.Car.Gear != null ? cl.Car.Gear.GearName : string.Empty,
                ImageUrl = cl.CarImages.OrderBy(i => i.Order).Select(i => i.ImageUrl).FirstOrDefault(),
                Whp = cl.Car.Whp,             
                DatePosted = cl.DatePosted.ToString("hh:mm 'on' dd/MM/yy", CultureInfo.InvariantCulture),
                SellerId = cl.SellerId.ToString(),
                EngineDisplacement = cl.Car.EngineDisplacement,
                LocationRegion = cl.City.CarLocationRegion.RegionName,
                LocationTown = cl.City.CityName,
                Milleage = cl.Car.Mileage.ToString("N0"),
                BodyType = cl.Car.CarBodyType.Name,
                Description = cl.Description ?? string.Empty,
            })
            .ToListAsync();

            int totalListings = carListings.Count();

            return new CarListingQueryServiceModel()
            {        
                CarListings = listings,
                TotalListingsCount = totalListings
            };
        }

        public async Task<CarDetailsViewModel?> CarListingDetails(int listingId)
        {
            CarDetailsViewModel? model = await carListingRepository
                    .GetAllAttached()
                    .Where(car => car.Id == listingId && car.IsDeleted == false)
                    .Select(cl => new CarDetailsViewModel()
                    {
                        Id = cl.Id,
                        Brand = cl.Car.Model.CarBrand.BrandName,
                        Model = cl.Car.Model.ModelName,
                        Trim = cl.Car.Trim,
                        EngineDisplacement = cl.Car.EngineDisplacement,
                        Price = cl.Price.ToString("C", new System.Globalization.CultureInfo("Fr-fr")),
                        FuelType = cl.Car.Fuel.FuelName,
                        GearType = cl.Car.Gear != null ? cl.Car.Gear.GearName : string.Empty,
                        BodyType = cl.Car.CarBodyType.Name,
                        Images = cl.CarImages.OrderBy(im => im.Order).ToList(),
                        Year = cl.Car.Year,
                        Seller = new SellerViewModel()
                        {
                            SellerFullName = $"{cl.Seller.FirstName} {cl.Seller.LastName}",
                            SellerEmail = cl.Seller.Email ?? string.Empty,
                            SellerPhoneNumber = cl.Seller.PhoneNumber ?? string.Empty
                        },
                        Whp = cl.Car.Whp,
                        Milleage = cl.Car.Mileage.ToString("N0"),
                        Description = cl.Description,
                        DatePosted = cl.DatePosted.ToString("hh:mm 'on' dd/MM/yy", CultureInfo.InvariantCulture)
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

            return model;
        }


        
    }
}
