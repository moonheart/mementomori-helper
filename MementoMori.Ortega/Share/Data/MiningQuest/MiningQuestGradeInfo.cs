using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Data.MiningQuest;

[MessagePackObject(true)]
public class MiningQuestGradeInfo
{
    [Description("階級タイプ")]
    public MiningQuestGradeType GradeType { get; set; }

    [Description("階級名")]
    public string NameTextKey { get; set; }

    [Description("必要スコア")]
    public long Score { get; set; }

    [Description("日次報酬リスト")]
    public List<UserItem> DailyRewardList { get; set; }

    [Description("日次報酬ステータス")]
    public MiningQuestDailyRewardStatusType DailyRewardStatusType { get; set; }

    [Description("シーズン報酬リスト")]
    public List<UserItem> SeasonRewardList { get; set; }
}
