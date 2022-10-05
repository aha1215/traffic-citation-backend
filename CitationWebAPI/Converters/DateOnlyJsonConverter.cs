using System.Text.Json;
using System.Text.Json.Serialization;

namespace CitationWebAPI.Converters
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        // Deserialize
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? readerValue = reader.GetString();
            return DateOnly.Parse(readerValue!);
        }

        // Serialize
        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
        }
    }
}
