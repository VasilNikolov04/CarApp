using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CarApp.Infrastructure.Constants.DataConstants.Car;

namespace CarApp.Infrastructure.Data.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ModelId { get; set; }
        [ForeignKey(nameof(ModelId))]
        public required CarModel Model { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public required CarCategory Category { get; set; }

        public int Whp { get; set; }

        [Required]
        public int FuelId { get; set; }
        [ForeignKey(nameof(FuelId))]

        public required CarFuelType Fuel { get; set; }

        public int TransmissionID { get; set; }
        [ForeignKey(nameof(TransmissionID))]
        public CarTransmission? Transmission { get; set; }

        public required CarListing CarListing { get; set; }

    }
}
