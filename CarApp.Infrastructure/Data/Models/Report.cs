using CarApp.Infrastructure.Constants.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CarApp.Infrastructure.Constants.DataConstants.Reports;


namespace CarApp.Infrastructure.Data.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ListingId { get; set; }
        [ForeignKey(nameof(ListingId))]
        public virtual CarListing CarListing { get; set; } = null!;

        [Required]
        public string ReporterId { get; set; } = null!;
        [ForeignKey(nameof(ReporterId))]
        public virtual ApplicationUser Reporter { get; set; } = null!;

        [Required]
        public string SellerId { get; set; } = null!;
        [ForeignKey(nameof(SellerId))]
        public virtual ApplicationUser Seller { get; set; } = null!;

        [MaxLength(ReportCommentMaxLength)]
        public string? Comment { get; set; }

        [Required]
        public ReportReason ReportReason { get; set; }

        [Required]
        public DateTime ReportedAt { get; set; }
    }

}
