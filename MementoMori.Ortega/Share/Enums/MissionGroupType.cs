using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
    [Description("ミッショングループ")]
    public enum MissionGroupType
    {
        [Description("メイン")] Main,
        [Description("デイリー")] Daily,
        [Description("ウィークリー")] Weekly,
        [Description("初心者")] Beginner,
        [Description("カムバック")] Comeback,
        [Description("新キャラ")] NewCharacter,
        [Description("イベント")] Limited,
        [Description("パネル")] Panel = 9,
        [Description("ギルドミッション")] Guild
    }
}