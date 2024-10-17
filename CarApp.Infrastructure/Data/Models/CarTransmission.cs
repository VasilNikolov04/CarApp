using System.ComponentModel.DataAnnotations;
using static CarApp.Infrastructure.Constants.DataConstants.Car;

namespace CarApp.Infrastructure.Data.Models
{
    public class CarTransmission
    {
        [Key]
        public int TransmissionId { get; set; }

        [Required]
        [MaxLength(CarTransmissionAbbreviation)]
        public required string TransmissionAbbreviation { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}