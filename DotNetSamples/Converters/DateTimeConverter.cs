using System.Text.Json;

namespace DotNetSamples.Converters
{
    public class DateTimeStringConverter : System.Text.Json.Serialization.JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // If there is no time value(default) then it is a normal date. We need to strip the timestamp from the DateTime object on JSON convert so that the endpoints can parse this properly when received.
            if (value.TimeOfDay.TotalSeconds == 0)
            {
                writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd"));
            }
            else
            {
                writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));
            }
        }
    }
}
