using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.ViewModels
{
    public class CarInfoViewModel
    {
        public string Model { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public int Price { get; set; }

        public int whp { get; set; }

        public string FuelType { get; set; } = null!;

        public string? GearType { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}
