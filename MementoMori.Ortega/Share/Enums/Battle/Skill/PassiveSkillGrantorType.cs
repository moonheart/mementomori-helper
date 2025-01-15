using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums.Battle.Skill;

public enum PassiveSkillGrantorType
{
    [Description("なし")] None,
    [Description("自分")] Self,
    [Description("加護")] Relic,
    [Description("ギルドバトル用効果")] GuildBattle,
    [Description("チーム用")] Team
}