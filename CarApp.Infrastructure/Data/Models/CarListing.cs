using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CarApp.Infrastructure.Constants.DataConstants.Car;

namespace CarApp.Infrastructure.Data.Models
{
    public class CarListing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public required Car Car { get; set; }

        [MaxLength(CarListingDescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        [Required]
        public int LocationId { get; set; }
        [ForeignKey(nameof(LocationId))]
        public required CarLocation Location { get; set; }

        [Required]
        public required int Mileage { get; set; }

        public required ICollection<CarImage> CarImages { get; set; } = new HashSet<CarImage>();

        public required ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
        public bool IsDeleted { get; set; }

        [Required]
        public required string SellerId { get; set; }
        [ForeignKey(nameof(SellerId))]
        public required ApplicationUser Seller { get; set; }
    }
}
