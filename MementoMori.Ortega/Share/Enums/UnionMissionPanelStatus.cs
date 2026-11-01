using System.ComponentModel;

[Description("団結ミッションのミッションパネル状態")]
public enum UnionMissionPanelStatus
{
    [Description("不明")]
    None,
    [Description("未受注")]
    NotAccepted,
    [Description("受注中")]
    Accepted,
    [Description("更新待ち")]
    WaitingRefresh,
    [Description("完了済み")]
    Completed
}
