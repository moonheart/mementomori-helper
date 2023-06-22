using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	[OrtegaApi("dungeonBattle/getBattleGridData", true, false)]
	public class GetBattleGridDataRequest : ApiRequestBase, IDungeonBattleRequest
	{
		public string DungeonGridGuid
		{
			get;
			set;
		}

		public long CurrentTermId
		{
			get;
			set;
		}

		public GetBattleGridDataRequest()
		{
		}
	}
}
