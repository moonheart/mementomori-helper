using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Data.LocalRaid;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LocalRaid;

[MessagePackObject(true)]
public class GetLocalRaidInfoResponse : ApiResponseBase, IUserSyncApiResponse, ILocalRaidInfoApiResponse
{
    public List<long> OpenLocalRaidQuestIds { get; set; }

    public List<long> OpenEventLocalRaidQuestIds { get; set; }

    public List<LocalRaidQuestInfo> LocalRaidQuestInfos { get; set; }

    public List<LocalRaidEnemyInfo> LocalRaidEnemyInfos { get; set; }
    public Dictionary<long, int> ClearCountDict { get; set; }

    public UserSyncData UserSyncData { get; set; }
}