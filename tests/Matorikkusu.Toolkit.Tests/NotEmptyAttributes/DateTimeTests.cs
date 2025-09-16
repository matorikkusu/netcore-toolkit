using System.ComponentModel.DataAnnotations;
using Matorikkusu.Toolkit.ValidationAttributes;
using Moq;

namespace Matorikkusu.Toolkit.Tests.NotEmptyAttributes;

public class DateTimeTests
{
    private readonly IServiceProvider _serviceProvider;

    public DateTimeTests()
    {
        _serviceProvider = new Mock<IServiceProvider>().Object;
    }

    [Fact]
    public void IsValid_Expected_False()
    {
        // Arrange
        var valueToValidate = new DateTime();
        var validationContext = new ValidationContext(valueToValidate, _serviceProvider, null);
        var notEmptyAttribute = new NotEmptyAttribute();

        // Act
        var result = notEmptyAttribute.GetValidationResult(valueToValidate, validationContext);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void IsValid_Expected_True()
    {
        // Arrange
        var valueToValidate = DateTime.UtcNow;
        var validationContext = new ValidationContext(valueToValidate, _serviceProvider, null);
        var notEmptyAttribute = new NotEmptyAttribute();

        // Act
        var result = notEmptyAttribute.GetValidationResult(valueToValidate, validationContext);

        // Assert
        Assert.Null(result);
    }
}