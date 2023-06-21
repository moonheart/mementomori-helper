using System.ComponentModel;
using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[Description("Steam価格表情報")]
	[MessagePackObject(true)]
	public class SteamProductPriceInfo
	{
		[Description("通貨コード")]
		public string CurrencyCode
		{
			get;
			set;
		}

		[Description("表示倍率")]
		public float Multiple
		{
			get;
			set;
		}

		[Description("価格")]
		public long ProductPrice
		{
			get;
			set;
		}

		public SteamProductPriceInfo()
		{
		}
	}
}
