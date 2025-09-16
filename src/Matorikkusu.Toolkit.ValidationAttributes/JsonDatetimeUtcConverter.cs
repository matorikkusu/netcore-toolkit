using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Matorikkusu.Toolkit.ValidationAttributes;

public class JsonDatetimeUtcConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.ParseExact(reader.GetString()!,
            "O", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        var utcValue = new DateTime(value.Ticks, DateTimeKind.Utc);
        writer.WriteStringValue(utcValue.ToString(
            "O", CultureInfo.InvariantCulture));
    }
}