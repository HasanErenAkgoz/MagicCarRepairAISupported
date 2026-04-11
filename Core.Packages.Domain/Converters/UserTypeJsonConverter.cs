using MagicCarRepairAISupported.Domain.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MagicCarRepairAISupported.Domain.Converters
{
    /// <summary>
    /// UserType enum'ını number olarak serialize/deserialize eder
    /// </summary>
    public class UserTypeJsonConverter : JsonConverter<UserType>
    {
        public override UserType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                var intValue = reader.GetInt32();
                if (Enum.IsDefined(typeof(UserType), intValue))
                {
                    return (UserType)intValue;
                }
                throw new JsonException($"Invalid UserType value: {intValue}");
            }
            else if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                if (Enum.TryParse<UserType>(stringValue, ignoreCase: true, out var result))
                {
                    return result;
                }
                throw new JsonException($"Invalid UserType value: {stringValue}");
            }
            
            throw new JsonException($"Unexpected token type: {reader.TokenType}");
        }

        public override void Write(Utf8JsonWriter writer, UserType value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue((int)value);
        }
    }
}
