using Matorikkusu.Toolkit.Extensions;

namespace Matorikkusu.Toolkit.Tests.StringHelpers;

public class IncrementAlphaNumericValueTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData("    ", "")]
    [InlineData("@#$%^&*()", "")]
    [InlineData("SAMPLE001", "SAMPLE002")]
    [InlineData("ABC999999999", "ABD000000000")]
    [InlineData("ABC", "ABD")]
    [InlineData("Z999", "")]
    [InlineData("Z", "")]
    [InlineData("z", "")]
    [InlineData("9", "")]
    public void IncrementAlphaNumericValue_ExpectSuccess(string input, string expectedResult)
    {
        // Arrange & Action
        var result = input.IncrementAlphaNumericValue();

        // Result
        Assert.Equal(expectedResult, result);
    }
}