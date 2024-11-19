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
    public class LocationConfiguration : IEntityTypeConfiguration<CarLocation>
    {
        public void Configure(EntityTypeBuilder<CarLocation> builder)
        {
            builder.HasData(SeedLocations());
        }
        private IEnumerable<CarLocation> SeedLocations()
        {
            IEnumerable<CarLocation> locations = new List<CarLocation>()
            {
                new CarLocation()
                {
                    Id = 1,
                    RegionName = "Out of Bulgaria"
                },
                new CarLocation()
                {
                    Id = 2,
                    RegionName = "Blagoevgrad"
                },
                new CarLocation()
                {
                    Id = 3,
                    RegionName = "Sofia"
                },
                new CarLocation()
                {
                    Id = 4,
                    RegionName = "Plovdiv"
                }


            };

            return locations;
        }
    }
}
