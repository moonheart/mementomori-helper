using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("フレンドミッション")]
	public class FriendMissionMB : MasterBookBase, IHasStartEndTime
	{
		[PropertyOrder(1)]
		[Description("ミッション名")]
		public string NameKey
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("遷移先")]
		public MissionTransitionDestinationType TransitionDestination
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("ミッションタイプ")]
		public MissionAchievementType AchievementType
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("達成要求値")]
		public long RequireValue
		{
			get;
		}

		[PropertyOrder(7)]
		[Description("表示優先度")]
		public int SortOrder
		{
			get;
		}

		[Description("報酬リスト")]
		[PropertyOrder(8)]
		[Nest(false, 0)]
		public IReadOnlyList<UserItem> RewardList
		{
			get;
		}

		[SerializationConstructor]
		public FriendMissionMB(long id, bool? isIgnore, string memo, string nameKey, MissionTransitionDestinationType transitionDestination, MissionAchievementType achievementType, long requireValue, string startTime, string endTime, int sortOrder, IReadOnlyList<UserItem> rewardList)
			:base(id, isIgnore, memo)
		{
			NameKey = nameKey;
			TransitionDestination = transitionDestination;
			AchievementType = achievementType;
			RequireValue = requireValue;
			StartTime = startTime;
			EndTime = endTime;
			SortOrder = sortOrder;
			RewardList = rewardList;
		}

		public FriendMissionMB() : base(0, false, "")
		{
		}

		[PropertyOrder(6)]
		[Description("終了時刻")]
		public string EndTime
		{
			get;
		}

		[PropertyOrder(5)]
		[Description("開始時刻")]
		public string StartTime
		{
			get;
		}
	}
}
