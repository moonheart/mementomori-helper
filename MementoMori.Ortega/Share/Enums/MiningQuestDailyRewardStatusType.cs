using System.ComponentModel;

public enum MiningQuestDailyRewardStatusType
{
    [Description("不明")]
    None,
    [Description("未解放")]
    Locked,
    [Description("未受取")]
    NotReceived,
    [Description("受取済み")]
    Received
}
