using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[OrtegaApi("dungeonBattle/execReinforceRelic", true, false)]
	[MessagePackObject(true)]
	public class ExecReinforceRelicRequest : ApiRequestBase, IDungeonBattleRequest
	{
		public string DungeonGridGuid{ get; set; }

		public long CurrentTermId { get; set; }
	}
}
