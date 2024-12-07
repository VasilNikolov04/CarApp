using CarApp.Core.Services;
using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels.Admin.DataManagement;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using static CarApp.Infrastructure.Constants.ApplicationConstants;

namespace CarApp.Areas.Admin.Controllers
{
    public class DataManagementController : AdminBaseController
    {
        private readonly IAdminService adminService;
        private readonly IRepository<CarBrand, int> brandRepository;

        public DataManagementController(IAdminService _adminService, IRepository<CarBrand, int> _brandRepository)
        {
            adminService = _adminService;   
            brandRepository = _brandRepository;
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
                return RedirectToAction(nameof(Index));
            }
            return View(models);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteModel(int modelId, int brandId, int modelCount)
        {
            CarBrand? brand = await brandRepository.GetByIdAsync(brandId);
            if(brand == null)
            {
                return BadRequest();
            }

            bool result = await adminService.DeleteModelByIdAsync(modelId, modelCount);
            if(result == false)
            {
                return RedirectToAction(nameof(BrandModels), new {brandId});
            }
            
            return RedirectToAction(nameof(BrandModels), new { brandId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBrandWithModels([FromBody]  BrandAddInputViewModel input)
        {
            if (String.IsNullOrEmpty(input.BrandName) || input.Models == null || input.Models.Count == 0)
            {
                return BadRequest("Brand name and at least one model are required.");
            }
            if (input.Models == null || input.Models.Count == 0)
            {
                return BadRequest("At least one model are required.");
            }

            bool result = await adminService.AddNewBrandWithModelsAsync(input.BrandName, input.Models);

            if(result == false)
            {
                return UnprocessableEntity("Brand name already exists.");
            }
            

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBrand(int brandId)
        {
            
            BrandDeleteViewModel? brand = await adminService.GetBrandForDeleteAsync(brandId);
            
            if(brand == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBrand(BrandDeleteViewModel brandAndModelsToDelete)
        {
            bool IsDeleted = await adminService.DeleteBrandAndModelsAsync(brandAndModelsToDelete);

            if (IsDeleted == false)
            {
                return View(nameof(DeleteBrand));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
