using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle.Result;

[MessagePackObject(true)]
public class BattleResult
{
    public BattleTime BattleTime { get; set; }

    public long QuestId { get; set; }

    public BattleReward Reward { get; set; }

    public BattleSimulationResult SimulationResult { get; set; }
}