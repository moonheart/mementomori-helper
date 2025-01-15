using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.LocalRaid;

[MessagePackObject(true)]
public class LocalRaidEnemyInfo
{
    public long Id { get; set; }

    public UnitIconType UnitIconType { get; set; }

    public long UnitIconId { get; set; }

    public long BattlePower { get; set; }

    public ElementType ElementType { get; set; }

    public CharacterRarityFlags CharacterRarityFlags { get; set; }

    public long EnemyRank { get; set; }

    public string NameKey { get; set; }
}