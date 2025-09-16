using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Matorikkusu.Toolkit.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class ValidDateTimeStringAttribute : ValidationAttribute
{
    private const string DateFormat = "yyyy-MM-dd";
    private readonly string _format;

    public ValidDateTimeStringAttribute(string format = "", string errorMessage = "")
        : base(errorMessage)
    {
        _format = string.IsNullOrEmpty(format) ? DateFormat : format;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        if (DateTime.TryParseExact(value.ToString(), _format,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
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