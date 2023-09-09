using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop
{
	[MessagePackObject(true)]
	[OrtegaApi("shop/buyProduct", true, false)]
	public class BuyProductRequest : ApiRequestBase, IHasSteamTicketApiRequest
	{
		public long GivePlayerId { get; set; }

		public long MbId { get; set; }

		public string ProductId { get; set; }

        public string Receipt { get; set; }

        public ShopProductType ShopProductType { get; set; }

		public string SteamTicket { get; set; }
    }
}
