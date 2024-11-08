using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.ViewModels.CarListing
{
    public class CarListingQueryServiceModel
    {
        public int TotalListingsCount { get; set; }

        public IEnumerable<CarInfoViewModel> CarListings { get; set; } = new List<CarInfoViewModel>();
    }
}
