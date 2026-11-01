using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest;

[MessagePackObject(true)]
public class MiningQuestReinforcementResponse : ApiResponseBase, IUserSyncApiResponse
{
    public int CurrentAttackPower { get; set; }

    public int CurrentAttackFrequency { get; set; }

    public int CurrentCollectionLevel { get; set; }

    public int AfterLevel { get; set; }

    public bool IsDisplayMissionBadge { get; set; }

    public UserSyncData UserSyncData { get; set; }
}
