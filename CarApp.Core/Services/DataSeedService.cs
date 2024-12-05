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
using static CarApp.Core.Services.DataSeedService;
using CarApp.Core.Services.Contracts;
using System.Text.Json;

namespace CarApp.Core.Services
{
    public class DataSeedService : IDataSeedService
    {
        private readonly IRepository<CarModel, int> modelRepository;
        private readonly IRepository<CarBrand, int> brandRepository;
        private readonly IRepository<CarLocationRegion, int> locationRepository;
        private readonly IRepository<CarLocationCity, int> cityRepository;
        private readonly HttpClient client;
        public DataSeedService(IRepository<CarModel, int> _modelRepository,
            IRepository<CarBrand, int> _brandRepository,
            IRepository<CarLocationRegion, int> _locationRepository,
            IRepository<CarLocationCity, int> _cityRepository,
            HttpClient _client)
        {
            modelRepository = _modelRepository;
            brandRepository = _brandRepository;
            locationRepository = _locationRepository;
            cityRepository = _cityRepository;
            client = _client;
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

        public async Task SeedCitiesAndRegionsFromApi()
        {
            var regions = locationRepository.GetAll();
            if (regions == null)
            {
                var regionsRequest = new HttpRequestMessage(HttpMethod.Post, "https://countriesnow.space/api/v0.1/countries/states");
                var regionsContent = new StringContent("{\"country\": \"Bulgaria\"}", Encoding.UTF8, "application/json");
                regionsRequest.Content = regionsContent;

                try
                {
                    var regionsResponse = await client.SendAsync(regionsRequest);
                    regionsResponse.EnsureSuccessStatusCode();

                    var regionsResponseContent = await regionsResponse.Content.ReadAsStringAsync();
                    var regionsJson = JsonDocument.Parse(regionsResponseContent);

                    if (regionsJson.RootElement.TryGetProperty("data", out var data) && data.ValueKind == JsonValueKind.Object)
                    {
                        var states = data.GetProperty("states").EnumerateArray();

                        foreach (var state in states)
                        {
                            string regionName = state.GetProperty("name").GetString();
                            string formattedRegion = regionName.Replace(" Province", "");

                            var existRegion = await locationRepository
                                .GetAllAttached()
                                .FirstOrDefaultAsync(r => r.RegionName == formattedRegion);
                            var region = new CarLocationRegion { RegionName = formattedRegion };
                            if (existRegion == null)
                            {
                                await locationRepository.AddAsync(region);
                            }



                            var citiesRequest = new HttpRequestMessage(HttpMethod.Post, "https://countriesnow.space/api/v0.1/countries/state/cities");
                            var citiesContent = new StringContent($"{{\"country\": \"Bulgaria\",\"state\": \"{regionName}\"}}", Encoding.UTF8, "application/json");
                            citiesRequest.Content = citiesContent;

                            var citiesResponse = await client.SendAsync(citiesRequest);
                            citiesResponse.EnsureSuccessStatusCode();

                            var citiesResponseContent = await citiesResponse.Content.ReadAsStringAsync();
                            var citiesJson = JsonDocument.Parse(citiesResponseContent);

                            if (citiesJson.RootElement.GetProperty("error").GetBoolean() == false)
                            {
                                var cities = citiesJson.RootElement.GetProperty("data").EnumerateArray();

                                foreach (var city in cities)
                                {
                                    if (!city.ToString().Contains("Obshtina"))
                                    {
                                        string cityName = city.GetString();

                                        var existCity = await cityRepository
                                            .GetAllAttached()
                                            .FirstOrDefaultAsync(r => r.CityName == cityName);
                                        if (existCity == null)
                                        {
                                            var cityEntity = new CarLocationCity { CityName = cityName, LocationId = region.Id };
                                            await cityRepository.AddAsync(cityEntity);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                throw new ArgumentNullException("No cities found or error occurred.");
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Error: Could not find 'data' as an object with 'states' in the regions response.");
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new ArgumentException($"Request error: {e.Message}");
                }
                catch (Exception e)
                {
                    throw new ArgumentException($"An error occurred: {e.Message}");
                }
            }
        }

        public class CarBrandDto
        {
            public string Brand { get; set; }
            public List<string> Models { get; set; }
        }
    }
}
