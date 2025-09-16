using System.ComponentModel.DataAnnotations;
using Matorikkusu.Toolkit.ValidationAttributes;
using Moq;

namespace Matorikkusu.Toolkit.Tests.NotEmptyAttributes;

public class EnumerableTests
{
    private readonly IServiceProvider _serviceProvider;

    public EnumerableTests()
    {
        _serviceProvider = new Mock<IServiceProvider>().Object;
    }

    [Fact]
    public void IsValid_Expected_False()
    {
        // Arrange
        var valueToValidate = Enumerable.Empty<object>();
        var validationContext = new ValidationContext(valueToValidate, _serviceProvider, null);
        var notEmptyAttribute = new NotEmptyAttribute();

        // Act
        var result = notEmptyAttribute.GetValidationResult(Enumerable.Empty<object>(), validationContext);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void IsValid_Guid_Expected_False()
    {
        // Arrange
        var valueToValidate = Enumerable.Empty<Guid>();
        var validationContext = new ValidationContext(valueToValidate, _serviceProvider, null);
        var notEmptyAttribute = new NotEmptyAttribute();

        // Act
        var result = notEmptyAttribute.GetValidationResult(Enumerable.Empty<Guid>(), validationContext);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void IsValid_Int_Expected_False()
    {
        // Arrange
        var valueToValidate = Enumerable.Empty<int>();
        var validationContext = new ValidationContext(valueToValidate, _serviceProvider, null);
        var notEmptyAttribute = new NotEmptyAttribute();

        // Act
        var result = notEmptyAttribute.GetValidationResult(Enumerable.Empty<int>(), validationContext);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void IsValid_Decimal_Expected_False()
    {
        // Arrange
        var valueToValidate = Enumerable.Empty<decimal>();
        var validationContext = new ValidationContext(valueToValidate, _serviceProvider, null);
        var notEmptyAttribute = new NotEmptyAttribute();

        // Act
        var result = notEmptyAttribute.GetValidationResult(Enumerable.Empty<decimal>(), validationContext);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void IsValid_String_Expected_False()
    {
        // Arrange
        var valueToValidate = Enumerable.Empty<string>();
        var validationContext = new ValidationContext(valueToValidate, _serviceProvider, null);
        var notEmptyAttribute = new NotEmptyAttribute();

        // Act
        var result = notEmptyAttribute.GetValidationResult(Enumerable.Empty<string>(), validationContext);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void IsValid_Float_Expected_False()
    {
        // Arrange
        var valueToValidate = Enumerable.Empty<float>();
        var validationContext = new ValidationContext(valueToValidate, _serviceProvider, null);
        var notEmptyAttribute = new NotEmptyAttribute();

        // Act
        var result = notEmptyAttribute.GetValidationResult(Enumerable.Empty<float>(), validationContext);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void IsValid_Double_Expected_False()
    {
        // Arrange
        var valueToValidate = Enumerable.Empty<double>();
        var validationContext = new ValidationContext(valueToValidate, _serviceProvider, null);
        var notEmptyAttribute = new NotEmptyAttribute();

        // Act
        var result = notEmptyAttribute.GetValidationResult(Enumerable.Empty<double>(), validationContext);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void IsValid_Char_Expected_False()
    {
        // Arrange
        var valueToValidate = Enumerable.Empty<char>();
        var validationContext = new ValidationContext(valueToValidate, _serviceProvider, null);
        var notEmptyAttribute = new NotEmptyAttribute();

        // Act
        var result = notEmptyAttribute.GetValidationResult(Enumerable.Empty<char>(), validationContext);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void IsValid_Expected_True()
    {
        // Arrange
        var list = new List<int> { 1 };
        var array = new[] { 1 };
        var listValidationContext = new ValidationContext(list, _serviceProvider, null);
        var arrayValidationContext = new ValidationContext(array, _serviceProvider, null);
        var notEmptyAttribute = new NotEmptyAttribute();

        // Act
        var resultList = notEmptyAttribute.GetValidationResult(list, listValidationContext);
        var resultArray = notEmptyAttribute.GetValidationResult(array, arrayValidationContext);

        // Assert
        Assert.Null(resultList);
        Assert.Null(resultArray);
    }
}