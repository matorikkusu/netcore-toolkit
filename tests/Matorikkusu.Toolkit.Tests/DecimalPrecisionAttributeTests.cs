using System.ComponentModel.DataAnnotations;
using Matorikkusu.Toolkit.ValidationAttributes;
using Moq;

namespace Matorikkusu.Toolkit.Tests;

public class DecimalPrecisionAttributeTests
{
    private readonly IServiceProvider _serviceProvider;

    public DecimalPrecisionAttributeTests()
    {
        _serviceProvider = new Mock<IServiceProvider>().Object;
    }

    [Theory]
    [InlineData("123456.123456", 12, 6)]
    [InlineData("123456.123456m", 12, 3)]
    public void IsValid_Expected_False_With_Invalid_Type(object valueToValidate, int precision, int scale)
    {
        // Arrange
        var validationContext = new ValidationContext(valueToValidate, _serviceProvider, null);
        var decimalPrecisionAttribute = new DecimalPrecisionAttribute(precision, scale);

        // Act
        var result = decimalPrecisionAttribute.GetValidationResult(valueToValidate, validationContext);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void IsValid_Expected_False()
    {
        // Arrange
        var valuesToValidate = new[]
        {
            new ValidateDecimal(1234567.123456m, 12, 6),
            new ValidateDecimal(123456.1234567m, 12, 6)
        };

        // Act
        var expectedResults = new Dictionary<ValidateDecimal, ValidationResult>();
        foreach (var valueToValidate in valuesToValidate)
        {
            var validationContext = new ValidationContext(valueToValidate.Value, _serviceProvider, null);
            var decimalPrecisionAttribute =
                new DecimalPrecisionAttribute(valueToValidate.Precision, valueToValidate.Scale);

            var result = decimalPrecisionAttribute.GetValidationResult(valueToValidate.Value, validationContext);

            expectedResults.Add(valueToValidate, result);
        }

        Assert.All(expectedResults.Values, Assert.NotNull);
    }

    [Fact]
    public void IsValid_Expected_True()
    {
        // Arrange
        var valuesToValidate = new[]
        {
            new ValidateDecimal(123456789987.123456m, 18, 6),
            new ValidateDecimal(123456789987.123456m, 18, 6),
            new ValidateDecimal(123456.123456m, 12, 6),
            new ValidateDecimal(123456.123m, 12, 3),
        };

        // Act
        var expectedResults = new Dictionary<ValidateDecimal, ValidationResult>();
        foreach (var valueToValidate in valuesToValidate)
        {
            var validationContext = new ValidationContext(valueToValidate.Value, _serviceProvider, null);
            var decimalPrecisionAttribute =
                new DecimalPrecisionAttribute(valueToValidate.Precision, valueToValidate.Scale);

            var result = decimalPrecisionAttribute.GetValidationResult(valueToValidate.Value, validationContext);

            expectedResults.Add(valueToValidate, result);
        }

        // Assert
        Assert.All(expectedResults.Values, Assert.Null);
    }

    private class ValidateDecimal
    {
        public decimal Value { get; }
        public int Precision { get; }
        public int Scale { get; }

        public ValidateDecimal(decimal value, int precision, int scale)
        {
            Value = value;
            Precision = precision;
            Scale = scale;
        }
    }
}