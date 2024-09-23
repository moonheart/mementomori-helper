using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums;

[Description("週間トピックスの注目バトルのラベル種別")]
public enum WeeklyTopicsHighlightBattleLabelType
{
    [Description("不明")] None,
    [Description("Pvpで発生した上位同士のバトル")] PvpTopRankerBattle,
    [Description("Pvpで発生した長期戦になったバトル")] PvpLongBattle,
    [Description("Pvpで発生した格上相手に勝利したバトル")] PvpGiantKillingBattle,
    [Description("ワールド内で最初に章をクリア")] BossFirstChapterClearInWorld,
    [Description("章をクリア")] BossChapterClear,
    [Description("中ボスをクリア")] BossHardQuestClear
}