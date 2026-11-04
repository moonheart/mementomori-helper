using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest
{
	[MessagePackObject(true)]
	[OrtegaApi("miningQuest/unlockBenefit", true, false)]
	public class MiningQuestUnlockBenefitRequest : ApiRequestBase
	{
	}
}
