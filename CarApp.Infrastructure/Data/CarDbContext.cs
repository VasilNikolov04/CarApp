using CarApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection;
using CarApp.Infrastructure.Data.Configurations;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;

namespace CarApp.Infrastructure.Data
{
    public class CarDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarListing> CarListings { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarBodyType> CarBodyTypes { get; set; }
        public DbSet<CarFuelType> CarFuelTypes { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<CarGear> CarGears { get; set; }
        public DbSet<CarDrivetrain> CarDrivetrains { get; set; }
        public DbSet<CarLocation> CarLocations { get; set; }
        public DbSet<CarLocationCity> CarLocationCities { get; set; }
        public DbSet<Favourite> Favourites { get; set; }

        public CarDbContext(DbContextOptions<CarDbContext> options)
        : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<CarListing>()
                .Property(p => p.Price)
                .HasConversion<decimal>();
        }
    }

}
