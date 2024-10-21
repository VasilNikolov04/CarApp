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
    public class GearConfiguration : IEntityTypeConfiguration<CarGear>
    {
        public void Configure(EntityTypeBuilder<CarGear> builder)
        {
            builder.HasData(SeedGears());
        }
        private IEnumerable<CarGear> SeedGears()
        {
            IEnumerable<CarGear> gears = new List<CarGear>()
            {
                new CarGear()
                {
                    Id = 1,
                    GearName = "Manual"
                },
                new CarGear()
                {
                    Id = 2,
                    GearName = "Automatic"
                },
                new CarGear()
                {
                    Id = 3,
                    GearName = "Semi-Automatic"
                },

            };

            return gears;

        }
    }
}
