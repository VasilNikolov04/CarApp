using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.ViewModels.Home
{
    public class FeaturedCarsViewModel
    {
        public int Id { get; set; }

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string Trim { get; set; } = null!;

        public int Year { get; set; }

        public string Price { get; set; } = null!;

        public string DatePosted { get; set; } = null!;

        public string LocationRegion { get; set; } = null!;

        public string LocationCity { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
    }
}
