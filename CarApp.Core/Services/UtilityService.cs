using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Infrastructure.Data;
using CarApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.Services
{
    public class UtilityService : IUtilityService
    {
        private readonly CarDbContext context;

        public UtilityService(CarDbContext _context)
        {
            context = _context;
        }

        public async Task<CarViewModel> PopulateDropdownsAsync()
        {

            var model = new CarViewModel
            {
                Brands = await GetBrandsAsync(),
                FuelTypes = await GetFuelTypesAsync(),
                Models = await GetModelsAsync(),
                Gears = await GetGearsAsync(),
                Drivetrains = await GetDrivetrainAsync(),
                CarBodyTypes = await GetBodyTypesAsync()
            };

            return model;
        }
        public async Task<List<CarFuelType>> GetFuelTypesAsync()
        {
            return await context.CarFuelTypes.ToListAsync();
        }
        public async Task<List<CarModel>> GetModelsAsync()
        {
            return await context.CarModels
                .OrderBy(b => b.ModelName)
                .ToListAsync();
        }
        public async Task<List<IGrouping<string, CarBrand>>> GetBrandsAsync()
        {
            return await context.CarBrands
                .OrderBy(b => b.BrandName)
                .GroupBy(b => b.BrandName.Substring(0, 1).ToUpper())
                .ToListAsync();
        }
        public async Task<List<CarGear>> GetGearsAsync()
        {
            return await context.CarGears.ToListAsync();
        }
        public async Task<List<CarDrivetrain>> GetDrivetrainAsync()
        {
            return await context.CarDrivetrains.ToListAsync();
        }
        public async Task<List<CarBodyType>> GetBodyTypesAsync()
        {
            return await context.CarBodyTypes.ToListAsync();
        }

        public List<int> GetPriceDropdown()
        {
            List<int> prices = new List<int>();
            for (int i = 500; i <= 3000; i = i + 500)
            {
                prices.Add(i);
            }

            for(int i = 4000; i <= 10000; i = i + 1000)
            {
                prices.Add(i);
            }

            for(int i = 12500; i <= 20000; i = i + 2500)
            {
                prices.Add(i);
            }
            prices.Add(25000);
            prices.Add(30000);
            prices.Add(40000);
            prices.Add(50000);
            prices.Add(75000);
            prices.Add(100000);
            prices.Order();

            return prices;
        }
    }

}
