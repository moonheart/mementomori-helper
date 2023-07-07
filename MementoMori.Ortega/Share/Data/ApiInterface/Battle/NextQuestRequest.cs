using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	[OrtegaApi("battle/nextQuest", true, false)]
	public class NextQuestRequest : ApiRequestBase
	{
	}
}
