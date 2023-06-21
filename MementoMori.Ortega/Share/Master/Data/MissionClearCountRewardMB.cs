using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("ミッションクリア個数報酬")]
	public class MissionClearCountRewardMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("ミッション種別")]
		public MissionGroupType MissionGroupType
		{
			get;
		}

		[Description("必要クリア数")]
		[PropertyOrder(2)]
		public long RequiredClearCount
		{
			get;
		}

		[Nest(false, 0)]
		[Description("報酬リスト")]
		[PropertyOrder(3)]
		public IReadOnlyList<MissionReward> RewardList
		{
			get;
		}

		[SerializationConstructor]
		public MissionClearCountRewardMB(long id, bool? isIgnore, string memo, MissionGroupType missionGroupType, long requiredClearCount, IReadOnlyList<MissionReward> rewardList)
			:base(id, isIgnore, memo)
		{
			MissionGroupType = missionGroupType;
			RequiredClearCount = requiredClearCount;
			RewardList = rewardList;
		}

		public MissionClearCountRewardMB() :base(0L, false, ""){}
	}
}
