using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GreenThumb.ValidationAttributes
{
	public class DateRangeAttribute : ValidationAttribute
	{
		private DateOnly _Min {  get; set; } = DateOnly.FromDateTime(DateTime.MinValue);
		private DateOnly _Max { get; set; } = DateOnly.FromDateTime(DateTime.MaxValue);

		public DateRangeAttribute(string option)
		{
			if (option == "Past")
				_Max = DateOnly.FromDateTime(DateTime.Now);
			else if (option == "Future")
				_Min = DateOnly.FromDateTime(DateTime.Now);
		}

		public DateRangeAttribute(string Min, string Max)
		{
			_Min = Min == "Now" ? DateOnly.FromDateTime(DateTime.Now) : DateOnly.FromDateTime(DateTime.Parse(Min, new CultureInfo("en-US")));
			_Max = Max == "Now" ? DateOnly.FromDateTime(DateTime.Now) : DateOnly.FromDateTime(DateTime.Parse(Max, new CultureInfo("en-US")));
		}

		protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
		{
			if (value is DateOnly)
			{
				DateOnly dateValue = (DateOnly)value;

				if (dateValue < _Min) 
					return new ValidationResult($"Date must be on or after {_Min}");
				else if (dateValue > _Max)
					return new ValidationResult($"Date must be on or before {_Max}");

				return ValidationResult.Success;
			}
			return new ValidationResult("Invalid date format");
		}
	}
}
