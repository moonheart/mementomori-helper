using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("ミッション")]
	[MessagePackObject(true)]
	public class MissionMB : MasterBookBase, IHasStartEndTime
	{
		[Description("ミッションタイプ")]
		[PropertyOrder(6)]
		public MissionAchievementType AchievementType
		{
			get;
		}

		[Description("ミッション説明")]
		[PropertyOrder(4)]
		public string DescriptionKey
		{
			get;
		}

		[Description("ミッション種別")]
		[PropertyOrder(1)]
		public MissionType MissionType
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("ミッション名")]
		public string NameKey
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("開放日数")]
		public int OpeningPeriod
		{
			get;
		}

		[Description("達成要求値")]
		[PropertyOrder(7)]
		public long RequireValue
		{
			get;
		}

		[Description("報酬リスト")]
		[PropertyOrder(12)]
		[Nest(false, 0)]
		public IReadOnlyList<MissionReward> RewardList
		{
			get;
		}

		[Description("表示優先度A")]
		[PropertyOrder(10)]
		public int SortOrderA
		{
			get;
		}

		[Description("表示優先度B")]
		[PropertyOrder(11)]
		public int SortOrderB
		{
			get;
		}

		[PropertyOrder(5)]
		[Description("遷移先")]
		public MissionTransitionDestinationType TransitionDestination
		{
			get;
		}

		[SerializationConstructor]
		public MissionMB(long id, bool? isIgnore, string memo, MissionType missionType, int openingPeriod, string nameKey, string descriptionKey, MissionTransitionDestinationType transitionDestination, MissionAchievementType achievementType, long requireValue, string startTime, string endTime, int sortOrderA, int sortOrderB, IReadOnlyList<MissionReward> rewardList)
			:base(id, isIgnore, memo)
		{
			MissionType = missionType;
			OpeningPeriod = openingPeriod;
			NameKey = nameKey;
			DescriptionKey = descriptionKey;
			TransitionDestination = transitionDestination;
			AchievementType = achievementType;
			RequireValue = requireValue;
			SortOrderA = sortOrderA;
			SortOrderB = sortOrderB;
			RewardList = rewardList;
		}

		public MissionMB() :base(0L, false, ""){}

		[PropertyOrder(9)]
		[Description("終了時刻")]
		public string EndTime
		{
			get;
		}

		[Description("開始時刻")]
		[PropertyOrder(8)]
		public string StartTime
		{
			get;
		}

		public MissionGroupType GetMissionGroupType()
		{
			return MissionUtil.MissionTypeToMissionGroup(this.MissionType);
		}
	}
}
