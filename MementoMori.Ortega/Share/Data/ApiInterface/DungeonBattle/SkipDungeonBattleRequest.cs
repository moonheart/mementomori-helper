using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	[OrtegaApi("dungeonBattle/skipBattle", true, false)]
	public class SkipDungeonBattleRequest : ApiRequestBase, IDungeonBattleRequest
	{
		public long CurrentTermId { get; set; }
	}
}
