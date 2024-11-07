using CarApp.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<CarInfoViewModel>> GetAllUserCarListingsAsync(string? userId);

        Task<CarListingEditViewModel?> GetCarListingForEditAsync(int id);

        Task<bool> EditCarListingAsync(CarListingEditViewModel model, string? userId);

        Task<CarListingDeleteViewModel?> GetCarListingForDeleteAsync(int id, string? userId);

        Task<bool> DeleteCarListingAsync(CarListingDeleteViewModel model, string? userId);


    }
}
