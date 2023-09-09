using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class ShopGrowthPackBuffInfo
	{
		[PropertyOrder(2)]
		[Description("上昇するパラメータ")]
		public BattleParameterType BattleParameterType { get; set; }

		[PropertyOrder(3)]
		[Description("パラメータ増減タイプ")]
		public ChangeParameterType ChangeParameterType { get; set; }

		[PropertyOrder(1)]
		[Description("バフの影響範囲")]
		public GrowthPackBuffType GrowthPackBuffType { get; set; }

		[PropertyOrder(4)]
		[Description("値")]
		public long Value { get; set; }
	}
}
