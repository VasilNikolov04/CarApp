using CarApp.Core.Enumerations;
using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.Services.Contracts
{
    public interface ICarListingService
    {
        Task<CarListingQueryServiceModel> GetAllCarListingsAsync(
            CarListingSorting sorting = CarListingSorting.BrandModelYear,
            int currentPage = 1,
            int listingsPerPage = 1);

        Task AddCarListingAsync(CarViewModel model, string? user);

        Task<CarViewModel> PopulateDropdownsAsync();

        Task<CarDetailsViewModel?> CarListingDetails(int listingId); 
    }
}
