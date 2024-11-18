using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;
using CarApp.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.Services.Contracts
{
    public interface IUtilityService
    {
        Task<CarViewModel> PopulateDropdownsAsync();
        Task<List<CarFuelType>> GetFuelTypesAsync();
        Task<List<CarModel>> GetModelsAsync();
        Task<List<IGrouping<string, CarBrand>>> GetBrandsAsync();
        Task<List<CarGear>> GetGearsAsync();
        Task<List<CarDrivetrain>> GetDrivetrainAsync();
        Task<List<CarBodyType>> GetBodyTypesAsync();
        List<int> GetPriceDropdown();
        List<int> GetMileageDropdown();

        Task<T> PopulateAllDropdownsAsync<T>(T viewModel) where T : DropDownViewModel, new();
    }
}
