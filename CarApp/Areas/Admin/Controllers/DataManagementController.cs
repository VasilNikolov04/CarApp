using CarApp.Core.Services;
using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels.Admin.DataManagement;
using Microsoft.AspNetCore.Mvc;

namespace CarApp.Areas.Admin.Controllers
{
    public class DataManagementController : AdminBaseController
    {
        private readonly IAdminService adminService;
        private readonly IUtilityService utilityService;

        public DataManagementController(IAdminService _adminService)
        {
            adminService = _adminService;   
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllBrandsViewModel> allBrands = await adminService.GetAllBrandsAsync();
            return View(allBrands);
        }

        [HttpGet]
        public async Task<IActionResult> BrandModels(int brandId)
        {
            BrandModelsViewModel? models = await adminService
                .GetModelsByBrandIdAsync(brandId);

            if (models == null)
            {
                return BadRequest();
            }
            return View(models);
        }
    }
}
