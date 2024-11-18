using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static CarApp.Infrastructure.Constants.DataConstants.Car;

namespace CarApp.Infrastructure.Data.Models
{
    public class CarLocationCity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CityNameMaxLength)]
        public required string CityName { get; set; }

        public int LocationId { get; set; }
        [ForeignKey(nameof(LocationId))]
        public CarLocation CarLocation { get; set; } = null!;

        public ICollection<CarListing> CarListings { get; set; } 
            = new List<CarListing>();
    }
}