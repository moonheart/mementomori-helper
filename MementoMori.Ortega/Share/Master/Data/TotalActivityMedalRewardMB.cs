﻿using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data;

[MessagePackObject(true)]
[Description("累計貢献メダル報酬")]
public class TotalActivityMedalRewardMB : MasterBookBase, IHasStartEndTime
{
    [SerializationConstructor]
    public TotalActivityMedalRewardMB(long id, bool? isIgnore, string memo, MissionGroupType missionGroupType, long requiredActivityMedalCount, long value, IReadOnlyList<MissionReward> rewardList,
        string startTime, string endTime, int sortOrder)
        : base(0L, null, null)
    {
    }

    public TotalActivityMedalRewardMB() : base(0L, false, "")
    {
    }

    [PropertyOrder(1)]
    [Description("ミッション種別")]
    public MissionGroupType MissionGroupType { get; }

    [PropertyOrder(2)]
    [Description("MissionGroupTypeごとの指定値")]
    public long Value { get; }

    [PropertyOrder(3)]
    [Description("必要貢献メダル")]
    public long RequiredActivityMedalCount { get; }

    [Nest()]
    [PropertyOrder(4)]
    [Description("報酬リスト")]
    public IReadOnlyList<MissionReward> RewardList { get; }

    [PropertyOrder(7)]
    [Description("表示順(昇順)")]
    public int SortOrder { get; }

    [PropertyOrder(5)]
    [Description("開始時刻")]
    public string StartTime { get; }

    [PropertyOrder(6)]
    [Description("終了時刻")]
    public string EndTime { get; }
}