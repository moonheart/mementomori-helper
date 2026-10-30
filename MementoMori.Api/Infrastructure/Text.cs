namespace MementoMori.Api;

/// <summary>User-facing strings from ResourceStrings by key (culture: Localization:Culture).</summary>
public static class Text
{
    public static string T(string key, params object?[] args)
    {
        var s = ResourceStrings.ResourceManager.GetString(key, ResourceStrings.Culture) ?? key;
        return args.Length == 0 ? s : string.Format(s, args);
    }
}
