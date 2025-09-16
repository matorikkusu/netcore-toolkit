using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Matorikkusu.Toolkit.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class CoordinatesAttribute : ValidationAttribute
{
    public CoordinatesAttribute()
    {
    }

    public CoordinatesAttribute(string errorMessage) : base(errorMessage)
    {
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null) return new ValidationResult(ErrorMessage);
        if (value is decimal)
        {
            return ValidationResult.Success;
        }

        if (value is int)
        {
            return ValidationResult.Success;
        }

        if (value is float)
        {
            return ValidationResult.Success;
        }

        if (value is double)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(
            $"Unsupported type. The field {validationContext.DisplayName}'s expected type is Decimal/Int/Float/Double.");
    }

    public override string FormatErrorMessage(string name)
    {
        return string.IsNullOrEmpty(ErrorMessageString)
            ? $"The coordinate's {name} must have value"
            : string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
    }
}