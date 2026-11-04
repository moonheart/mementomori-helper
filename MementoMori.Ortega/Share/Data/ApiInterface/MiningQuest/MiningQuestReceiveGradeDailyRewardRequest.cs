using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest
{
	[MessagePackObject(true)]
	[OrtegaApi("miningQuest/receiveGradeDailyReward", true, false)]
	public class MiningQuestReceiveGradeDailyRewardRequest : ApiRequestBase
	{
	}
}
