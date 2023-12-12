using Newtonsoft.Json;

namespace MementoMori.WebUI.Extensions;

public static class JsonExtensions
{
    public static string ToJson<T>(this T obj, bool pretty = false) => JsonConvert.SerializeObject(obj, pretty ? Formatting.Indented : Formatting.None);
}