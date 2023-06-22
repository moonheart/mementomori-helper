using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	[OrtegaApi("dungeonBattle/finishBattle", true, false)]
	public class FinishBattleRequest : ApiRequestBase, IDungeonBattleRequest
	{
		public string DungeonGridGuid
		{
			get;
			set;
		}

		public int VisitDungeonCount
		{
			get;
			set;
		}

		public long CurrentTermId
		{
			get;
			set;
		}

		public FinishBattleRequest()
		{
		}
	}
}
