using CarApp.Core.Services.Contracts;
using CarApp.Core.ViewModels.Admin.DataManagement;
using CarApp.Core.ViewModels.Admin.UserManagement;
using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IRepository<CarBrand, int> brandRepository;
        private readonly IRepository<CarModel, int> modelRepository;

        public AdminService(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager,
            IRepository<CarBrand, int> _brandRepository, IRepository<CarModel, int> _modelRepository)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            brandRepository = _brandRepository;
            modelRepository = _modelRepository;
        }

        public async Task<bool> AssignUserToRoleAsync(string userId, string roleName)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);
            bool roleExist = await roleManager.RoleExistsAsync(roleName);

            if (user == null || !roleExist)
            {
                return false;
            }

            bool alreadyInRole = await userManager.IsInRoleAsync(user, roleName);
            if (!alreadyInRole)
            {
                IdentityResult result = await userManager.AddToRoleAsync(user, roleName);

                if (!result.Succeeded)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            IdentityResult result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<AllBrandsViewModel>> GetAllBrandsAsync()
        {

            IEnumerable<AllBrandsViewModel> allBrands = await brandRepository
                .GetAllAttached()
                .Select(b => new AllBrandsViewModel()
                {
                    BrandId = b.Id,
                    BrandName = b.BrandName
                })
                .ToListAsync();
            return allBrands;
        }

        public async Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync(string currentUserId)
        {

            IEnumerable<ApplicationUser> allUsers = await userManager.Users
                .Where(u => u.Id != currentUserId).ToListAsync();

            ICollection<AllUsersViewModel> allUsersViewModels = new List<AllUsersViewModel>();

            foreach (ApplicationUser user in allUsers)
            {
                IEnumerable<string> roles = await userManager.GetRolesAsync(user);

                allUsersViewModels.Add(new AllUsersViewModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName ?? string.Empty,
                    LastName = user.LastName ?? string.Empty,
                    Email = user.Email,
                    Roles = roles
                });
            }
            return allUsersViewModels;
        }

        public async Task<BrandModelsViewModel?> GetModelsByBrandIdAsync(int brandId)
        {
            BrandModelsViewModel? brandModels = await modelRepository
                .GetAllAttached()
                .Where(bm => bm.BrandId == brandId)
                .Select(bm => new BrandModelsViewModel()
                {
                    BrandId = bm.BrandId,
                    BrandName = bm.CarBrand.BrandName,
                    CarModels = bm.CarBrand.CarModels
                        .Select(m => new AllModelsViewModel()
                        {
                            ModelId = m.Id,
                            ModelName = m.ModelName
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();


            return brandModels;

        }

        public async Task<bool> RemoveUserFromRoleAsync(string userId, string roleName)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);
            bool roleExist = await roleManager.RoleExistsAsync(roleName);

            if (user == null || !roleExist)
            {
                return false;
            }

            bool alreadyInRole = await userManager.IsInRoleAsync(user, roleName);
            if (alreadyInRole)
            {
                IdentityResult result = await userManager.RemoveFromRoleAsync(user, roleName);

                if (!result.Succeeded)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> UserExistsByIdAsync(string userId)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);

            return user != null;
        }
    }
}
