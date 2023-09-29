using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	[OrtegaApi("dungeonBattle/execShop", true, false)]
	public class ExecShopRequest : ApiRequestBase, IDungeonBattleRequest
	{
		public string DungeonGridGuid { get; set; }

		public long TradeShopItemId { get; set; }

		public long CurrentTermId { get; set; }
	}
}
