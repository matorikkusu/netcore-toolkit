using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Matorikkusu.Toolkit.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class NotEmptyAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null) return new ValidationResult(ErrorMessage);
        return value switch
        {
            Guid guid => guid != Guid.Empty ? ValidationResult.Success : new ValidationResult(ErrorMessage),
            DateTime dateTime => dateTime != new DateTime()
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage),
            ICollection collection => collection.Count > 0
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage),
            IEnumerable enumerable => enumerable.GetEnumerator().MoveNext()
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage),
            _ => ValidationResult.Success
        };
    }
}