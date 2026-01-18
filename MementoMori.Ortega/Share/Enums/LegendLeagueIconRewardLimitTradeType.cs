using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
    [Description("レジェンドリーグアイコン報酬交換制限タイプ")]
    public enum LegendLeagueIconRewardLimitTradeType
    {
        [Description("永続")]
        Permanent = 1,
        [Description("月間")]
        Monthly
    }
}