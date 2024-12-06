using CarApp.Core.Enumerations;
using CarApp.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CarApp.Core.ViewModels.CarListing
{
    public class AllCarsQueryModel
    {
        public int CarsPerPage { get; } = 10;

        public CarListingSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalCarsCount { get; set; }
         
        public string? Brand { get; set; }
        public List<IGrouping<string, CarBrand>> Brands { get; set; } 
            = new List<IGrouping<string, CarBrand>>();

        public string? Model { get; set; }
        public ICollection<CarModel> Models { get; set; } 
            = new List<CarModel>();

        public int PriceLimit { get; set; }
        public ICollection<int> PriceList { get; set; }
            = new List<int>();

        public IEnumerable<CarInfoViewModel> CarListings { get; set; }
            = new List<CarInfoViewModel>();
    }
}
