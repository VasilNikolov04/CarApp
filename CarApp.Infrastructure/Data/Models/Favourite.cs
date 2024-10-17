using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Infrastructure.Data.Models
{
    [PrimaryKey(nameof(UserId),nameof(CarListingId))]
    public class Favourite
    {
        [Required]
        public required string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public required ApplicationUser User { get; set; }

        [Required]
        public required int CarListingId { get; set; }

        [ForeignKey(nameof(CarListingId))]
        public required CarListing CarListing { get; set; }
    }
}
