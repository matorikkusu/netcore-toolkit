using System.ComponentModel.DataAnnotations;
using Matorikkusu.Toolkit.ValidationAttributes;

namespace Matorikkusu.Toolkit.Tests;

public class ValidateNumberString
{
    [Theory]
    [InlineData("sample", typeof(int), true, false)]
    [InlineData("1", typeof(int), true, true)]
    [InlineData("-1", typeof(int), true, false)]
    [InlineData("-1", typeof(int), false, true)]
    public void Validate_Invalid(object inputValue, Type type, bool isPositiveNumber, bool expectedResult)
    {
        // Arrange
        var validationContext = new ValidationContext(inputValue, null, null);

        var greaterThanDecimalAttribute = new ValidNumberStringAttribute(type, isPositiveNumber);

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