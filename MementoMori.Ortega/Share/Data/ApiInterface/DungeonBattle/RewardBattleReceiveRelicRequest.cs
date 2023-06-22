using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
    [MessagePackObject(true)]
    [OrtegaApi("dungeonBattle/rewardBattleReceiveRelic", true, false)]
    public class RewardBattleReceiveRelicRequest : ApiRequestBase, IDungeonBattleRequest
    {
        public string DungeonGridGuid { get; set; }

        public long SelectedRelicId { get; set; }

        public long CurrentTermId { get; set; }

        public RewardBattleReceiveRelicRequest()
        {
        }
    }
}