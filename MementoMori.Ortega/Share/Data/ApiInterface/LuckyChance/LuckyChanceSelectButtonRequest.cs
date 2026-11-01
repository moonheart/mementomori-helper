using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LuckyChance
{
	[MessagePackObject(true)]
	[OrtegaApi("luckyChance/SelectButton", true, false)]
	public class LuckyChanceSelectButtonRequest : ApiRequestBase
	{
		public long LuckyChanceButtonId { get; set; }
	}
}
