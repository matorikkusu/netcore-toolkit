using System.ComponentModel.DataAnnotations;
using Matorikkusu.Toolkit.ValidationAttributes;

namespace Matorikkusu.Toolkit.Tests;

public class ValidateDateTimeString
{
    [Theory]
    [InlineData("2024-05-10", "", true)]
    [InlineData("2024/05/10", "", false)]
    [InlineData("2024/05/10", "yyyy/MM/dd", true)]
    public void Validate_Invalid(object inputValue, string format, bool expectedResult)
    {
        // Arrange
        var validationContext = new ValidationContext(inputValue, null, null);

        var greaterThanDecimalAttribute = new ValidDateTimeStringAttribute(format);

        // Act
        var result = greaterThanDecimalAttribute.GetValidationResult(inputValue, validationContext);

        // Assert
        if (expectedResult)
        {
            Assert.Null(result);
        }
        else
        {
            Assert.NotNull(result);
        }
    }
}