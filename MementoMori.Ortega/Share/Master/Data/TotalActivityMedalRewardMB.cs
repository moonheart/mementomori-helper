using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("累計貢献メダル報酬")]
	public class TotalActivityMedalRewardMB : MasterBookBase, IHasEventStartEndTime
	{
		[PropertyOrder(1)]
		[Description("ミッション種別")]
		public MissionGroupType MissionGroupType
		{
			get;
		}

		[Description("必要貢献メダル")]
		[PropertyOrder(2)]
		public long RequiredActivityMedalCount
		{
			get;
		}

		[PropertyOrder(3)]
		[Nest(false, 0)]
		[Description("報酬リスト")]
		public IReadOnlyList<MissionReward> RewardList
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("イベント開始時刻")]
		public string EventStartTime
		{
			get;
		}

		[Description("イベント終了時刻")]
		[PropertyOrder(5)]
		public string EventEndTime
		{
			get;
		}

		[PropertyOrder(6)]
		[Nest(false, 0)]
		[Description("イベント報酬リスト")]
		public IReadOnlyList<EventMissionReward> EventMissionRewardList
		{
			get;
		}

		[SerializationConstructor]
		public TotalActivityMedalRewardMB(long id, bool? isIgnore, string memo, MissionGroupType missionGroupType, long requiredActivityMedalCount, IReadOnlyList<MissionReward> rewardList, string eventStartTime, string eventEndTime, IReadOnlyList<EventMissionReward> eventMissionRewardList)
			:base(id, isIgnore, memo)
		{
			MissionGroupType = missionGroupType;
			RequiredActivityMedalCount = requiredActivityMedalCount;
			RewardList = rewardList;
			EventStartTime = eventStartTime;
			EventEndTime = eventEndTime;
			EventMissionRewardList = eventMissionRewardList;
		}

		public TotalActivityMedalRewardMB() :base(0L, false, ""){}
	}
}
