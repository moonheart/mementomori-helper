using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.TradeShop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.TradeShop
{
	[MessagePackObject(true)]
	public class BuyItemResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<TradeShopItem> TradeShopItems { get; set; }

        public UserSyncData UserSyncData { get; set; }
    }
}
