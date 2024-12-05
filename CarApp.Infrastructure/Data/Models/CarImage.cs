using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CarApp.Infrastructure.Constants.DataConstants.Car;

namespace CarApp.Infrastructure.Data.Models
{
    public class CarImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public required string ImageUrl { get; set; }

        [Required]
        public int CarListingId { get; set; }
        [ForeignKey(nameof(CarListingId))]
        public CarListing CarListing { get; set; } = null!;
        public int Order { get; set; }
    }
}
