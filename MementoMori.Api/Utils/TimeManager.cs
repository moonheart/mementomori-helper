using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Utils;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Utils;

/// <summary>
/// 时间管理器 - 每个账户独立实例，处理时区差异。
///
/// 对外保留旧版 API（DiffFromUtc / ServerNow / IsInTime / SetTimeServerMb），
/// 内部实现委托给 OrtegaTimeManager，便于平滑替换。
/// </summary>
public class TimeManager
{
    private OrtegaTimeManager _inner = new();

    public TimeSpan DiffFromUtc => TimeSpan.FromMilliseconds(_inner.DifferenceFromUtc);

    public DateTime ServerNow => _inner.GetLocalDateTime();

    public void SetTimeServerMb(TimeServerMB timeServerMb)
    {
        if (timeServerMb == null)
        {
            throw new ArgumentNullException(nameof(timeServerMb));
        }

        _inner = new OrtegaTimeManager(timeServerMb);
    }

    public bool IsInTime(IHasStartEndTime hasStartEndTime)
    {
        if (hasStartEndTime == null)
        {
            throw new ArgumentNullException(nameof(hasStartEndTime));
        }

        return _inner.IsInTime(hasStartEndTime);
    }

    public bool IsInTime(IHasStartEndTimeZone hasStartEndTimeZone)
    {
        if (hasStartEndTimeZone == null)
        {
            throw new ArgumentNullException(nameof(hasStartEndTimeZone));
        }

        return _inner.IsInTime(hasStartEndTimeZone);
    }
}
