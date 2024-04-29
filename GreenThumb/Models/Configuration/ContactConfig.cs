using GreenThumb.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenThumb.Models.Configuration
{
	public class ContactConfig : IEntityTypeConfiguration<Contact>
	{
		public void Configure(EntityTypeBuilder<Contact> entity)
		{
			entity.HasData(
				new Contact() { ContactId = 1, FirstName = "Steve", LastName = "French", Email = "SteveFrench@outlook.com", PhoneNumber = "111-111-1111", Relation = "Seed distributer" }
				);
		}
	}
}
