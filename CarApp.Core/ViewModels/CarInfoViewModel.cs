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
        public int Id { get; set; }
        public string Model { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string? Trim { get; set; }

        public string Price { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int Whp { get; set; }

        public int Year { get; set; }

        public string Milleage { get; set; } = null!;

        public string BodyType { get; set; } = null!;

        public string FuelType { get; set; } = null!;

        public string DatePosted { get; set; } = null!;

        public string SellerId { get; set; } = null!;

        public string GearType { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string LocationRegion { get; set; } = null!;

        public string LocationTown { get; set; } = null!;

        public int EngineDisplacement { get; set; }
    }
}
