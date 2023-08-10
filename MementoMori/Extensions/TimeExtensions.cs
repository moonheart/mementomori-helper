namespace MementoMori.Extensions;

public static class TimeExtensions
{
    public static DateTimeOffset ToDateTimeOffset(this long timespan) =>
        DateTimeOffset.FromUnixTimeMilliseconds(timespan).ToLocalTime();

    public static DateTimeOffset ToDateTimeOffset(this long? timespan) =>
        (timespan ?? 0).ToDateTimeOffset();
}