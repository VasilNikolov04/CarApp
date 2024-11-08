using CarApp.Core.Enumerations;

namespace CarApp.Core.ViewModels.CarListing
{
    public class AllCarsQueryModel
    {
        public int CarsPerPage { get; } = 4;

        public CarListingSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalCarsCount { get; set; }

        public IEnumerable<CarInfoViewModel> CarListings { get; set; }
            = new List<CarInfoViewModel>();
    }
}
