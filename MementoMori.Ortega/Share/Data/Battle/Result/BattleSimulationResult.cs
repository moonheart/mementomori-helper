using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.RentalRaid;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle.Result;

[MessagePackObject(true)]
public class BattleSimulationResult
{
    public BattleEndInfo BattleEndInfo { get; set; }

    public BattleField BattleField { get; set; }

    public BattleLog BattleLog { get; set; }
    public string BattleToken { get; set; }

    public List<BattleCharacterReport> BattleCharacterReports { get; set; }

    public ShareCharacterOwnerInfo ShareCharacterOwnerInfo { get; set; }
}