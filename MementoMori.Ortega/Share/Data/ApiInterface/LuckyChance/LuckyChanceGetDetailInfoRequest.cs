using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LuckyChance
{
	[MessagePackObject(true)]
	[OrtegaApi("luckyChance/GetDetailInfo", true, false)]
	public class LuckyChanceGetDetailInfoRequest : ApiRequestBase
	{
		public long LuckyChanceButtonId { get; set; }
	}
}
