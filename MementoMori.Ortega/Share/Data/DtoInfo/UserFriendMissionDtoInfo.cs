using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserFriendMissionDtoInfo
    {
        public long FriendCampaignId { get; set; }

        public MissionAchievementType AchievementType { get; set; }

        public long ProgressCount { get; set; }

        public Dictionary<MissionStatusType, List<long>> MissionStatusHistory { get; set; }

        public bool Any(MissionStatusType statusType)
        {
            return MissionStatusHistory.TryGetValue(statusType, out var list) && list.Any();
        }
    }
}