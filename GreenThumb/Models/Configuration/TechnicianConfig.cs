using GreenThumb.Models.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GreenThumb.Models.Configuration
{
	public class TechnicianConfig : IEntityTypeConfiguration<Technician>
	{
		public void Configure(EntityTypeBuilder<Technician> entity)
		{
			entity.HasData(
				new Technician() { TechnicianId = 1, Name = "Steve French", Email = "SteveFrench@outlook.com", PhoneNumber = "111-111-1111" }
				);
		}
	}
}
