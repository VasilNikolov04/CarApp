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

        [Required]
        public int Whp { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int FuelId { get; set; }
        [ForeignKey(nameof(FuelId))]

        public required CarFuelType Fuel { get; set; }

        public int GearId { get; set; }
        [ForeignKey(nameof(GearId))]
        public CarGear? Gear { get; set; }

        public int DrivetrainId { get; set; }
        [ForeignKey(nameof(DrivetrainId))]
        public CarDrivetrain? Drivetrain { get; set; }

        public CarListing CarListing { get; set; }

    }
}
