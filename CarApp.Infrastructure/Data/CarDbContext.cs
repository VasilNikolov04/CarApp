using CarApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection;

namespace CarApp.Infrastructure.Data
{
    public class CarDbContext : IdentityDbContext<ApplicationUser>
    {
        public CarDbContext(DbContextOptions<CarDbContext> options)
        : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<CarListing>()
                .Property(p => p.Price)
                .HasConversion<decimal>();
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<CarListing> CarListings { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarCategory> CarCategories { get; set; }
        public DbSet<CarFuelType> CarFuelTypes { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<CarGear> CarGears { get; set; }
        public DbSet<CarDrivetrain> CarDrivetrains { get; set; }

        public DbSet<CarLocation> CarLocations { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
    }
}
