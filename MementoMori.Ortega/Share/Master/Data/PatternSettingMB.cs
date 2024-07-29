using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data;

[MessagePackObject(true)]
[Description("PatternSetting")]
public class PatternSettingMB : MasterBookBase, IHasStartEndTime
{
    [SerializationConstructor]
    public PatternSettingMB(long id, bool? isIgnore, string memo, PatternSettingType patternSettingType, int numberInfo, IReadOnlyList<long> playerIdRemainConditionList,
        IReadOnlyList<long> timeServerIdConditionList, IReadOnlyList<long> progressQuestIdConditionList, IReadOnlyList<long> elapsedFromCreatePlayerDayConditionList, string startTime, string endTime)
        : base(id, isIgnore, memo)
    {
        PatternSettingType = patternSettingType;
        NumberInfo = numberInfo;
        PlayerIdRemainConditionList = playerIdRemainConditionList;
        TimeServerIdConditionList = timeServerIdConditionList;
        ProgressQuestIdConditionList = progressQuestIdConditionList;
        ElapsedFromCreatePlayerDayConditionList = elapsedFromCreatePlayerDayConditionList;
        StartTime = startTime;
        EndTime = endTime;
    }

    public PatternSettingMB()
        : base(0L, null, null)
    {
    }

    [PropertyOrder(6)]
    [Description("プレイヤー新規登録後経過日数範囲条件リスト")]
    public IReadOnlyList<long> ElapsedFromCreatePlayerDayConditionList { get; }

    [PropertyOrder(2)]
    [Description("指定番号")]
    public int NumberInfo { get; }

    [PropertyOrder(1)]
    [Description("PatternSetting種別")]
    public PatternSettingType PatternSettingType { get; }

    [PropertyOrder(3)]
    [Description("プレイヤーIDの上9桁を16で割った剰余条件リスト")]
    public IReadOnlyList<long> PlayerIdRemainConditionList { get; }

    [PropertyOrder(5)]
    [Description("クエスト進行度範囲条件リスト")]
    public IReadOnlyList<long> ProgressQuestIdConditionList { get; }

    [PropertyOrder(4)]
    [Description("TimeServerId条件リスト")]
    public IReadOnlyList<long> TimeServerIdConditionList { get; }

    [PropertyOrder(8)]
    [Description("終了時刻")]
    public string EndTime { get; }

    [PropertyOrder(7)]
    [Description("開始時刻")]
    public string StartTime { get; }
}