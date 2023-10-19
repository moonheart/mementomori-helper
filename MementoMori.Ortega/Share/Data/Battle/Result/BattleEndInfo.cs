using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle.Result;

[MessagePackObject(true)]
public class BattleEndInfo
{
    public bool IsOutOfTurn { get; set; }

    public int EndTurn { get; set; }

    public BattleFieldCharacterGroupType WinGroupType { get; set; }

    public HashSet<long> WinPlayerIdSet { get; set; }

    public bool IsWinAttacker()
    {
        return WinGroupType == BattleFieldCharacterGroupType.Attacker;
    }

    public bool IsWin(long targetPlayerId)
    {
        return WinPlayerIdSet.Contains(targetPlayerId);
    }
}