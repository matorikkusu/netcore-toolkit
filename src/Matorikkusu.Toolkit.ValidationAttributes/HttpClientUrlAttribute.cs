using System.ComponentModel.DataAnnotations;

namespace Matorikkusu.Toolkit.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
    AllowMultiple = false)]
public class HttpClientUrlAttribute : DataTypeAttribute
{
    public HttpClientUrlAttribute() : base(DataType.Url)
    {
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult(ErrorMessage);
        }

        if (value is not string valueAsString) return new ValidationResult(ErrorMessage);

        var result = (valueAsString.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                      || valueAsString.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
                      || valueAsString.StartsWith("ftp://", StringComparison.OrdinalIgnoreCase))
                     && Uri.TryCreate(valueAsString, UriKind.Absolute, out _);

        return result ? ValidationResult.Success : new ValidationResult(ErrorMessage);
    }
}