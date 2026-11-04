using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Debug
{
	[MessagePackObject(true)]
	[OrtegaApi("debug/setMiningQuestMaxScore", true, false)]
	public class SetMiningQuestMaxScoreRequest : ApiRequestBase
	{
		public int MaxScore { get; set; }
	}
}
