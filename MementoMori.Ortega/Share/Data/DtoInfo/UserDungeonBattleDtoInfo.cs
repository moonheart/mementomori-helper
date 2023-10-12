using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo;

[MessagePackObject(true)]
public class UserDungeonBattleDtoInfo
{
    public List<long> CurrentBoughtShopCounts { get; set; }

    public string CurrentGridGuid { get; set; }

    public DungeonBattleGridState CurrentGridState { get; set; }

    public List<string> DoneGridGuids { get; set; }

    public List<long> DoneRewardClearLayers { get; set; }

    public Dictionary<string, List<long>> GuestCharacterMap { get; set; }

    public bool IsLostLatestBattle { get; set; }

    public List<long> RelicIds { get; set; }

    public int UseDungeonRecoveryItemCount { get; set; }

    public bool IsDoneRewardClearLayer(int layerCount)
    {
        return DoneRewardClearLayers.Contains(layerCount);
    }

    public UserDungeonBattleDtoInfo()
    {
    }
}