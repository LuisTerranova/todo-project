using System.ComponentModel.DataAnnotations;

namespace Todo.Core.Common;

public class FutureDate : ValidationAttribute
{
    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        switch (value)
        {
            case null:
                return ValidationResult.Success;
            case DateTime dateTime when dateTime.Date < DateTime.Now.Date:
                return new ValidationResult(ErrorMessage ?? "The date must be after the current date");
            default:
                return ValidationResult.Success;
        }
    }
}