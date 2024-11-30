using CarApp.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.Services.Contracts
{
    public interface IFavouritesService
    {
        Task<IEnumerable<CarInfoViewModel>> GetAllUserFavouritesAsync(string userId);

        Task<bool> AddCarListingToFavouritesAsync(int carListingId, string userId);

        Task<bool> RemoveCarListingFromFavouritesAsync(int carListingId, string userId);

        Task<bool> IsCarListingInFavourites(int carListingId, string userId);
    }
}
