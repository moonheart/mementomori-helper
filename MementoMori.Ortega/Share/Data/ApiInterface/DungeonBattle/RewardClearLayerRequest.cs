using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	[OrtegaApi("dungeonBattle/rewardClearLayer", true, false)]
	public class RewardClearLayerRequest : ApiRequestBase, IDungeonBattleRequest
	{
		public int ClearedLayer
		{
			get;
			set;
		}

		public DungeonBattleDifficultyType DungeonBattleDifficultyType
		{
			get;
			set;
		}

		public long CurrentTermId
		{
			get;
			set;
		}

		public RewardClearLayerRequest()
		{
		}
	}
}
