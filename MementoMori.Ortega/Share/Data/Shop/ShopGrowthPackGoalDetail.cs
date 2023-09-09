using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class ShopGrowthPackGoalDetail
	{
		[Description("目標レアリティ")]
		public CharacterRarityFlags CharacterRarityFlags { get; set; }

		[Description("商品リスト")]
		public List<ShopProductGrowthPackDetail> ShopProductGrowthPackInfoList { get; set; }
    }
}
