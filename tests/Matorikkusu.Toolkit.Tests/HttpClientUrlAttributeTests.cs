using System.ComponentModel.DataAnnotations;
using Matorikkusu.Toolkit.ValidationAttributes;
using Moq;

namespace Matorikkusu.Toolkit.Tests;

public class HttpClientUrlAttributeTests
{
    private readonly IServiceProvider _serviceProvider;

    public HttpClientUrlAttributeTests()
    {
        _serviceProvider = new Mock<IServiceProvider>().Object;
    }
    
    [Theory]
    [InlineData("http{{domain}}")]
    [InlineData("sub.domain.com")]
    [InlineData("domain.com")]
    [InlineData("sub.domain.com:3000")]
    [InlineData("domain.com:3000")]
    [InlineData("sub.domain.com/path/to")]
    [InlineData("domain.com/path/to")]
    [InlineData("sub.domain.com:3000/path/to")]
    [InlineData("domain.com:3000/path/to")]
    [InlineData("#@!#$#%$^%&^%&^%*&^%")]
    [InlineData("http://{{domain}}")]
    [InlineData("https://{{domain}}")]
    [InlineData("ftp://{{domain}}")]
    public void IsValid_Expected_False(object valueToValidate)
    {
        // Arrange
        var validationContext = new ValidationContext(valueToValidate, _serviceProvider, null);
        var decimalPrecisionAttribute = new HttpClientUrlAttribute();

        // Act
        var result = decimalPrecisionAttribute.GetValidationResult(valueToValidate, validationContext);

        // Assert
        Assert.NotNull(result);
    }
    
    [Theory]
    [InlineData("http://domain.com")]
    [InlineData("https://domain.com")]
    [InlineData("ftp://domain.com")]
    [InlineData("http://sub.domain.com")]
    [InlineData("https://sub.domain.com")]
    [InlineData("ftp://sub.domain.com")]
    [InlineData("http://domain.com:3000")]
    [InlineData("https://domain.com:3000")]
    [InlineData("ftp://domain.com:3000")]
    [InlineData("http://sub.domain.com/path/to")]
    [InlineData("https://sub.domain.com/path/to")]
    [InlineData("ftp://sub.domain.com/path/to")]
    [InlineData("http://domain.com:3000/path/to")]
    [InlineData("https://domain.com:3000/path/to")]
    [InlineData("ftp://domain.com:3000/path/to")]
    public void IsValid_Expected_True(object valueToValidate)
    {
        // Arrange
        var validationContext = new ValidationContext(valueToValidate, _serviceProvider, null);
        var decimalPrecisionAttribute = new HttpClientUrlAttribute();

        // Act
        var result = decimalPrecisionAttribute.GetValidationResult(valueToValidate, validationContext);

        // Assert
        Assert.Null(result);
    }
}