using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("団結ミッション階級MB")]
	public class UnionMissionClassMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("階級")]
		public UnionMissionClassType ClassType
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("階級名")]
		public string ClassNameKey
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("階級説明テキスト")]
		public string ClassDescriptionKey
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("1ブロック当たりのギルド数")]
		public int GuildCountPerBlock
		{
			get;
		}

		[PropertyOrder(5)]
		[Description("1ブロック当たりのNPC数")]
		public int NpcCountPerBlock
		{
			get;
		}

		[PropertyOrder(6)]
		[Description("昇格条件の順位")]
		public long PromotionRank
		{
			get;
		}

		[PropertyOrder(7)]
		[Description("降格条件の順位")]
		public long RelegationRank
		{
			get;
		}

		[SerializationConstructor]
		public UnionMissionClassMB(long id, bool? isIgnore, string memo, UnionMissionClassType classType, string classNameKey, string classDescriptionKey, int guildCountPerBlock, int npcCountPerBlock, long promotionRank, long relegationRank)
			:base(id, isIgnore, memo)
		{
			ClassType = classType;
			ClassNameKey = classNameKey;
			ClassDescriptionKey = classDescriptionKey;
			GuildCountPerBlock = guildCountPerBlock;
			NpcCountPerBlock = npcCountPerBlock;
			PromotionRank = promotionRank;
			RelegationRank = relegationRank;
		}

		public UnionMissionClassMB():base(0, false, "")
		{
		}
	}
}
