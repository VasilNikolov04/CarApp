using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarApp.Core.ViewModels
{
    public class CarInfoViewModel
    {
        public int id { get; set; }
        public string Model { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string Price { get; set; } = null!;

        public int whp { get; set; }

        public string FuelType { get; set; } = null!;

        public string DatePosted { get; set; } = null!;

        public string SellerId { get; set; } = null!;

        public string? GearType { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}
