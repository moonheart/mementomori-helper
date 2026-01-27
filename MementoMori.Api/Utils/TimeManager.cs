using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Utils;

namespace MementoMori.Api.Utils;

/// <summary>
/// 时间管理器 - 每个账户独立实例，处理时区差异
/// </summary>
public class TimeManager
{
    private TimeServerMB? _timeServerMb;

    public TimeSpan DiffFromUtc => _timeServerMb != null
        ? TimeSpan.Parse(_timeServerMb.DifferenceDateTimeFromUtc)
        : TimeSpan.Zero;

    public void SetTimeServerMb(TimeServerMB timeServerMb)
    {
        _timeServerMb = timeServerMb;
    }

    public DateTime ServerNow => DateTime.UtcNow + DiffFromUtc;

    public bool IsInTime(IHasStartEndTime hasStartEndTime)
    {
        var now = DateTime.UtcNow;
        var start = DateTime.Parse(hasStartEndTime.StartTime);
        var end = DateTime.Parse(hasStartEndTime.EndTime);
        return now >= start && now <= end;
    }
}
