using System.ComponentModel.DataAnnotations;

namespace GreenThumb.Models.DomainModels
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Please enter a first name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a last name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a phone number")]
        [RegularExpression(@"^(\d{10}|\d{3}-\d{3}-\d{4})$", ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter their relation to the project")]
        public string Relation { get; set; } = string.Empty;
    }
}
