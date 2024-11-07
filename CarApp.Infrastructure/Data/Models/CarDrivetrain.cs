using System.ComponentModel.DataAnnotations;
using static CarApp.Infrastructure.Constants.DataConstants.Car;

namespace CarApp.Infrastructure.Data.Models
{
    public class CarDrivetrain
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DrivetrainNameMaxLength)]
        public required string DrivetrainName { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}