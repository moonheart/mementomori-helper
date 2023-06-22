using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
    [MessagePackObject(true)]
    [OrtegaApi("dungeonBattle/execRecovery", true, false)]
    public class ExecRecoveryRequest : ApiRequestBase, IDungeonBattleRequest
    {
        public string DungeonGridGuid { get; set; }

        public bool IsHealed { get; set; }

        public long CurrentTermId { get; set; }

        public ExecRecoveryRequest()
        {
        }
    }
}