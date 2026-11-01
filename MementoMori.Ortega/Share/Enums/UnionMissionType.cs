using System.ComponentModel;

[Description("団結ミッションのミッションタイプ")]
public enum UnionMissionType
{
    [Description("不明")]
    None,
    [Description("個人ミッション")]
    Personal,
    [Description("共用ミッション")]
    Shared
}
