using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.LocalRaid;

[MessagePackObject(true)]
public class LocalRaidQuestInfo
{
    public long Id { get; set; }

    public IReadOnlyList<UserItem> FirstBattleRewards { get; set; }

    public IReadOnlyList<UserItem> FixedBattleRewards { get; set; }

    public IReadOnlyList<long> LocalRaidEnemyIds { get; set; }

    public int Level { get; set; }

    public long LocalRaidBannerId { get; set; }

    public long RecommendedBattlePower { get; set; }
}