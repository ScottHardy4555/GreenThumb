using GreenThumb.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenThumb.Models.Configuration
{
	public class DailyLogConfig : IEntityTypeConfiguration<DailyLog>
	{
		public void Configure(EntityTypeBuilder<DailyLog> entity)
		{
			entity.HasMany(d => d.StatusReports).WithOne(s => s.DailyLog).OnDelete(DeleteBehavior.Cascade);

			entity.HasData(
				new DailyLog() { DailyLogId = 1, Date = new DateOnly(2024, 1, 31) }
				);
		}
	}
}
