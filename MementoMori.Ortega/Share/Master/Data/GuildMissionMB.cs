using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("ギルドミッション")]
    public class GuildMissionMB : MasterBookBase, IHasStartEndTime
    {
        [PropertyOrder(1)]
        [Description("開始日時")]
        public string StartTime { get; }

        [PropertyOrder(2)]
        [Description("終了日時")]
        public string EndTime { get; }

        [PropertyOrder(3)]
        [Description("ミッションIDリスト")]
        public IReadOnlyList<long> MissionIdList { get; }

        [SerializationConstructor]
        public GuildMissionMB(long id, bool? isIgnore, string memo, string startTime, string endTime, IReadOnlyList<long> missionIdList) : base(id, isIgnore, memo)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.MissionIdList = missionIdList;
        }

        public GuildMissionMB() : base(0L, false, "")
        {
        }
    }
}