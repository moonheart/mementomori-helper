using MementoMori.Ortega.Share.Data.LocalRaid;

namespace MementoMori.Ortega.Share.Data.Interface;

public interface ILocalRaidInfoApiResponse
{
    List<LocalRaidQuestInfo> LocalRaidQuestInfos { get; set; }

    List<LocalRaidEnemyInfo> LocalRaidEnemyInfos { get; set; }
}