using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.ViewModels.Home
{
    public class HomePageViewModel
    {
        public IEnumerable<FeaturedCarsViewModel> LatestCars { get; set; } 
            = new List<FeaturedCarsViewModel>();
        public int TotalCarsListed { get; set; }
        public int AllUsers { get; set; }
        public int AllSellers { get; set; }
    }
}
