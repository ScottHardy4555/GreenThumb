﻿using System.ComponentModel.DataAnnotations;

namespace GreenThumb.Models.DomainModels
{
    public class Technician
    {
        public int TechnicianId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an email")]
		[DataType(DataType.EmailAddress, ErrorMessage = "Please provide a valid email address")]
		public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a phone number")]
        [RegularExpression(@"^(\d{10}|\d{3}-\d{3}-\d{4})$", ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
