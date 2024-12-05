using CarApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static CarApp.Infrastructure.Constants.DataErrorMessages.Car;

namespace CarApp.Core.ViewModels
{
    /// <summary>
    /// Car Listing data transfer model
    /// </summary>
    public class CarViewModel : DropDownViewModel
    {
        /// <summary>
        /// Car Brand
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int BrandId { get; set; }
        public List<IGrouping<string, CarBrand>> Brands { get; set; } = new List<IGrouping<string, CarBrand>>();

        /// <summary>
        /// Car Model
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int ModelId { get; set; }
        public List<CarModel> Models { get; set; } = new List<CarModel>();


        /// <summary>
        /// Car horsepower
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int Whp { get; set; }


        /// <summary>
        /// Images for the car
        /// </summary>

        [Required(ErrorMessage = RequiredErrorMessage)]
        public List<IFormFile> CarImages { get; set; } = new List<IFormFile>();
        /// <summary>
        /// Car Price
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int Price { get; set; }

        /// <summary>
        /// Car Trim
        /// </summary>
        public string? Trim { get; set; }

        /// <summary>
        /// Car Engine displacement
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(50,10000)]
        public int EngineDisplacement { get; set; }

        /// <summary>
        /// Car Description
        /// </summary>
        public string? Description { get; set; }= string.Empty;

        /// <summary>
        /// Car Year
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int Year { get; set; }
        public List<int> YearList { get; set; } = new List<int>();

        /// <summary>
        /// Car Milleage
        /// </summary>

        [Required(ErrorMessage = RequiredErrorMessage)]
        public int Milleage { get; set; }

        /// <summary>
        /// Car Fuel
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int FuelId { get; set; }
        public List<CarFuelType> FuelTypes { get; set; } = new List<CarFuelType>();

        /// <summary>
        /// Car Transmission
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int GearId { get; set; }
        public List<CarGear> Gears { get; set; } = new List<CarGear>();

        /// <summary>
        /// Car Category
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int CarBodyId { get; set; }
        public List<CarBodyType> CarBodyTypes { get; set; } = new List<CarBodyType>();

        /// <summary>
        /// Car Drivetrain
        /// </summary>

        public int DrivetrainId { get; set; }
        public List<CarDrivetrain> Drivetrains { get; set; } = new List<CarDrivetrain>();


        /// <summary>
        /// Car Locations
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int CarLocationId { get; set; }
        public List<CarLocationRegion> CarLocations { get; set; } = new List<CarLocationRegion>();

        /// <summary>
        /// Location City
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int CityId { get; set; }
        public List<CarLocationCity> Cities { get; set; } = new List<CarLocationCity>();
    }
}
