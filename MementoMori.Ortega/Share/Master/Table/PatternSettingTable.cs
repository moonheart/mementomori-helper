using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table;

public class PatternSettingTable : TableBase<PatternSettingMB>
{
    public bool GetByAllCompleteCondition(long nowTimestamp, DateTime nowDateTime, long playerId, long timeServerId, long currentMaxQuestId, long createPlayerTimestamp,
        PatternSettingType patternSettingType)
    {
        return false;
    }

    public bool IsMissionDisplayOrderA(OrtegaTimeManager timeManager, long playerId, long timeServerId)
    {
        return false;
    }

    public bool IsDynamicLinkNumberInfoOne(OrtegaTimeManager timeManager, long userId, long timeServerId)
    {
        return false;
    }

    public int GetNumberInfoFirstCharge(OrtegaTimeManager timeManager, long playerId, long timeServerId, long currentMaxQuestId, long createPlayerTimestamp)
    {
        return 0;
    }
}