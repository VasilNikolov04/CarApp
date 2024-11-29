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
        public Car Car { get; set; } = null!;

        [MaxLength(CarListingDescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public DateTime DatePosted { get; set; } = DateTime.Now;

        [Required]
        public int CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public CarLocationCity City { get; set; } = null!;



        [MaxLength(ImageUrlMaxLength)]
        [Required]
        public string MainImageUrl { get; set; } = null!;
        public ICollection<CarImage> CarImages { get; set; } = new List<CarImage>();

        public ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();

        public bool IsDeleted { get; set; }

        [Required]
        public required string SellerId { get; set; }
        [ForeignKey(nameof(SellerId))]
        public ApplicationUser Seller { get; set; } = null!;
    }
}
