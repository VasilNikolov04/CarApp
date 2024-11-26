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
    }
}
