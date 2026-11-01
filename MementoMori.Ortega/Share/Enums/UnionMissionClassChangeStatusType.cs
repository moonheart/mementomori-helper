using System.ComponentModel;

[Description("団結ミッションの階級変動")]
public enum UnionMissionClassChangeStatusType
{
    [Description("未設定")]
    None,
    [Description("階級維持")]
    Keep,
    [Description("階級昇格")]
    Promotion,
    [Description("階級降格")]
    Relegation
}
