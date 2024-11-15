using CarApp.Core.Services;
using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.RefinedSearch;
using Microsoft.AspNetCore.Mvc;

namespace CarApp.Controllers
{
    public class RefinedSearch : Controller
    {
        private readonly IUtilityService utilityService;

        public RefinedSearch(IUtilityService _utilityService)
        {
            utilityService = _utilityService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await utilityService.PopulateAllDropdownsAsync(new RefinedSearchViewModel());

            return View(model);
        }
    }
}
