namespace CarApp.Core.ViewModels.Admin.UserManagement
{
    using Infrastructure.Data.Models;
    public class DeleteUserViewModel
    {
        public string? UserId { get; set; }

        public string? UserFirstName { get; set; }

        public string? UserLastName { get; set;}

        public string? UserEmail { get; set; }

        public List<DeleteUserCarListingsViewModel>? CarListings { get; set; } 
            = new List<DeleteUserCarListingsViewModel>();
    }
}
