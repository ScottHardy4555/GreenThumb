using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace GreenThumb.Models
{
    public class DailyLog
    {
        public int DailyLogId { get; set; }

        [Required(ErrorMessage = "Date is required for daily log")]
        public DateOnly Date { get; set; }

        
    }
}
