using System.ComponentModel.DataAnnotations;
using static CarApp.Infrastructure.Constants.DataConstants.Car;

namespace CarApp.Infrastructure.Data.Models
{
    public class CarLocation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CityNameMaxLength)]
        public required string City { get; set; }

        [MaxLength(RegionNameMaxLength)]
        public string? Region { get; set; }

        [Required]
        [MaxLength(CountryNameMaxLength)]
        public required string Country { get; set; }

    }
}