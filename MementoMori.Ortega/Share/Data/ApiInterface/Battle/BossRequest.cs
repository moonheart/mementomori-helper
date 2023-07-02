using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[OrtegaApi("battle/boss", true, false)]
	[MessagePackObject(true)]
	public class BossRequest : ApiRequestBase
	{
		public long QuestId { get; set; }
	}
}
