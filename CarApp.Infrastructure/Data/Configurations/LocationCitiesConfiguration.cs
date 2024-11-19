using CarApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Infrastructure.Data.Configurations
{
    public class LocationCitiesConfiguration : IEntityTypeConfiguration<CarLocationCity>
    {
        public void Configure(EntityTypeBuilder<CarLocationCity> builder)
        {
            builder.HasData(SeedLocationCities());
        }
        private IEnumerable<CarLocationCity> SeedLocationCities()
        {
            IEnumerable<CarLocationCity> cities = new List<CarLocationCity>()
            {
                new CarLocationCity()
                {
                    Id = 1,
                    CityName = "USA",
                    LocationId = 1
                },
                new CarLocationCity()
                {
                    Id = 2,
                    CityName = "Germany",
                    LocationId = 1
                },
                new CarLocationCity()
                {
                    Id = 3,
                    CityName = "Italy",
                    LocationId = 1
                },
                new CarLocationCity()
                {
                    Id = 4,
                    CityName = "Japan",
                    LocationId = 1
                },
                new CarLocationCity()
                {
                    Id = 5,
                    CityName = "Blagoevgrad",
                    LocationId = 2
                },
                new CarLocationCity()
                {
                    Id = 6,
                    CityName = "Bansko",
                    LocationId = 2
                },
                new CarLocationCity()
                {
                    Id = 7,
                    CityName = "Sofia",
                    LocationId = 3
                },
                new CarLocationCity()
                {
                    Id = 8,
                    CityName = "Botevgrad",
                    LocationId = 3
                },
                new CarLocationCity()
                {
                    Id = 9,
                    CityName = "Plovdiv",
                    LocationId = 4
                },
                new CarLocationCity()
                {
                    Id = 10,
                    CityName = "Asenovgrad",
                    LocationId = 4
                },
                new CarLocationCity()
                {
                    Id = 11,
                    CityName = "UK",
                    LocationId = 1
                }


            };

            return cities;
        }
    }
}
