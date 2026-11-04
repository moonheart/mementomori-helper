using MementoMori.Ortega.Share.Data.Shop;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("団結ミッションランキング報酬MB")]
	public class UnionMissionRankingRewardMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("階級")]
		public UnionMissionClassType ClassType
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("ランキング種類")]
		public UnionMissionRankingType RankingType
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("順位")]
		public long Rank
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(4)]
		[Description("報酬アイテムリスト")]
		public IReadOnlyList<RarityItem> RewardItemList
		{
			get;
		}

		[SerializationConstructor]
		public UnionMissionRankingRewardMB(long id, bool? isIgnore, string memo, UnionMissionClassType classType, UnionMissionRankingType rankingType, long rank, IReadOnlyList<RarityItem> rewardItemList)
			:base(id, isIgnore, memo)
		{
			ClassType = classType;
			RankingType = rankingType;
			Rank = rank;
			RewardItemList = rewardItemList;
		}

		public UnionMissionRankingRewardMB():base(0, false, "")
		{
		}
	}
}
