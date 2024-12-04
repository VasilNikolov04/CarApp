using CarApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static CarApp.Infrastructure.Constants.DataErrorMessages.Car;
using static CarApp.Infrastructure.Constants.DataConstants.Car;

namespace CarApp.Core.ViewModels
{
    public class CarListingEditViewModel
    {
        /// <summary>
        /// CarListing identifier
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int Id { get; set; }

        /// <summary>
        /// Car horsepower
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int Whp { get; set; }

        /// <summary>
        /// Images for the car
        /// </summary>

        [Required(ErrorMessage = RequiredErrorMessage)]
        public IList<CarImage> CarImages { get; set; } = new List<CarImage>();

        [Required(ErrorMessage = RequiredErrorMessage)]
        public IList<IFormFile>? NewCarImages { get; set; } = new List<IFormFile>();
        /// <summary>
        /// Car Price
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int Price { get; set; }

        /// <summary>
        /// Car Description
        /// </summary>
        [MaxLength(CarListingDescriptionMaxLength)]
        public string? Description { get; set; } = string.Empty;

        /// <summary>
        /// Car Milleage
        /// </summary>

        [Required(ErrorMessage = RequiredErrorMessage)]
        public int Milleage { get; set; }


        /// <summary>
        /// Is the car deleted
        /// </summary>

        [Required(ErrorMessage = RequiredErrorMessage)]
        public bool IsDeleted { get; set; }
    }
}
