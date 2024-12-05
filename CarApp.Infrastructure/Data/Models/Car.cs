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
        public CarModel Model { get; set; } = null!;

        [Required]
        public int CarBodyId { get; set; }
        [ForeignKey(nameof(CarBodyId))]
        public CarBodyType CarBodyType { get; set; } = null!;

        public string? Trim { get; set; }

        [Required]
        [Range(50, 10000)]
        public int EngineDisplacement { get; set; }

        [Required]
        public int Whp { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int FuelId { get; set; }
        [ForeignKey(nameof(FuelId))]

        public CarFuelType Fuel { get; set; } = null!;

        public int GearId { get; set; }
        [ForeignKey(nameof(GearId))]
        public CarGear Gear { get; set; } = null!;

        public int DrivetrainId { get; set; }
        [ForeignKey(nameof(DrivetrainId))]
        public CarDrivetrain Drivetrain { get; set; } = null!;

        public CarListing CarListing { get; set; } = null!;

    }
}
