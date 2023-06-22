using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	[OrtegaApi("dungeonBattle/proceedLayer", true, false)]
	public class ProceedLayerRequest : ApiRequestBase, IDungeonBattleRequest
	{
		public DungeonBattleDifficultyType DungeonDifficultyType
		{
			get;
			set;
		}

		public long CurrentTermId
		{
			get;
			set;
		}

		public ProceedLayerRequest()
		{
		}
	}
}
