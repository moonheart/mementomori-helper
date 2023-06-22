using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
    [OrtegaApi("dungeonBattle/leaveShop", true, false)]
    [MessagePackObject(true)]
    public class LeaveShopRequest : ApiRequestBase, IDungeonBattleRequest
    {
        public string DungeonGridGuid { get; set; }

        public long CurrentTermId { get; set; }

        public LeaveShopRequest()
        {
        }
    }
}