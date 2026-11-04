using MementoMori.Ortega.Share.Data.Shop;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("団結ミッション繰り返し個人報酬MB")]
	public class UnionMissionRepeatPersonalRewardMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("階級")]
		public UnionMissionClassType ClassType
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("必要個人ポイント")]
		public long RequirePersonalPoint
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(3)]
		[Description("報酬アイテムリスト")]
		public IReadOnlyList<RarityItem> RewardItemList
		{
			get;
		}

		[SerializationConstructor]
		public UnionMissionRepeatPersonalRewardMB(long id, bool? isIgnore, string memo, UnionMissionClassType classType, long requirePersonalPoint, IReadOnlyList<RarityItem> rewardItemList)
			:base(id, isIgnore, memo)
		{
			ClassType = classType;
			RequirePersonalPoint = requirePersonalPoint;
			RewardItemList = rewardItemList;
		}

		public UnionMissionRepeatPersonalRewardMB():base(0, false, "")
		{
		}
	}
}
