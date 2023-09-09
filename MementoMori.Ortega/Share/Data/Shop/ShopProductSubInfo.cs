using System.ComponentModel;
using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class ShopProductSubInfo
	{
		[Description("商品Id")]
		public string ProductId
		{
			get;set;
		}

		[Description("商品値段")]
		public int ShopProductPrice { get; set; }
	}
}
