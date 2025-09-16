namespace Matorikkusu.Toolkit.Tests;

public class DateTimeWithTimeZoneTests
{
    // [Theory]
    // [InlineData("2020-01-01T00:00:00", "Asia/Bangkok", "2019-12-31T17:00:00")]
    // [InlineData("2020-01-01T00:00:00", "", "2020-01-01T00:00:00")]
    // public void CreateNewDateTimeWithZone(string dateTimeString, string timeZoneString, string expected)
    // {
    //     // Arrange
    //     var dateTimeInput = DateTime.Parse(dateTimeString);
    //
    //     // Act
    //     var result = new DateTimeWithTimeZone(dateTimeInput, timeZoneString);
    //     var expectedResult = DateTime.SpecifyKind(DateTime.Parse(expected), DateTimeKind.Utc);
    //
    //     // Assert
    //     Assert.Equal(expectedResult, result.UniversalTime);
    // }
}