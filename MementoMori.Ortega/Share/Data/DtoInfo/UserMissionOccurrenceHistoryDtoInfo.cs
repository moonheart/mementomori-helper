using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserMissionOccurrenceHistoryDtoInfo
    {
        public long BeginnerStartTime { get; set; }

        public long ComebackStartTime { get; set; }

        public long DailyStartTime { get; set; }

        public long WeeklyStartTime { get; set; }

        public long LimitedStartTime { get; set; }

        public long LimitedMissionMBId { get; set; }

        public long NewCharacterMissionMBId { get; set; }

        public UserMissionOccurrenceHistoryDtoInfo()
        {
        }
    }
}