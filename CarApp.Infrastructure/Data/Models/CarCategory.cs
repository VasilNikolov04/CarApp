using System.ComponentModel.DataAnnotations;
using static CarApp.Infrastructure.Constants.DataConstants.Car;

namespace CarApp.Infrastructure.Data.Models
{
    public class CarCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CarCategoryNameMaxLength)]
        public required string Name { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}