using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums;

[Description("ヘルプ付加情報タイプ")]
public enum HelpParameterType
{
    [Description("無し")] None,
    [Description("所属するワールド")] BelongingWorlds,
    [Description("ギルドバトル最小配置数")] GuildBattleMinPlacement,
    [Description("グランドバトル最小配置数")] GrandBattleMinPlacement,
    [Description("人気投票期間情報")] PopularityVoteTimeInfo
}