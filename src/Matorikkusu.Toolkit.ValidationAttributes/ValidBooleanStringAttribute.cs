using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Matorikkusu.Toolkit.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class ValidBooleanStringAttribute : ValidationAttribute
{
    public ValidBooleanStringAttribute(string errorMessage = "")
        : base(errorMessage)
    {
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        if (bool.TryParse(value.ToString(), out _))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName, value));
    }

    private string FormatErrorMessage(string name, object value)
    {
        return string.IsNullOrEmpty(ErrorMessage)
            ? $"The value of field {name}: {value} is not valid."
            : string.Format(CultureInfo.CurrentCulture, ErrorMessage, name, value);
    }
}