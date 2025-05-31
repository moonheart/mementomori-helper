using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Data.Mission;
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
        public MissionGroupType MissionGroupType { get; }

        [PropertyOrder(2)]
        [Description("必要クリア数")]
        public long RequiredClearCount { get; }

        [Nest(false, 0)]
        [PropertyOrder(3)]
        [Description("報酬リスト")]
        public IReadOnlyList<MissionReward> RewardList { get; }

        [Nest(false, 0)]
        [PropertyOrder(4)]
        [Description("報酬情報リスト")]
        public IReadOnlyList<MissionClearCountRewardInfo> RewardInfoList { get; }

		[SerializationConstructor]
		public MissionClearCountRewardMB(long id, bool? isIgnore, string memo, MissionGroupType missionGroupType, long requiredClearCount, IReadOnlyList<MissionReward> rewardList, IReadOnlyList<MissionClearCountRewardInfo> rewardInfoList)
			:base(id, isIgnore, memo)
		{
			MissionGroupType = missionGroupType;
			RequiredClearCount = requiredClearCount;
			RewardList = rewardList;
            RewardInfoList = rewardInfoList;
		}

		public MissionClearCountRewardMB() :base(0L, false, ""){}
	}
}
