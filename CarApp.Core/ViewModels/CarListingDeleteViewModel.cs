using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.ViewModels
{
    public class CarListingDeleteViewModel
    {
        public int Id { get; set; }
        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;
    }
}
