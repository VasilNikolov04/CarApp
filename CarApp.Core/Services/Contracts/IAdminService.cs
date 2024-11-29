using CarApp.Core.ViewModels.Admin.DataManagement;
using CarApp.Core.ViewModels.Admin.UserManagement;

namespace CarApp.Core.Services.Contracts
{
    public interface IAdminService
    {
        Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync(string currentUserId);
        Task<bool> UserExistsByIdAsync(string userId);
        Task<bool> AssignUserToRoleAsync(string userId, string roleName);
        Task<bool> RemoveUserFromRoleAsync(string userId, string roleName);
        Task<bool> DeleteUserAsync(DeleteUserViewModel user);
        Task<IEnumerable<AllBrandsViewModel>> GetAllBrandsAsync();
        Task<BrandModelsViewModel?> GetModelsByBrandIdAsync(int brandId);
        Task<bool> EditBrandNameAsync(int brandId, string brandName);
        Task<bool> EditModelNameAsync(int modelId, string modelName);
        Task<bool> AddNewModelAsync(int brandId, string newModelName);
        Task<bool> DeleteModelByIdAsync(int modelId, int modelCount);
        Task<bool> AddNewBrandWithModelsAsync(string brandName, List<string> models);
        Task<BrandDeleteViewModel?> GetBrandForDeleteAsync(int brandId);
        Task<bool> DeleteBrandAndModelsAsync(BrandDeleteViewModel brandAndModelsToDelete);
        Task<DeleteUserViewModel?> GetUserForDelete(string userId);
    }
}
