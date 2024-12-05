using System.ComponentModel.DataAnnotations;
using static CarApp.Infrastructure.Constants.DataConstants.Car;

namespace CarApp.Infrastructure.Data.Models
{
    public class CarLocationRegion
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(RegionNameMaxLength)]
        public required string RegionName { get; set; } = null!;

        public ICollection<CarLocationCity> LocationCities { get; set; } 
            = new List<CarLocationCity>();

    }
}