using System.ComponentModel;

[Description("団結ミッション累計ポイント報酬の種別")]
public enum UnionMissionTotalPointRewardType
{
    [Description("不明")]
    None,
    [Description("ギルドポイント報酬")]
    Guild,
    [Description("個人ポイント報酬")]
    Personal
}
