using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.TradeShop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.TradeShop
{
	[MessagePackObject(true)]
	public class ResetTabResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public long LastFreeManualUpdateTime { get; set; }

		public List<TradeShopItem> TradeShopItems { get; set; }

        public UserSyncData UserSyncData { get; set; }
    }
}
