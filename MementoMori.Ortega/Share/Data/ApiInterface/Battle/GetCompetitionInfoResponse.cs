using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle;

[MessagePackObject(true)]
public class GetCompetitionInfoResponse : ApiResponseBase
{
    public bool ExistDungeonBattleMissedCompensation { get; set; }

    public int DungeonBattleMissedCount { get; set; }

    public bool IsDungeonBattleEventOpen { get; set; }

    public ItemType DungeonBattleEventItemType { get; set; }

    public long DungeonBattleEventItemId { get; set; }
}