using System.ComponentModel.DataAnnotations;
using Matorikkusu.Toolkit.ValidationAttributes;

namespace Matorikkusu.Toolkit.Tests;

public class ValidateBooleanString
{
    [Theory]
    [InlineData("false", true)]
    [InlineData("", false)]
    [InlineData("1", false)]
    [InlineData("sample", false)]
    public void Validate_Invalid(object inputValue, bool expectedResult)
    {
        // Arrange
        var validationContext = new ValidationContext(inputValue, null, null);

        var greaterThanDecimalAttribute = new ValidBooleanStringAttribute();

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