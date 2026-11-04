using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest
{
	[MessagePackObject(true)]
	[OrtegaApi("miningQuest/start", true, false)]
	public class MiningQuestStartRequest : ApiRequestBase
	{
	}
}
