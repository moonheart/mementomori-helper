using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("鋳造データ")]
	[MessagePackObject(true)]
	public class EquipmentForgeMB : MasterBookBase
	{
		[PropertyOrder(3)]
		[Description("魔装ステータス")]
		public bool MatchlessSacredTreasure
		{
			get;
		}

		[Description("得られる鋳造値")]
		[PropertyOrder(4)]
		public long PriceForForge
		{
			get;
		}

		[Description("成功時の武具レアリティ")]
		[PropertyOrder(2)]
		public EquipmentRarityFlags SuccessRarityFlag
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("成功率ID")]
		public long SuccessRateId
		{
			get;
		}

		[SerializationConstructor]
		public EquipmentForgeMB(long id, bool? isIgnore, string memo, long successRateId, EquipmentRarityFlags successRarityFlag, bool matchlessSacredTreasure, long priceForForge)
			:base(id, isIgnore, memo)
		{
			SuccessRateId = successRateId;
			SuccessRarityFlag = successRarityFlag;
			MatchlessSacredTreasure = matchlessSacredTreasure;
			PriceForForge = priceForForge;
		}

		public EquipmentForgeMB() : base(0, false, "")
		{
		}
	}
}
