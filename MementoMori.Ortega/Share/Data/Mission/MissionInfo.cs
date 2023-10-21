using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Mission
{
    [MessagePackObject(true)]
    public class MissionInfo
    {
        public long MissionExpirationTimeStamp { get; set; }

        public UserMissionActivityDtoInfo UserMissionActivityDtoInfo { get; set; }

        public Dictionary<MissionType, List<UserMissionDtoInfo>> UserMissionDtoInfoDict { get; set; }

        public List<UserPanelMissionDtoInfo> UserPanelMissionDtoInfoList { get; set; }

        public List<long> GetNotReceivedIdList()
        {
            return UserMissionDtoInfoDict.Values.SelectMany(d =>
                d.SelectMany(x =>
                    x.MissionStatusHistory[MissionStatusType.NotReceived]
                )
            ).ToList();
        }

        public bool IsReceiveActivity()
        {
            if (UserMissionActivityDtoInfo != null)
            {
                var values = this.UserMissionActivityDtoInfo.RewardStatusDict.Values;
                return values.Any(d => d == MissionActivityRewardStatusType.NotReceived);
            }

            return false;
        }

        public bool IsProgress(MissionType missionType)
        {
            if (!UserMissionDtoInfoDict.TryGetValue(missionType, out var missionDtoInfos))
            {
                return false;
            }

            return missionDtoInfos.Any(d => d.MissionType == missionType && d.MissionStatusHistory[MissionStatusType.Progress].Any());
        }
    }
}