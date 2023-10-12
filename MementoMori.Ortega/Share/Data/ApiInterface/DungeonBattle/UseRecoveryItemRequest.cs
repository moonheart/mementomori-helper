using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	[OrtegaApi("dungeonBattle/useRecoveryItem", true, false)]
	public class UseRecoveryItemRequest : ApiRequestBase, IDungeonBattleRequest
	{
		public long CurrentTermId { get; set; }
	}
}
