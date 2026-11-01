using MementoMori.Ortega.Share.Data.MiningQuest;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest;

[MessagePackObject(true)]
[OrtegaApi("miningQuest/finish", true, false)]
public class MiningQuestFinishRequest : ApiRequestBase
{
    public List<MiningQuestMinedBlockInfo> MinedBlockInfoList { get; set; }
}
