using CarApp.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.ViewModels
{
    public class CarDetailsViewModel
    {
        public int id { get; set; }
        public string Model { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string? Description { get; set; }

        public int Milleage { get; set; }

        public string Price { get; set; } = null!;

        public int Whp { get; set; }

        public string FuelType { get; set; } = null!;
        public string BodyType { get; set; } = null!;

        public string? GearType { get; set; }

        public string DatePosted { get; set; } = null!;

        public ICollection<CarImage> Images { get; set; } = new List<CarImage>();
    }
}
