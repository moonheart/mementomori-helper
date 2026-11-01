using MementoMori.Ortega.Share.Data.Shop;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("団結ミッション累計ポイント報酬MB")]
	public class UnionMissionTotalPointRewardMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("階級")]
		public UnionMissionClassType ClassType
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("必要ギルドポイント")]
		public long RequireGuildPoint
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("必要個人ポイント")]
		public long RequirePersonalPoint
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(4)]
		[Description("ギルドポイント報酬アイテムリスト")]
		public IReadOnlyList<RarityItem> GuildPointRewardItemList
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(5)]
		[Description("個人ポイント報酬アイテムリスト")]
		public IReadOnlyList<RarityItem> PersonalPointRewardItemList
		{
			get;
		}

		[SerializationConstructor]
		public UnionMissionTotalPointRewardMB(long id, bool? isIgnore, string memo, UnionMissionClassType classType, long requireGuildPoint, long requirePersonalPoint, IReadOnlyList<RarityItem> guildPointRewardItemList, IReadOnlyList<RarityItem> personalPointRewardItemList)
			:base(id, isIgnore, memo)
		{
			ClassType = classType;
			RequireGuildPoint = requireGuildPoint;
			RequirePersonalPoint = requirePersonalPoint;
			GuildPointRewardItemList = guildPointRewardItemList;
			PersonalPointRewardItemList = personalPointRewardItemList;
		}

		public UnionMissionTotalPointRewardMB():base(0, false, "")
		{
		}
	}
}
