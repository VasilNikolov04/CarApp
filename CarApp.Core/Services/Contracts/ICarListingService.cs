using CarApp.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.Services.Contracts
{
    public interface ICarListingService
    {
        Task<IEnumerable<CarInfoViewModel>> GetAllCarListingsAsync();

        Task AddCarListingAsync(CarViewModel model, string? user);

        Task<CarViewModel> PopulateDropdownsAsync();

        Task<CarDetailsViewModel?> CarListingDetails(int listingId); 
    }
}
