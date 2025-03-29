using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.MyPage;
using MementoMori.Ortega.Share.Data.WorldGuidance;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.User;

[MessagePackObject(true)]
public class GetMypageResponse : ApiResponseBase, IGuildSyncApiResponse, IUserSyncApiResponse
{
    public List<long> DisplayNoticeIdList { get; set; }

    public bool ExistNewFriendPointTransfer { get; set; }

    public bool ExistNewPrivateChat { get; set; }

    public bool ExistNotReceivedBountyQuestReward { get; set; }

    public bool ExistNotReceivedMissionReward { get; set; }

    public long LatestAnnounceChatRegistrationLocalTimestamp { get; set; }

    public MissionGuideInfo MissionGuideInfo { get; set; }

    public DisplayMypageInfo MypageInfo { get; set; }

    public List<long> NotOrderedBountyQuestIdList { get; set; }

    public long ReceivableLimitedLoginBonusId { get; set; }

    public List<long> UnreadIndividualNotificationIdList { get; set; }

    public List<UserFriendDtoInfo> UserFriendDtoInfoList { get; set; }

    public GuildSyncData GuildSyncData { get; set; }

    public UserSyncData UserSyncData { get; set; }
    
    public WorldGuidanceInfo WorldGuidanceInfo { get; set; }
}