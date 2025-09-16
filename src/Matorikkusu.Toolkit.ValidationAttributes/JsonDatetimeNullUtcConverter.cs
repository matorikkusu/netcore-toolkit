using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Matorikkusu.Toolkit.ValidationAttributes;

public class JsonDatetimeNullUtcConverter : JsonConverter<DateTime?>
{
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (string.IsNullOrEmpty(reader.GetString())) return null;
        return DateTime.ParseExact(reader.GetString()!,
            "O", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (!value.HasValue) return;
        var utcValue = new DateTime(value.Value.Ticks, DateTimeKind.Utc);
        writer.WriteStringValue(utcValue.ToString(
            "O", CultureInfo.InvariantCulture));
    }
}