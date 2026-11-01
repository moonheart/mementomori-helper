using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Debug
{
	[MessagePackObject(true)]
	[OrtegaApi("debug/setMiningQuestBenefitPurchaseCount", true, false)]
	public class SetMiningQuestBenefitPurchaseCountRequest : ApiRequestBase
	{
		public int BenefitPurchaseCount { get; set; }
	}
}
