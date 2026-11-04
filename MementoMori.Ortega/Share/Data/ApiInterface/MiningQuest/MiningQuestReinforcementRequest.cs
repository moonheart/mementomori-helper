using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest
{
	[MessagePackObject(true)]
	[OrtegaApi("miningQuest/reinforcement", true, false)]
	public class MiningQuestReinforcementRequest : ApiRequestBase
	{
		public long MiningQuestReinforcementId { get; set; }
	}
}
