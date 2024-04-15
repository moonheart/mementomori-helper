using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
    [MessagePackObject(true)]
    public class RewardFriendMissionResponse : ApiResponseBase, IUserSyncApiResponse
    {
        public List<UserItem> RewardItemList { get; set; }

        public List<UserFriendMissionDtoInfo> UserFriendMissionDtoInfoList { get; set; }

        public UserSyncData UserSyncData { get; set; }
    }
}