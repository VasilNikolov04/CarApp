using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static CarApp.Core.Services.BrandAndModelSeedService;
using CarApp.Core.Services.Contracts;

namespace CarApp.Core.Services
{
    public class BrandAndModelSeedService : IBrandAndModelSeedService
    {
        private readonly IRepository<CarModel, int> modelRepository;
        private readonly IRepository<CarBrand, int> brandRepository;
        public BrandAndModelSeedService(IRepository<CarModel, int> _modelRepository,
            IRepository<CarBrand, int> _brandRepository)
        {
            modelRepository = _modelRepository;
            brandRepository = _brandRepository;
        }

        public List<CarBrandDto> GetCarBrandData()
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\..\..\CarApp.Infrastructure\Data\SeedData\BrandSeed.json");
            string jsonData = File.ReadAllText(jsonFilePath);
            var carBrandList = JsonConvert.DeserializeObject<List<CarBrandDto>>(jsonData);


            return carBrandList;
        }

        public async Task SeedBrandsAndModelsFromJson()
        {
            var carBrandData = GetCarBrandData();
            if (carBrandData == null || !carBrandData.Any())
                return;

            // Seed CarBrands with IDs explicitly
            var existingBrands = await brandRepository.GetAllAttached().ToListAsync();
            var newBrands = new List<CarBrand>();

            for (int i = 1; i <= carBrandData.Count; i++)
            {
                var brandDto = carBrandData[i - 1];

                if (!existingBrands.Any(b => b.BrandName
                .Equals(brandDto.Brand, StringComparison.OrdinalIgnoreCase)))
                {
                    var carBrand = new CarBrand
                    {
                        BrandName = brandDto.Brand
                    };
                    newBrands.Add(carBrand);
                }
            }

            if (newBrands.Any())
            {
                var newBrandsArray = newBrands.ToArray(); // Convert List<CarBrand> to CarBrand[]
                brandRepository.AddRange(newBrandsArray);
                existingBrands.AddRange(newBrands);
            }

            // Seed CarModels
            var existingModels = await modelRepository.GetAllAttached().ToListAsync();
            var modelsToSeed = new List<CarModel>();

            foreach (var brandDto in carBrandData)
            {
                var brand = existingBrands
                    .FirstOrDefault(b => b.BrandName
                    .Equals(brandDto.Brand, StringComparison.OrdinalIgnoreCase));
                if (brand != null && brandDto.Models != null)
                {
                    var models = brandDto.Models
                        .Where(modelName => !existingModels
                        .Any(m => m.ModelName
                        .Equals(modelName, StringComparison.OrdinalIgnoreCase) && m.BrandId == brand.Id))
                        .Select(modelName => new CarModel { ModelName = modelName, BrandId = brand.Id });

                    modelsToSeed.AddRange(models);
                }
            }

            if (modelsToSeed.Any())
            {
                var modelsToSeedArray = modelsToSeed.ToArray(); // Convert List<CarModel> to CarModel[]
                modelRepository.AddRange(modelsToSeedArray);
            }
        }

        public class CarBrandDto
        {
            public string Brand { get; set; }
            public List<string> Models { get; set; }
        }
    }
}
