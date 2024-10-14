using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Infrastructure.Data
{
    public class CarDbContext : IdentityDbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {


            base.OnModelCreating(builder);
        }
    }
}
