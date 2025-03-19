using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.TradeShop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.TradeShop
{
	[MessagePackObject(true)]
	public class GetListResponse : ApiResponseBase
	{
		public List<TradeShopTabInfo> TradeShopTabInfoList { get; set; }

        public long MinOpenQuestId { get; set; }

        public long MinOpenPartyLevel { get; set; }
    }
}
