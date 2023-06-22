using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	[OrtegaApi("dungeonBattle/execBattle", true, false)]
	public class ExecBattleRequest : ApiRequestBase, IDungeonBattleRequest
	{
		public List<string> CharacterGuids
		{
			get;
			set;
		}

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

		public ExecBattleRequest()
		{
		}
	}
}
