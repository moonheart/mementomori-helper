using System.Resources;

namespace MementoMori.Common;

public static class ResourceManagerExtensions
{
    public static string? GetEnumString<T>(this ResourceManager resourceManager, T value)
    {
        return resourceManager.GetString($"{typeof(T).Name}{value}");
    }
}