using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest
{
	[MessagePackObject(true)]
	[OrtegaApi("miningQuest/getBenefitInfo", true, false)]
	public class MiningQuestGetBenefitInfoRequest : ApiRequestBase
	{
	}
}
