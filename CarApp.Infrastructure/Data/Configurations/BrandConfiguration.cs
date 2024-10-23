using CarApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace CarApp.Infrastructure.Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<CarBrand>
    {
        public void Configure(EntityTypeBuilder<CarBrand> builder)
        {
            var carBrandData = GetCarBrandData();
            for (int i = 1; i <= carBrandData.Count; i++)
            {
                var carBrand = new CarBrand
                {
                    Id = i,
                    BrandName = carBrandData[i-1].Brand
                };
                builder.HasData(carBrand);
            }
        }

        private List<CarBrandDto> GetCarBrandData()
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                @"..\..\..\..\CarApp.Infrastructure\Data\SeedData\BrandModelSeed.json");
            string jsonData = File.ReadAllText(jsonFilePath);
            var carBrandList = JsonConvert.DeserializeObject<List<CarBrandDto>>(jsonData);


            return carBrandList;
        }
        public class CarBrandDto
        {
            public string Brand { get; set; }
            public List<string> Models { get; set; }
        }
    }
}
