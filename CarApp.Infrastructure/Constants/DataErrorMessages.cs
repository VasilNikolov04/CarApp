using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Infrastructure.Constants
{
    public static class DataErrorMessages
    {
        public static class Car
        {
            public const string RequiredErrorMessage = "The {0} field is required";

            public const string LengthErrorMessage = "The {0} field must be between {2} and {1} characters long";
        }
    }
}
