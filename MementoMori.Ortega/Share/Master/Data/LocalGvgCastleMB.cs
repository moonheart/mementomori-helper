using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("ギルドバトル城")]
	public class LocalGvgCastleMB : MasterBookBase
	{
		[Description("隣接する城のIDリスト")]
		[PropertyOrder(3)]
		public IReadOnlyList<long> AdjacentCastleIds
		{
			get;
		}

		[Description("城タイプ")]
		[PropertyOrder(2)]
		public CastleType CastleType
		{
			get;
		}

		[Description("拠点報酬_固定")]
		[PropertyOrder(4)]
		[Nest(false, 0)]
		public IReadOnlyList<UserItem> LocalGvgFixedRewards
		{
			get;
		}

		[Description("拠点確率報酬ID")]
		[PropertyOrder(5)]
		public long LotteryRewardId
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("名称キー")]
		public string NameKey
		{
			get;
		}

		[SerializationConstructor]
		public LocalGvgCastleMB(long id, bool? isIgnore, string memo, IReadOnlyList<long> adjacentCastleIds, CastleType castleType, IReadOnlyList<UserItem> localGvgFixedRewards, long lotteryRewardId, string nameKey)
			:base(id, isIgnore, memo)
		{
			AdjacentCastleIds = adjacentCastleIds;
			CastleType = castleType;
			LocalGvgFixedRewards = localGvgFixedRewards;
			LotteryRewardId = lotteryRewardId;
			NameKey = nameKey;
		}

		public LocalGvgCastleMB() :base(0L, false, ""){}
	}
}
