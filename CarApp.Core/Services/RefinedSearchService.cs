using CarApp.Core.Enumerations;
using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;
using CarApp.Core.ViewModels.RefinedSearch;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarApp.Core.Services
{
    public class RefinedSearchService : IRefinedSearchService
    {
        private readonly IRepository<CarListing, int> carListingRepository;
        private readonly IRepository<Car, int> carRepository;

        public RefinedSearchService(IRepository<CarListing, int> _carListingRepository,
            IRepository<Car, int> _carRepository)
        {
            carListingRepository = _carListingRepository;
            carRepository = _carRepository;
        }
        public async Task<CarListingQueryServiceModel> GetAllCarListingsAsync(
            string? brand,
            string? model,
            int? minprice = 0, int? maxprice = 0,
            int? minyear = 0, int? maxyear = 0,
            int? minwhp = 0, int? maxwhp = 0,
            int? mindisplacement = 0, int? maxdisplacement = 0,
            int? mileage = 0,
            string? fuel = null, string? gear = null, 
            string? carbody = null, string? drivetrain = null, 
            int currentPage = 1, 
            int listingsPerPage = 1,
            CarListingSorting sorting = CarListingSorting.BrandModelYear)
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
                _ => carListings
                    .OrderBy(cl => cl.Car.Model.CarBrand.BrandName)
                    .ThenBy(cl => cl.Car.Model.ModelName)
                    .ThenBy(cl => cl.Car.Year)
            };

            if (minprice != 0)carListings = carListings.Where(l => l.Price >= minprice);
            if (maxprice != 0)carListings = carListings.Where(l => l.Price <= maxprice);

            if (minyear != 0)carListings = carListings.Where(l => l.Car.Year >= minyear);
            if (maxyear != 0)carListings = carListings.Where(l => l.Car.Year <= maxyear);

            if (minwhp != 0) carListings = carListings.Where(l => l.Car.Whp >= minwhp);
            if (maxwhp != 0) carListings = carListings.Where(l => l.Car.Whp <= maxwhp);

            if (mindisplacement != 0) carListings = carListings.Where(l => l.Car.EngineDisplacement >= mindisplacement);
            if (maxdisplacement != 0) carListings = carListings.Where(l => l.Car.EngineDisplacement <= maxdisplacement);

            if (mileage != 0) carListings = carListings.Where(l => l.Car.Mileage <= mileage);

            if (fuel != null) carListings = carListings.Where(l => l.Car.Fuel.FuelName == fuel);

            if (gear != null) carListings = carListings.Where(l => l.Car.Gear.GearName == gear);

            if (drivetrain != null) carListings = carListings.Where(l => l.Car.Drivetrain.DrivetrainName == drivetrain);

            if (carbody != null) carListings = carListings.Where(l => l.Car.CarBodyType.Name == carbody);

            if (brand != null)
            {
                carListings = carListings
                    .Where(l => l.Car.Model.CarBrand.BrandName == brand);

                if (model != null)
                {
                    carListings = carListings
                        .Where(l => l.Car.Model.ModelName == model);
                }
            }

            int totalListings = carListings.Count();

            var listings = await carListings
            .Skip((currentPage - 1) * listingsPerPage)
            .Take(listingsPerPage)
            .Select(cl => new CarInfoViewModel()
            {
                id = cl.Id,
                Brand = cl.Car.Model.CarBrand.BrandName,
                Model = cl.Car.Model.ModelName,
                Trim = cl.Car.Trim,
                Price = cl.Price.ToString("C", new System.Globalization.CultureInfo("fr-FR")),
                FuelType = cl.Car.Fuel.FuelName,
                Year = cl.Car.Year,
                GearType = cl.Car.Gear != null ? cl.Car.Gear.GearName : string.Empty,
                ImageUrl = cl.MainImageUrl ?? string.Empty,
                whp = cl.Car.Whp,
                DatePosted = cl.DatePosted.ToString("hh:mm 'on' dd/MM/yy", CultureInfo.InvariantCulture),
                SellerId = cl.SellerId.ToString()
            })
            .ToListAsync();

            return new CarListingQueryServiceModel()
            {
                CarListings = listings,
                TotalListingsCount = totalListings
            };
        }
    }
}
