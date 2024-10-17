using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CarApp.Infrastructure.Constants.DataConstants.Car;

namespace CarApp.Infrastructure.Data.Models
{
    public class CarModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CarModelNameMaxLength)]
        public required string ModelName { get; set; }

        public int BrandId { get; set; }
        [ForeignKey(nameof(BrandId))]
        public required CarBrand CarBrand { get; set; }

        public ICollection<Car>? Cars { get; set; }
    }
}