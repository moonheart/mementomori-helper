using System.ComponentModel;
using MementoMori.Ortega.Share.Data.GlobalGvg;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("グランドバトル城")]
	public class GlobalGvgCastleMB : MasterBookBase
	{
		[PropertyOrder(4)]
		[Description("隣接する城のIDリスト")]
		public IReadOnlyList<long> AdjacentCastleIds
		{
			get;
		}

		[Description("拠点ポイント")]
		[PropertyOrder(3)]
		public int CastlePoint
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("城タイプ")]
		public CastleType CastleType
		{
			get;
		}

		[Description("拠点報酬_固定")]
		[PropertyOrder(6)]
		[Nest(true, 0)]
		public GlobalGvgFixedRewards GlobalGvgFixedRewards
		{
			get;
		}

		[Description("抽選報酬ID")]
		[PropertyOrder(7)]
		public long LotteryRewardId
		{
			get;
		}

		[Description("名称キー")]
		[PropertyOrder(1)]
		public string NameKey
		{
			get;
		}

		[PropertyOrder(5)]
		[Description("初期城グループ")]
		public int Priority
		{
			get;
		}

		[SerializationConstructor]
		public GlobalGvgCastleMB(long id, bool? isIgnore, string memo, IReadOnlyList<long> adjacentCastleIds, int castlePoint, CastleType castleType, GlobalGvgFixedRewards globalGvgFixedRewards, string nameKey, long lotteryRewardId, int priority)
			:base(id, isIgnore, memo)
		{
			AdjacentCastleIds = adjacentCastleIds;
			CastlePoint = castlePoint;
			CastleType = castleType;
			GlobalGvgFixedRewards = globalGvgFixedRewards;
			NameKey = nameKey;
			LotteryRewardId = lotteryRewardId;
			Priority = priority;
		}

		public GlobalGvgCastleMB() : base(0, false, "")
		{
		}

		public IReadOnlyList<UserItem> GetFixedRewards(GlobalGvgGroupType globalGvgGroupType)
		{
			int num = globalGvgGroupType - GlobalGvgGroupType.Bronze;
			if (num == 0)
			{
				return this.GlobalGvgFixedRewards.LowerClass;
			}
			if (num != 0)
			{
				if (num != 1)
				{
					List<UserItem> list = new List<UserItem>();
					IReadOnlyList<UserItem> UpperClass = this.GlobalGvgFixedRewards.UpperClass;
					list.AddRange(UpperClass);
					IReadOnlyList<UserItem> MediumClass = this.GlobalGvgFixedRewards.MediumClass;
					list.AddRange(MediumClass);
					IReadOnlyList<UserItem> LowerClass = this.GlobalGvgFixedRewards.LowerClass;
					list.AddRange(LowerClass);
				}
				return this.GlobalGvgFixedRewards.UpperClass;
			}
			return this.GlobalGvgFixedRewards.MediumClass;
		}
	}
}
