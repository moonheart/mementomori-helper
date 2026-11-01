using MementoMori.Ortega.Share.Data.MiningQuest;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest;

[MessagePackObject(true)]
public class MiningQuestStartResponse : ApiResponseBase
{
    public int LimitTimeSeconds { get; set; }

    public MiningQuestParameterInfo ParameterInfo { get; set; }

    public List<MiningQuestBlockInfo> BlockInfoList { get; set; }

    public List<MiningQuestBlockRespawnInfo> BlockRespawnInfoList { get; set; }

    public List<MiningQuestSpecialAttackInfo> SpecialAttackInfoList { get; set; }

    public List<int> LayerBlockSizePercentList { get; set; }

    public List<int> LayerBlockCapacityWeightList { get; set; }

    public int Seed { get; set; }
}
