using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace GreenThumb.Models.DomainModels
{
    public class StatusReport
    {
        public int StatusReportId { get; set; }

        [Required (ErrorMessage = "")]
        public int DailyLogId { get; set; }
        public DailyLog DailyLog { get; set; } = null!;

        [Required(ErrorMessage = "Time is required for hourly log")]
        public TimeOnly Time { get; set; }

        

        [Required(ErrorMessage = "Temperature is required")]
        public float? Temperature { get; set; } = null!;

        [Required(ErrorMessage = "Humidity is required")]
        [Range(0, 100, ErrorMessage = "Humidity must be between 0-100%")]
        public float? Humidity { get; set; } = null!;

        [Required(ErrorMessage = "PPM is required")]
        public float? PartsPerMillion { get; set; } = null!;

        [Required(ErrorMessage = "EC is required")]
        public float? ElectricalConductivity { get; set; } = null!;

        [Required(ErrorMessage = "PH is required")]
        public float? PH { get; set; } = null!;
    }
}
