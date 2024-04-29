using System.ComponentModel.DataAnnotations;
using GreenThumb.ValidationAttributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
namespace GreenThumb.Models.DomainModels
{
    public class DailyLog
    {
        public int DailyLogId { get; set; }

        [Required(ErrorMessage = "Date is required for daily log")]
		[DateRange("01/01/2024", "Now", ErrorMessage = "Invalid date")]
		public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public ICollection<StatusReport> StatusReports { get; set; } = new HashSet<StatusReport>();

		public string GetDateFormatted() => $"{Date.ToString("MMMM")} {Date.Day}, {Date.Year}";
    }
}
