using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest
{
	[MessagePackObject(true)]
	[OrtegaApi("miningQuest/getInfo", true, false)]
	public class MiningQuestGetInfoRequest : ApiRequestBase
	{
	}
}
