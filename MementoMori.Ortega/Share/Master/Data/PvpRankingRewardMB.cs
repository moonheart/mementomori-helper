using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("PVPランキング報酬")]
	[MessagePackObject(true)]
	public class PvpRankingRewardMB : MasterBookBase
	{
		[Description("下限")]
		[PropertyOrder(2)]
		public long LowerLimitRanking
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("PVPランキング報酬タイプ")]
		public PvpRankingRewardType PvpRankingRewardType
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(4)]
		[Description("報酬リスト")]
		public IReadOnlyList<UserItem> RewardList
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("上限")]
		public long UpperLimitRanking
		{
			get;
		}

		[SerializationConstructor]
		public PvpRankingRewardMB(long id, bool? isIgnore, string memo, long lowerLimitRanking, PvpRankingRewardType pvpRankingRewardType, IReadOnlyList<UserItem> rewardList, long upperLimitRanking)
			:base(id, isIgnore, memo)
		{
			LowerLimitRanking = lowerLimitRanking;
			PvpRankingRewardType = pvpRankingRewardType;
			RewardList = rewardList;
			UpperLimitRanking = upperLimitRanking;
		}

		public PvpRankingRewardMB() :base(0L, false, ""){}
	}
}
