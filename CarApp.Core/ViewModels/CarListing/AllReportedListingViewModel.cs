using CarApp.Infrastructure.Constants.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Core.ViewModels.CarListing
{
    public class AllReportedListingViewModel
    {
        public int CarListingId { get; set; }
        public string CarImage { get; set; } = null!;
        public List<ReportReason> ReportReason { get; set; } = new List<ReportReason>();
        public List<string> Comment { get; set; } = new List<string>();
        public List<string> CommentAuthors { get; set; } = new List<string>();
        public string ReportedAt { get; set; } = null!;
    }
}
