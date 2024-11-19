using CarApp.Core.Enumerations;
using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;
using CarApp.Core.ViewModels.RefinedSearch;
using CarApp.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.Services.Contracts
{
    public interface IRefinedSearchService
    {
        Task<CarListingQueryServiceModel> GetAllCarListingsAsync(
            string? brand,
            string? model,
            string? fuel = null,
            string? gear = null,
            string? carbody = null,
            string? drivetrain = null,
            int? minprice = 0,
            int? maxprice = 0,
            int? minyear = 0,
            int? maxyear = 0,
            int minwhp = 0,
            int maxwhp = 0,
            int? mindisplacement = 0,
            int? maxdisplacement = 0,
            int? mileage = 0,
            int currentPage = 1,
            int listingsPerPage = 1,
            CarListingSorting sorting = CarListingSorting.BrandModelYear);
    }

    
}
