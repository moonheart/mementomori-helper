using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserMissionDtoInfo
    {
        public MissionAchievementType AchievementType { get; set; }

        public Dictionary<MissionStatusType, List<long>> MissionStatusHistory { get; set; }

        public MissionType MissionType { get; set; }

        public long PlayerId { get; set; }

        public long ProgressCount { get; set; }

        public MissionGroupType GetMissionGroupType()
        {
            return MissionUtil.MissionTypeToMissionGroup(this.MissionType);
        }

        public List<long> GetNotReceivedIdList()
        {
            Dictionary<MissionStatusType, List<long>> dictionary = this.MissionStatusHistory;
            return dictionary.TryGetValue(MissionStatusType.NotReceived, out var list) ? list : new List<long>();
        }

        public bool Any(MissionStatusType statusType)
        {
            Dictionary<MissionStatusType, List<long>> dictionary = this.MissionStatusHistory;
            return dictionary.TryGetValue(statusType, out var list) && list.Any();
        }

        public UserMissionDtoInfo()
        {
        }
    }
}