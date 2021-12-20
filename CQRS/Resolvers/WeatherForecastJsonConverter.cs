namespace CQRS.Resolvers
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using CQRS.Model;

    public class WeatherForecastJsonConverter : JsonConverter<WeatherForecast>
    {
        public override WeatherForecast Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            WeatherForecast result = new WeatherForecast();

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return result;
                }

                // Get the key.
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                string propertyName = reader.GetString();

                switch (propertyName.ToLower())
                {
                    case "weatherforecastid":
                        result.WeatherForecastId = reader.GetGuid();
                        break;
                    case "date":
                        result.Date = reader.GetDateTime();
                        break;
                    case "temperaturec":
                        result.TemperatureC = reader.GetInt32();
                        break;
                    case "summary":
                        result.Summary = reader.GetString();
                        break;
                    case "operations":
                        reader.Skip();
                        break;
                }
            }
            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, WeatherForecast value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("weatherForecastId", value.WeatherForecastId);
            writer.WriteString("date", value.Date);
            writer.WriteNumber("temperatureC", value.TemperatureC);
            writer.WriteNumber("temperatureF", value.TemperatureF);
            writer.WriteString("summary", value.Summary);
            writer.WriteEndObject();
        }
    }
}
