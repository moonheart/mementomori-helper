using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Gacha
{
	[MessagePackObject(true)]
	[OrtegaApi("gacha/getLotteryItemList", true, false)]
	public class GetLotteryItemListRequest : ApiRequestBase
	{
		public long GachaButtonId { get; set; }
	}
}
