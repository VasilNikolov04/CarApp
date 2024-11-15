using CarApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.ViewModels.RefinedSearch
{
    public class RefinedSearchViewModel : DropDownViewModel
    {

        public string? Brand { get; set; }
        public List<IGrouping<string, CarBrand>> Brands { get; set; } = new List<IGrouping<string, CarBrand>>();

        public string? Model { get; set; }
        public List<CarModel> Models { get; set; } = new List<CarModel>();


        public int MinWhp { get; set; }
        public int MaxWhp { get; set; }


        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }


        public int MinEngineDisplacement { get; set; }
        public int MaxEngineDisplacement { get; set; }


        public int MinYear { get; set; }
        public int MaxYear { get; set; }


        public int MinMilleage { get; set; }
        public int MaxMilleage { get; set; }

        public string? Fuel { get; set; }
        public List<CarFuelType> FuelTypes { get; set; } = new List<CarFuelType>();


        public string? Gear { get; set; }
        public List<CarGear> Gears { get; set; } = new List<CarGear>();


        public string? CarBody { get; set; }
        public List<CarBodyType> CarBodyTypes { get; set; } = new List<CarBodyType>();


        public string? Drivetrain { get; set; }
        public List<CarDrivetrain> Drivetrains { get; set; } = new List<CarDrivetrain>();
    }
}
