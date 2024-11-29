using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.ViewModels.Admin.UserManagement
{
    public class DeleteUserCarListingsViewModel
    {
        public string BrandName { get; set; } = null!;

        public string ModelName { get; set; } = null!;

        public string Trim { get; set; } = null!;

        public string Image { get; set; } = null!;
    }
}
