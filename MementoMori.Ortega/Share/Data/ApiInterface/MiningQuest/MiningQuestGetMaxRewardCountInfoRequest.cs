using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest
{
	[MessagePackObject(true)]
	[OrtegaApi("miningQuest/getMaxRewardCountInfo", true, false)]
	public class MiningQuestGetMaxRewardCountInfoRequest : ApiRequestBase
	{
	}
}
