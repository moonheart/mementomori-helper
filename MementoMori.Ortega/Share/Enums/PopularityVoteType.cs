using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums;

[Description("人気投票タイプ")]
public enum PopularityVoteType
{
    [Description("なし")] None,
    [Description("準備期限")] Ready,
    [Description("予選投票期限")] Preliminary,
    [Description("集計期限1")] Aggregation1,
    [Description("本選投票期限")] Final,
    [Description("集計期限2")] Aggregation2,
    [Description("結果発表")] Result
}