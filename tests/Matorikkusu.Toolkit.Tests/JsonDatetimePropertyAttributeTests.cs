using System.Text.Json;
using System.Text.Json.Serialization;
using Matorikkusu.Toolkit.ValidationAttributes;

namespace Matorikkusu.Toolkit.Tests;

public class JsonDatetimePropertyAttributeTests
{
    [Fact]
    public void SerializeTest()
    {
        // Arrange
        var datetimeClasses = new List<DateTimeClass>
        {
            new()
            {
                DateTime = new DateTime(2023, 02, 01, 11, 59, 59, 0000000, DateTimeKind.Local),
            },
            new()
            {
                DateTime = new DateTime(2023, 02, 01, 11, 59, 59, 0000000, DateTimeKind.Unspecified)
            },
            new()
            {
                DateTime = new DateTime(2023, 02, 01, 11, 59, 59, 0000000, DateTimeKind.Utc)
            }
        };
        const string expectedResult = "2023-02-01T11:59:59.0000000Z";

        // Act & Assert
        var serializedDatetimeClasses = JsonSerializer.Serialize(datetimeClasses);
        var newDatetimeClasses = JsonSerializer.Deserialize<IList<DateTimeClass>>(serializedDatetimeClasses);

        // Assert
        Assert.All(newDatetimeClasses,
            item => Assert.Equal(expectedResult, item.DateTime.ToString("O")));
        Assert.All(newDatetimeClasses,
            item => Assert.Null(item.DateTimeNull));
    }

    private class DateTimeClass
    {
        [JsonPropertyName("DateTime")]
        [JsonConverter(typeof(JsonDatetimeUtcConverter))]
        public DateTime DateTime { get; set; }
        
        [JsonPropertyName("DateTimeNull")]
        [JsonConverter(typeof(JsonDatetimeNullUtcConverter))]
        public DateTime? DateTimeNull { get; set; }
    }
}