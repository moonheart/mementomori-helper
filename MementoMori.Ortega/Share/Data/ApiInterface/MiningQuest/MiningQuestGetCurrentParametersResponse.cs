using MementoMori.Ortega.Share.Data.MiningQuest;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest;

[MessagePackObject(true)]
public class MiningQuestGetCurrentParametersResponse : ApiResponseBase
{
    public int AttackPower { get; set; }

    public int AttackRange { get; set; }

    public int AttackFrequency { get; set; }

    public int AttackSpeedAdditionalRate { get; set; }

    public int SpecialAttackRange { get; set; }

    public int SpecialAttackCoolTimeMilliseconds { get; set; }

    public int SpecialAttackSimultaneousCount { get; set; }

    public int BlockGenerateLimit { get; set; }

    public int RespawnBlockLevel { get; set; }

    public int LimitTimeSeconds { get; set; }

    public int RewardCountAdditionalRate { get; set; }

    public int SpawnBlockLevel { get; set; }

    public List<MiningQuestBlockLotteryInfo> BlockLotteryInfoList { get; set; }
}
