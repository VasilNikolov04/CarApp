using CarApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarApp.Infrastructure.Constants.DataConstants.Car;
using static CarApp.Infrastructure.Constants.DataErrorMessages.Car;

namespace CarApp.Core.ViewModels
{
    /// <summary>
    /// Car Listing data transfer model
    /// </summary>
    public class CarViewModel
    {
        /// <summary>
        /// Car Brand
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int BrandId { get; set; }
        public ICollection<CarBrand> Brands { get; set; } = new List<CarBrand>();

        /// <summary>
        /// Car Model
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int ModelId { get; set; }
        public ICollection<CarModel> Models { get; set; } = new List<CarModel>();


        /// <summary>
        /// Car horsepower
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int Whp { get; set; }


        /// <summary>
        /// Images for the car
        /// </summary>

        [Required(ErrorMessage = RequiredErrorMessage)]
        public ICollection<IFormFile> CarImages { get; set; } = null!;

        /// <summary>
        /// Car Price
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int Price { get; set; }

        /// <summary>
        /// Car Year
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int Year { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public int Milleage { get; set; }

        /// <summary>
        /// Car Fuel
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int FuelId { get; set; }
        public ICollection<CarFuelType> FuelTypes { get; set; } = new List<CarFuelType>();

        /// <summary>
        /// Car Transmission
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int GearId { get; set; }
        public ICollection<CarGear> Gears { get; set; } = new List<CarGear>();

        /// <summary>
        /// Car Category
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int CarBodyId { get; set; }
        public ICollection<CarBodyType> CarBodyTypes { get; set; } = new List<CarBodyType>();

        /// <summary>
        /// Car Drivetrain
        /// </summary>

        [Required(ErrorMessage = RequiredErrorMessage)]
        public int DrivetrainId { get; set; }
        public ICollection<CarDrivetrain> Drivetrains { get; set; } = new List<CarDrivetrain>();





        ///// <summary>
        ///// Car Location City
        ///// </summary>
        //[Required(ErrorMessage = RequiredErrorMessage)]
        //[StringLength(CityNameMaxLength,
        //    MinimumLength = CityNameMinLength,
        //    ErrorMessage = LengthErrorMessage)]
        //public string City { get; set; } = string.Empty;

        ///// <summary>
        ///// Car Location Regionv
        ///// </summary>
        //public string? Region { get; set; }

        //[Required(ErrorMessage = RequiredErrorMessage)]
        //[StringLength(CountryNameMaxLength, 
        //    MinimumLength = CountryNameMinLength,
        //    ErrorMessage = LengthErrorMessage)]

        ///// <summary>
        ///// Car Location Country
        ///// </summary>
        //public string Country { get; set; } = string.Empty;
    }
}
