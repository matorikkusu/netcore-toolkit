using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Matorikkusu.Toolkit.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DecimalPrecisionAttribute : ValidationAttribute
{
    private readonly int _precision;
    private readonly int _scale;

    public DecimalPrecisionAttribute(int precision, int scale)
    {
        _precision = precision;
        _scale = scale;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        if (value is decimal decimalValue)
        {
            try
            {
                // Check if the decimal value is too large to fit into a long
                if (decimalValue > long.MaxValue)
                {
                    return new ValidationResult(
                        $"The value of {validationContext.DisplayName} is too large. The maximum allowed value is {long.MaxValue}.");
                }

                var integerPart = (long)Math.Truncate(decimalValue);
                var decimalPart = Math.Abs(decimalValue - integerPart);
                var decimalPlaces = BitConverter.GetBytes(decimal.GetBits(decimalPart)[3])[2];

                if (decimalPlaces > _scale || integerPart.ToString().Length > _precision - _scale)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }

                return ValidationResult.Success;
            }
            catch (OverflowException)
            {
                return new ValidationResult($"The value of {validationContext.DisplayName} is too large to be parsed.");
            }
        }

        return new ValidationResult(
            $"Invalid type. The field {validationContext.DisplayName}'s expected type is Decimal.");
    }

    public override string FormatErrorMessage(string name)
    {
        return string.IsNullOrEmpty(ErrorMessageString)
            ? $"The field {name} must have precision {_precision} and scale {_scale}."
            : string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, _precision, _scale);
    }
}