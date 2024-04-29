using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace GreenThumb.Models.DomainModels
{
	public class User : IdentityUser
	{
		public int? TechnicianId { get; set; } = null!;

		[NotMapped]
		public IList<string> RoleNames { get; set; } = null!;
	}
}
