using CarApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Infrastructure.Data.Configurations
{
    public class FavouritesConfiguration : IEntityTypeConfiguration<Favourite>
    {
        public void Configure(EntityTypeBuilder<Favourite> builder)
        {
            builder
           .HasKey(f => new { f.UserId, f.CarListingId });

            builder
                .HasOne(f => f.User)
                .WithMany(u => u.Favourites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(f => f.CarListing)
                .WithMany(cl => cl.Favourites)
                .HasForeignKey(f => f.CarListingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
