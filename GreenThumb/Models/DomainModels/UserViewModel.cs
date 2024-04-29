﻿using Microsoft.AspNetCore.Identity;

namespace GreenThumb.Models.DomainModels
{
	public class UserViewModel
	{
		public IEnumerable<User> Users { get; set; } = null!;
		public IEnumerable<IdentityRole> Roles { get; set; } = null!;
	}
}
