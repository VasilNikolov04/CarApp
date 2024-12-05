using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.ViewModels.CarListing
{
    public class CarImageViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int CarListingId { get; set; }
        public int Order { get; set; }
    }
}
