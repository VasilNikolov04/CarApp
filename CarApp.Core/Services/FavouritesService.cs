using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Infrastructure.Data;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Core.Services
{
    public class FavouritesService : IFavouritesService
    {
        private readonly IRepository<Favourite, object> favouritesRepository;
        private readonly CarDbContext context;
        private readonly IRepository<CarListing, int> carListingRepository;

        public FavouritesService(IRepository<Favourite, object> _favouritesRepository,
            IRepository<CarListing, int> _carListingRepository, CarDbContext _context)
        {
            favouritesRepository = _favouritesRepository;  
            carListingRepository = _carListingRepository;
            context = _context;
        }

        public async Task<bool> AddCarListingToFavouritesAsync(int carListingId, string userId)
        {
            CarListing? carListing = await carListingRepository
                .GetByIdAsync(carListingId);

            if (carListing != null && userId != null)
            {
                Favourite? alreadyAddedToFavourites = await context.Favourites
                    .FirstOrDefaultAsync(f => f.UserId == userId && f.CarListingId == carListingId);

                if (alreadyAddedToFavourites == null)
                {
                    Favourite newFavourite = new()
                    {
                        UserId = userId,
                        CarListingId = carListing.Id
                    };
                    await context.Favourites.AddAsync(newFavourite);
                    await context.SaveChangesAsync();
                }
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<CarInfoViewModel>> GetAllUserFavouritesAsync(string? userId)
        {
            var model = await favouritesRepository
                .GetAllAttached()
                .Where(cl => cl.UserId == userId && cl.CarListing.IsDeleted == false)
                .Select(cl => new CarInfoViewModel()
                {
                    Id = cl.CarListing.Id,
                    Brand = cl.CarListing.Car.Model.CarBrand.BrandName,
                    Model = cl.CarListing.Car.Model.ModelName,
                    Price = cl.CarListing.Price.ToString("C", new System.Globalization.CultureInfo("fr-FR")),
                    FuelType = cl.CarListing.Car.Fuel.FuelName,
                    GearType = cl.CarListing.Car.Gear.GearName,
                    ImageUrl = cl.CarListing.MainImageUrl,
                    Whp = cl.CarListing.Car.Whp,
                    EngineDisplacement = cl.CarListing.Car.EngineDisplacement,
                    LocationRegion = cl.CarListing.City.CarLocationRegion.RegionName,
                    LocationTown = cl.CarListing.City.CityName,
                    Milleage = cl.CarListing.Car.Mileage.ToString("N0"),
                    BodyType = cl.CarListing.Car.CarBodyType.Name

                })
                .AsNoTracking()
                .ToListAsync();

            return model;
        }

        public async Task<bool> IsCarListingInFavourites(int carListingId, string userId)
        {

            Favourite? favourite = await context.Favourites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.CarListingId == carListingId);

            if(favourite == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> RemoveCarListingFromFavouritesAsync(int carListingId, string? userId)
        {
            Favourite? favourite = await context.Favourites
                    .FirstOrDefaultAsync(f => f.UserId == userId && f.CarListingId == carListingId);

            if (favourite == null)
            {
                return false;
            }

            bool result = await favouritesRepository.DeleteAsync(favourite);

            if(result == false)
            {
                return false;
            }

            return true;

        }
    }
}
