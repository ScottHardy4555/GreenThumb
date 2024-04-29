using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GreenThumb.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace GreenThumb.Models.Configuration
{
	public class StatusReportConfig : IEntityTypeConfiguration<StatusReport>
	{
		public void Configure(EntityTypeBuilder<StatusReport> entity)
		{
			entity.HasOne(s => s.DailyLog).WithMany(d => d.StatusReports).OnDelete(DeleteBehavior.Restrict);
			entity.HasData(
				new StatusReport() { StatusReportId = 1, DailyLogId = 1, Time = new TimeOnly(0, 0), Temperature = 29f, Humidity = 60f, PartsPerMillion = 10f, ElectricalConductivity = 128f, PH = 7.2f },
				new StatusReport() { StatusReportId = 2, DailyLogId = 1, Time = new TimeOnly(1, 0), Temperature = 22.5f, Humidity = 62f, PartsPerMillion = 10f, ElectricalConductivity = 128f, PH = 7.1f },
				new StatusReport() { StatusReportId = 3, DailyLogId = 1, Time = new TimeOnly(2, 0), Temperature = 20.1f, Humidity = 64.5f, PartsPerMillion = 10f, ElectricalConductivity = 128f, PH = 7.2f }
				);
		}
	}
}
