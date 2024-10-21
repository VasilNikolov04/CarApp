using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Infrastructure.Constants
{
    public static class DataConstants
    {

        public const int UserFirstNameMaxLength = 50;

        public const int UserLastNameMaxLength = 50;

        public static class Car
        {
            public const int CarListingDescriptionMaxLength = 500;

            public const int CarBrandNameMaxLength = 50;
            public const int CarModelNameMaxLength = 50;
            public const int CarCategoryNameMaxLength = 30;

            public const int FuelNameMinLength = 3;
            public const int FuelNameMaxLength = 30;

            public const int ImageUrlMaxLength = 255;

            public const int CityNameMinLength = 3;
            public const int CityNameMaxLength = 50;

            public const int RegionNameMinLength = 3;
            public const int RegionNameMaxLength = 50;

            public const int CountryNameMinLength = 3;
            public const int CountryNameMaxLength = 50;

            public const int CarYearMinRange = 1930;

            public const int CarGearMaxLength = 20;

            public const int DrivetrainNameMaxLength = 25;
        }
    }
}
