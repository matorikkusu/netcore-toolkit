using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Matorikkusu.Toolkit.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class ValidNumberStringAttribute : ValidationAttribute
{
    private readonly Type _type;
    private readonly bool _isPositiveNumber;

    public ValidNumberStringAttribute(Type type, bool isPositiveNumber, string errorMessage = "")
        : base(errorMessage)
    {
        _type = type;
        _isPositiveNumber = isPositiveNumber;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        if (TryParseValueByType(value))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName, value));
    }

    private bool TryParseValueByType(object value)
    {
        //ToDo: need to validate more number type here
        return Type.GetTypeCode(_type) switch
        {
            TypeCode.Int32 => ValidationNumber(value),
            _ => true
        };
    }

    private bool ValidationNumber(object value)
    {
        var parseResult = int.TryParse(value.ToString(), out var numberValue);
        if (!parseResult)
        {
            return parseResult;
        }

        if (_isPositiveNumber) return numberValue >= 0;
        return parseResult;
    }

    private string FormatErrorMessage(string name, object value)
    {
        return string.IsNullOrEmpty(ErrorMessage)
            ? $"The value of field {name}: {value} is not valid."
            : string.Format(CultureInfo.CurrentCulture, ErrorMessage, name, value);
    }
}