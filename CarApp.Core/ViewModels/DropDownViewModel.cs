using CarApp.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.ViewModels
{
    public interface DropDownViewModel
    {
        List<IGrouping<string, CarBrand>> Brands { get; set;}
        List<CarModel> Models { get; set; }
        List<CarFuelType> FuelTypes { get; set; }
        List<CarGear> Gears { get; set; }
        List<CarDrivetrain> Drivetrains { get; set; }
        List<CarBodyType> CarBodyTypes { get; set; }
    }
}
