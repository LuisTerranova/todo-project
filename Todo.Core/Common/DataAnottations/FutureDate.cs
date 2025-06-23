using System.ComponentModel.DataAnnotations;

namespace Todo.Core.Common;

public class FutureDate : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        if (value is DateTime dateTime)
        {
            if (dateTime.Date < DateTime.Now.Date)
                return new ValidationResult(ErrorMessage ?? "The date must be after the current date");
        }

        return ValidationResult.Success;
    }
}