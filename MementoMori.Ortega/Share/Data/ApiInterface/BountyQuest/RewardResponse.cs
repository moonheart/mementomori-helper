using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.BountyQuest;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest
{
    [MessagePackObject(true)]
    public class RewardResponse : ApiResponseBase, IUserSyncApiResponse
    {
        public List<BountyQuestInfo> BountyQuestInfos { get; set; }

        public List<UserItem> RewardItems { get; set; }

        public long UserBoardRank { get; set; }

        public List<UserBountyQuestDtoInfo> UserBountyQuestDtoInfos { get; set; }

        public UserSyncData UserSyncData { get; set; }

        public RewardResponse()
        {
        }
    }
}