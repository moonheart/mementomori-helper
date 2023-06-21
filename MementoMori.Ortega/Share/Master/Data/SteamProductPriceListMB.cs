using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Shop;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("Steam商品価格表")]
	[MessagePackObject(true)]
	public class SteamProductPriceListMB : MasterBookBase
	{
		[Description("JPYでの価格")]
		[PropertyOrder(1)]
		public int JpyProductPrice
		{
			get;
		}

		[Nest(false, 0)]
		[Description("価格表")]
		[PropertyOrder(2)]
		public IReadOnlyList<SteamProductPriceInfo> ProductPriceInfoList
		{
			get;
		}

		[SerializationConstructor]
		public SteamProductPriceListMB(long id, bool? isIgnore, string memo, int jpyProductPrice, IReadOnlyList<SteamProductPriceInfo> productPriceInfoList)
			:base(id, isIgnore, memo)
		{
			JpyProductPrice = jpyProductPrice;
			ProductPriceInfoList = productPriceInfoList;
		}

		public SteamProductPriceListMB() :base(0L, false, ""){}
	}
}
