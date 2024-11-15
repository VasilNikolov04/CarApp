using CarApp.Core.Enumerations;
using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;
using CarApp.Core.ViewModels.RefinedSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.Services.Contracts
{
    public interface IRefinedSearchService
    {
        //Task<CarListingQueryServiceModel> GetAllCarListingsAsync(
        //    string? brand,
        //    string? model,
        //    int? minprice = 0,
        //    int? maxprice = 0,
        //    int currentPage = 1,
        //    int listingsPerPage = 1);

        Task FilterCarListingAsync(RefinedSearchViewModel model);
    }

    
}
