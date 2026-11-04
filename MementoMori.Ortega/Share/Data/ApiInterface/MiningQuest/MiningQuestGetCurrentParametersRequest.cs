using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest
{
	[MessagePackObject(true)]
	[OrtegaApi("miningQuest/getCurrentParameters", true, false)]
	public class MiningQuestGetCurrentParametersRequest : ApiRequestBase
	{
	}
}
