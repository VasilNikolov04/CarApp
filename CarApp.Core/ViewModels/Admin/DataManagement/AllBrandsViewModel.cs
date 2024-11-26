using CarApp.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.ViewModels.Admin.DataManagement
{
    public class AllBrandsViewModel
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
    }
}
