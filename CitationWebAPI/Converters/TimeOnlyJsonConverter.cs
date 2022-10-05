using System.Text.Json;
using System.Text.Json.Serialization;


namespace CitationWebAPI.Converters
{
    public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? readerValue = reader.GetString();
            return TimeOnly.Parse(readerValue!);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("HH:mm:ss.fff"));
        }
    }
}
