using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace GreenThumb.Models
{
    public class StatusReport
    {
        public int StatusReportId { get; set; }
        public int DailyLogId { get; set; }

        [Required(ErrorMessage = "Time is required for hourly log")]
        public TimeOnly Time { get; set; }

        public float? Temperature { get; set; } = null!;

        [Range(0, 100)]
        public float? Humidity { get; set; } = null!;

        public float? PartsPerMillion { get; set; } = null!;

        public float? ElectricalConductivity { get; set; } = null!;

        public float? PH { get; set; } = null!;
    }
}
