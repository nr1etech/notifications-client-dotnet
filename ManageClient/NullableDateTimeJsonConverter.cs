using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ManageClient;

/// <summary>
/// Handles nullable DateTime conversion, treating an empty string as null
/// </summary>
public class NullableDateTimeJsonConverter : JsonConverter<DateTime?>
{
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Debug.Assert(typeToConvert == typeof(DateTime?));

        var value = reader.GetString() ?? string.Empty;
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        return DateTime.Parse(value);
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (!value.HasValue)
        {
            writer.WriteStringValue("");
        }

        writer.WriteStringValue(value.Value.ToString("s"));
    }}
