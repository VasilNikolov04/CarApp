using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarApp.Infrastructure.Constants.DataConstants;

namespace CarApp.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser<string>
    { 

        [MaxLength(UserFirstNameMaxLength)]
        public string? FirstName { get; set; }

        [MaxLength(UserLastNameMaxLength)]
        public string? LastName { get; set; }
        public ICollection<CarListing>? CarListings { get; set; } = new HashSet<CarListing>();

        public ICollection<Favourite>? Favourites { get; set; } = new HashSet<Favourite>();
    }
}
