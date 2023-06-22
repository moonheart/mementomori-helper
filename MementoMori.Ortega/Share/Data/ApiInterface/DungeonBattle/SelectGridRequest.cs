using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[OrtegaApi("dungeonBattle/selectGrid", true, false)]
	[MessagePackObject(true)]
	public class SelectGridRequest : ApiRequestBase, IDungeonBattleRequest
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

		public SelectGridRequest()
		{
		}
	}
}
