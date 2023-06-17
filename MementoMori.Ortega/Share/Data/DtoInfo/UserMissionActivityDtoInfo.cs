using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserMissionActivityDtoInfo
    {
        public MissionGroupType MissionGroupType { get; set; }

        public long PlayerId { get; set; }

        public long ProgressCount { get; set; }

        public Dictionary<long, MissionActivityRewardStatusType> RewardStatusDict { get; set; }

        public UserMissionActivityDtoInfo()
        {
        }
    }
}