using CarApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Infrastructure.Data.SeedDb
{
    internal class FuelSeed : IEntityTypeConfiguration<CarFuelType>
    {
        public void Configure(EntityTypeBuilder<CarFuelType> builder)
        {
            builder.HasData(SeedFuelTypes());
        }

        private IEnumerable<CarFuelType> SeedFuelTypes()
        {
            IEnumerable<CarFuelType> fuelTypes = new List<CarFuelType>()
            {
                new CarFuelType()
                {
                    Id = 1,
                    FuelName = "Hybrid(Electric/Gasoline)"
                },
                new CarFuelType()
                {   
                    Id = 2,
                    FuelName = "Hybrid(Electric/Diesel)"
                },
                new CarFuelType()
                {
                    Id = 3,
                    FuelName = "Gasoline"
                },
                new CarFuelType()
                {
                    Id = 4,
                    FuelName = "Diesel"
                },
                new CarFuelType()
                {
                    Id = 5,
                    FuelName = "Electric"
                },
                new CarFuelType()
                {
                    Id = 6,
                    FuelName = "Ethanol"
                },
                new CarFuelType()
                {
                    Id = 7,
                    FuelName = "Hydrogen"
                },
                new CarFuelType()
                {
                    Id = 8,
                    FuelName = "Other"
                }
            };

            return fuelTypes;

        }
    }
}
