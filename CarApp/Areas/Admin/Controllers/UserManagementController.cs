using CarApp.Core.Services;
using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels.Admin.UserManagement;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarApp.Areas.Admin.Controllers
{
    public class UserManagementController : AdminBaseController
    {
        private readonly IAdminService adminService;
        public UserManagementController(IAdminService _adminService)
        {
            adminService = _adminService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null)
            {
                return BadRequest();
            }
            IEnumerable<AllUsersViewModel> model 
                = await adminService.GetAllUsersAsync(userId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            bool userExist = await adminService.UserExistsByIdAsync(userId);
            if(!userExist)
            {
                return RedirectToAction(nameof(Index));
            }
            bool assignResult = await adminService
                .AssignUserToRoleAsync(userId, role);

            if (!assignResult)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string role)
        {
            bool userExists = await adminService
                .UserExistsByIdAsync(userId);
            if (!userExists)
            {
                return RedirectToAction(nameof(Index));
            }

            bool removeResult = await adminService
                .RemoveUserFromRoleAsync(userId, role);

            if (!removeResult)
            {
                return RedirectToAction(nameof(Index));
            }
            TempData["RemoveRoleMessage"] = $"User successfully Removed from role {role}";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            bool userExists = await adminService
                .UserExistsByIdAsync(userId);
            if (!userExists)
            {
                return RedirectToAction(nameof(Index));
            }

            bool deleteResult = await adminService
                .DeleteUserAsync(userId);

            if (!deleteResult)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
