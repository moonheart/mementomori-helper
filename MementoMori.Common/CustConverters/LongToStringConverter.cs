using System.Text.Json;
using System.Text.Json.Serialization;

namespace MementoMori.Common.CustConverters;

/// <summary>
/// 将 long/ulong 序列化为字符串的自定义转换器
/// </summary>
public class LongToStringConverter : JsonConverter<long>
{
    public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            return long.Parse(reader.GetString()!);
        }
        return reader.GetInt64();
    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}