using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class ShopCurrencyMissionInfo
	{
		[Nest(true, 1)]
		[Description("商品ID")]
		public UserItem CommonRewardItem { get; set; }

        [Nest(true, 1)]
		[Description("商品種別タイプ")]
		public UserItem PremiumRewardItem1 { get; set; }

        [Nest(true, 1)]
		[Description("商品種別タイプ")]
		public UserItem PremiumRewardItem2 { get; set; }

        [Description("要求Pt")]
		public int RequiredPoint { get; set; }
	}
}
