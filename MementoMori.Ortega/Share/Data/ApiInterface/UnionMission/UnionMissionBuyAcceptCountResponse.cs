using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission;

[MessagePackObject(true)]
public class UnionMissionBuyAcceptCountResponse : ApiResponseBase, IUserSyncApiResponse
{
    public int RemainingAcceptCount { get; set; }

    public int BuyAcceptCountToday { get; set; }

    public UserSyncData UserSyncData { get; set; }
}
