using System.ComponentModel.DataAnnotations;
using Matorikkusu.Toolkit.ValidationAttributes;
using Moq;

namespace Matorikkusu.Toolkit.Tests.NotEmptyAttributes;

public class GuidTests
{
    private readonly IServiceProvider _serviceProvider;

    public GuidTests()
    {
        _serviceProvider = new Mock<IServiceProvider>().Object;
    }

    [Theory]
    [InlineData("")]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public void IsValid_Expected_False(string testCase)
    {
        // Arrange
        Guid.TryParse(testCase, out var valueToValidate);
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
        var valueToValidate = Guid.NewGuid();
        var validationContext = new ValidationContext(valueToValidate, _serviceProvider, null);
        var notEmptyAttribute = new NotEmptyAttribute();

        // Act
        var result = notEmptyAttribute.GetValidationResult(valueToValidate, validationContext);

        // Assert
        Assert.Null(result);
    }
}