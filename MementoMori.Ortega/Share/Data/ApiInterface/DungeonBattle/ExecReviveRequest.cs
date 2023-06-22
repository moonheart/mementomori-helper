using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
    [MessagePackObject(true)]
    [OrtegaApi("dungeonBattle/execRevive", true, false)]
    public class ExecReviveRequest : ApiRequestBase, IDungeonBattleRequest
    {
        public string DungeonGridGuid { get; set; }

        public bool IsRevived { get; set; }

        public long CurrentTermId { get; set; }

        public ExecReviveRequest()
        {
        }
    }
}