using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.ViewModels.Admin.DataManagement
{
    public class BrandAddInputViewModel
    {
        public string BrandName { get; set; } = null!;
        public List<string> Models { get; set; } = null!;
    }
}
