using System.ComponentModel.DataAnnotations;
using static CarApp.Infrastructure.Constants.DataConstants.Car;

namespace CarApp.Infrastructure.Data.Models
{
    public class CarBrand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CarBrandNameMaxLength)]
        public required string BrandName { get; set; }

        public ICollection<CarModel> CarModels { get; set; } 
            = new List<CarModel>();
    }
}