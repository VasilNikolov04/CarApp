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
        private readonly IRepository<CarListing, int> carListingRepository;

        public AdminService(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager,
            IRepository<CarBrand, int> _brandRepository, IRepository<CarModel, int> _modelRepository,
            IUtilityService _utilityService, IRepository<CarListing, int> _carListingRepository)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            brandRepository = _brandRepository;
            modelRepository = _modelRepository;
            carListingRepository = _carListingRepository;
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

        public async Task<bool> DeleteUserAsync(DeleteUserViewModel user)
        {
            if(user.UserId == null)
            {
                return false;
            }

            ApplicationUser? userExist = await userManager.FindByIdAsync(user.UserId);

            if (userExist == null)
            {
                return false;
            }

            IdentityResult result = await userManager.DeleteAsync(userExist);

            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> EditBrandNameAsync(int brandId, string brandName)
        {
            var brand = await brandRepository
                .GetAllAttached()
                .Where(b => b.Id == brandId)
                .FirstOrDefaultAsync();

            if(brand == null)
            {
                return false;
            }

            if(brand.BrandName != brandName)
            {
                brand.BrandName = brandName;
                bool result = await brandRepository.UpdateAsync(brand);
            }

            return true;
        }

        public async Task<bool> EditModelNameAsync(int modelId, string modelName)
        {
            var model = await modelRepository
               .GetAllAttached()
               .Where(b => b.Id == modelId)
               .FirstOrDefaultAsync();

            if (model == null)
            {
                return false;
            }

            if (model.ModelName != modelName)
            {
                model.ModelName = modelName;
                bool result = await modelRepository.UpdateAsync(model);
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
                .OrderBy(b => b.BrandName)
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
                        .Select(m =>  new AllModelsViewModel()
                        {
                            ModelId = m.Id,
                            ModelName = m.ModelName
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
            if (brandModels != null)
            {
                brandModels.CarModels = brandModels.CarModels
                .OrderBy(m => ExtractModelSeries(m.ModelName))
                .ThenByDescending(m => m.ModelName.Contains("(All)"))
                .ThenBy(m => m.ModelName);
            }


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

        public async Task<bool> AddNewModelAsync(int brandId, string newModelName)
        {
            CarBrand? brand = await brandRepository.GetByIdAsync(brandId);
            if(brand == null)
            {
                return false;
            }
            CarModel? model = await modelRepository
                .GetAllAttached()
                .Where(m => m.BrandId == brandId && m.ModelName == newModelName)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                var newModel = new CarModel
                {
                    BrandId = brandId,
                    ModelName = newModelName
                };

                await modelRepository.AddAsync(newModel);
            }
            return true;
        }
        public static string ExtractModelSeries(string modelName)
        {
            var parts = modelName.Split(' ');
            return parts[0];
        }

        public async Task<bool> DeleteModelByIdAsync(int modelId, int modelCount)
        {
            CarModel? model = await modelRepository.GetByIdAsync(modelId);

            if (model == null)
            {
                return false;
            }
            CarBrand? brand = await brandRepository.GetByIdAsync(model.BrandId);
            if (brand == null)
            {
                return false;
            }
            bool result = await modelRepository.DeleteAsync(model);

            if (result == false)
            {
                return false;
            }
            modelCount--;
            

            if (modelCount <= 0)
            {
               await brandRepository.DeleteAsync(brand);
            }

            return true;
        }

        public async Task<bool> AddNewBrandWithModelsAsync(string brandName, List<string> models)
        {
            CarBrand? carBrand = await brandRepository
                .GetAllAttached()
                .Where(b => b.BrandName == brandName) 
                .FirstOrDefaultAsync();

            if (carBrand != null)
            {
                return false;
            }

            var newBrand = new CarBrand
            {
                BrandName = brandName,
                CarModels = new List<CarModel>()
            };

            foreach (var modelName in models)
            {
                newBrand.CarModels.Add(new CarModel { ModelName = modelName });
            }

            await brandRepository.AddAsync(newBrand);
            
            return true;
        }

        public async Task<BrandDeleteViewModel?> GetBrandForDeleteAsync(int brandId)
        {
            BrandDeleteViewModel? carBrand = await brandRepository
                .GetAllAttached()
                .Where(b => b.Id == brandId)
                .Select(b => new BrandDeleteViewModel()
                {
                    BrandId = brandId,
                    BrandName = b.BrandName,
                    ModelCount = b.CarModels.Count()
                })
                .FirstOrDefaultAsync();

            return carBrand;
        }

        public async Task<bool> DeleteBrandAndModelsAsync(BrandDeleteViewModel brandAndModelsToDelete)
        {
            CarBrand? carBrand = await brandRepository
                .GetAllAttached()
                .Where(b => b.Id == brandAndModelsToDelete.BrandId)
                .FirstOrDefaultAsync();

            if (carBrand == null)
            {
                return false;
            }
            bool isDeleted = await brandRepository.DeleteAsync(carBrand);

            if (isDeleted == false)
            {
                return false;
            }

            return true;
        }

        public async Task<DeleteUserViewModel?> GetUserForDelete(string userId)
        {

            ApplicationUser? user = await userManager
                .FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            List<DeleteUserCarListingsViewModel>? userCarListings = await carListingRepository
                .GetAllAttached()
                .Where(cl => cl.SellerId == user.Id)
                .Select(cl => new DeleteUserCarListingsViewModel()
                {
                    BrandName = cl.Car.Model.CarBrand.BrandName,
                    ModelName = cl.Car.Model.ModelName,
                    Image =cl.MainImageUrl ?? string.Empty
                })
                .ToListAsync();

            DeleteUserViewModel? model = new DeleteUserViewModel()
            {
                UserId = user.Id,
                UserFirstName = user.FirstName,
                UserLastName = user.LastName,
                UserEmail = user.Email,
                CarListings = userCarListings
            };

            return model;
        }
    }

}
