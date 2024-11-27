using CarApp.Core.ViewModels.Admin.DataManagement;
using CarApp.Core.ViewModels.Admin.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.Services.Contracts
{
    public interface IAdminService
    {
        Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync(string currentUserId);
        Task<bool> UserExistsByIdAsync(string userId);
        Task<bool> AssignUserToRoleAsync(string userId, string roleName);
        Task<bool> RemoveUserFromRoleAsync(string userId, string roleName);
        Task<bool> DeleteUserAsync(string userId);
        Task<IEnumerable<AllBrandsViewModel>> GetAllBrandsAsync();
        Task<BrandModelsViewModel?> GetModelsByBrandIdAsync(int brandId);
        Task<bool> EditBrandNameAsync(int brandId, string brandName);
        Task<bool> EditModelNameAsync(int modelId, string modelName);
        Task<bool> AddNewModelAsync(int brandId, string newModelName);
        Task<bool> DeleteModelByIdAsync(int modelId);
    }
}
