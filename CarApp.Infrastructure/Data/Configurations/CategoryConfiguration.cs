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
    public class CategoryConfiguration : IEntityTypeConfiguration<CarBodyType>
    {
        public void Configure(EntityTypeBuilder<CarBodyType> builder)
        {
            builder.HasData(SeedDrivetrain());
        }
        private IEnumerable<CarBodyType> SeedDrivetrain()
        {
            IEnumerable<CarBodyType> categories = new List<CarBodyType>()
            {
                new CarBodyType()
                {
                    Id = 1,
                    Name = "Convertible"
                },
                new CarBodyType()
                {
                    Id = 2,
                    Name = "Coupe"
                },
                new CarBodyType()
                {
                    Id = 3,
                    Name = "SUV"
                },
                new CarBodyType()
                {
                    Id = 4,
                    Name = "Sedan"
                },
                new CarBodyType()
                {
                    Id = 5,
                    Name = "Van"
                },
                new CarBodyType()
                {
                    Id = 6,
                    Name = "Hatchback"
                },
                new CarBodyType()
                {
                    Id = 7,
                    Name = "Station Wagon"
                },
                new CarBodyType()
                {
                    Id = 8,
                    Name = "Pickup Truck"
                },
                new CarBodyType()
                {
                    Id = 9,
                    Name = "Compact"
                },
                new CarBodyType()
                {
                    Id = 10,
                    Name = "Other"
                }


            };

            return categories;
        }
    }
}
