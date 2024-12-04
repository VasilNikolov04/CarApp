using System.ComponentModel.DataAnnotations;
using CarApp.Core.Enumerations;
using CarApp.Infrastructure.Constants.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace CarApp.Core.ViewModels.CarListing
{
    public class ReportListingViewModel
    {
        [Required]
        public int ListingId { get; set; }

        [Required]
        public string SellerId { get; set; } = null!;
        [Required]
        public string SellerFullName { get; set; } = null!;
        [Required]
        public string SellerEmail { get; set; } = null!;

        [Required]
        public string CarListingTitle { get; set; } = null!;

        public string? Comment { get; set; }

        [Required]
        public string SelectedReason { get; set; } = null!;

        public List<ReportReason> Reasons { get; set; } = new List<ReportReason>();
    }
}
