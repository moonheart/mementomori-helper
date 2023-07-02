using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[OrtegaApi("dungeonBattle/rewardBattleReinforceRelic", true, false)]
	[MessagePackObject(true)]
	public class RewardBattleReinforceRelicRequest : ApiRequestBase, IDungeonBattleRequest
	{
		public string DungeonGridGuid{ get; set; }

		public long SelectedRelicId { get; set; }

		public long CurrentTermId { get; set; }
	}
}
