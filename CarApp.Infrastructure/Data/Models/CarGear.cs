using System.ComponentModel.DataAnnotations;
using static CarApp.Infrastructure.Constants.DataConstants.Car;

namespace CarApp.Infrastructure.Data.Models
{
    public class CarGear
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CarGearMaxLength)]
        public required string GearName { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}