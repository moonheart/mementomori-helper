using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
    [Description("ミッションクリア個数/累計貢献メダル報酬状態種別")]
    public enum MissionActivityRewardStatusType
    {
        [Description("未解放")] Locked,
        [Description("獲得可能")] NotReceived,
        [Description("獲得済")] Received
    }
}