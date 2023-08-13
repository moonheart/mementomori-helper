using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.BountyQuest;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest
{
    [MessagePackObject(true)]
    public class GetListResponse : ApiResponseBase
    {
        public List<BountyQuestInfo> BountyQuestInfos { get; set; }

        public long ChangeDayTime { get; set; }

        public long UserBoardRank { get; set; }

        public List<UserBountyQuestBoardDtoInfo> UserBountyQuestBoardDtoInfos { get; set; }

        public List<UserBountyQuestDtoInfo> UserBountyQuestDtoInfos { get; set; }

        public List<UserBountyQuestMemberDtoInfo> SelfUserBountyQuestMemberDtoInfos { get; set; }

        public List<UserBountyQuestMemberDtoInfo> FriendAndGuildMemberUserBountyQuestMemberDtoInfos { get; set; }
    }
}