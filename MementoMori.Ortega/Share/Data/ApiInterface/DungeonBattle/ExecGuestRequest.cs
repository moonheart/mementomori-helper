using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[OrtegaApi("dungeonBattle/execGuest", true, false)]
	[MessagePackObject(true)]
	public class ExecGuestRequest : ApiRequestBase, IDungeonBattleRequest
	{
		public string DungeonGridGuid { get; set; }

		public long GuestMBId { get; set; }

		public long CurrentTermId { get; set; }
	}
}
