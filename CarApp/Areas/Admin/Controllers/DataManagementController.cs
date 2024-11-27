using CarApp.Core.Services;
using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels.Admin.DataManagement;
using CarApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        [HttpPost]
        public async Task<IActionResult> EditBrand(int brandId, string brandName)
        {
            bool result = await adminService.EditBrandNameAsync(brandId, brandName);

            if(result == false)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> EditModel(int modelId, string modelName, int brandId)
        {
            bool result = await adminService.EditModelNameAsync(modelId, modelName);

            if (result == false)
            {
                return RedirectToAction(nameof(BrandModels), new { brandId });
            }

            return RedirectToAction(nameof(BrandModels), new { brandId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel(int brandId, string newModelName)
        {
            if (string.IsNullOrWhiteSpace(newModelName))
            {
                ModelState.AddModelError("newModelName", "Model name is required.");
                return RedirectToAction("BrandModels", new { brandId });
            }

            bool result = await adminService.AddNewModelAsync(brandId, newModelName);

            if(result == false)
            {
                return RedirectToAction(nameof(BrandModels), new { brandId });
            }

            return RedirectToAction(nameof(BrandModels), new { brandId });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteModel(int modelId, int brandId)
        { 

            bool result = await adminService.DeleteModelByIdAsync(modelId);
            if(result == false)
            {
                return RedirectToAction(nameof(BrandModels), new {brandId});
            }

            return RedirectToAction(nameof(BrandModels), new { brandId });
        }
    }
}
