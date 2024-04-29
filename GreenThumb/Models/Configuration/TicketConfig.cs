using GreenThumb.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenThumb.Models.Configuration
{
	public class TicketConfig : IEntityTypeConfiguration<Ticket>
	{
		public void Configure(EntityTypeBuilder<Ticket> entity)
		{
			entity.HasData(
				new Ticket() { TicketId = 1, Title = "Buggy site", Description = "The website is buggy because it is not complete", TechnicianId = 1, TypeName = "Website", DateOpened = new DateOnly(2024, 2, 11) }
				);
		}
	}
}
