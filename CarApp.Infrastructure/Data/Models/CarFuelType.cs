using System.ComponentModel.DataAnnotations;
using static CarApp.Infrastructure.Constants.DataConstants.Car;

namespace CarApp.Infrastructure.Data.Models
{
    public class CarFuelType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(FuelNameMaxLength)]
        public required string FuelName { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}