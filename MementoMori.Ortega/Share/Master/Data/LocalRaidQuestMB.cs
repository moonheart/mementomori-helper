using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("幻影の神殿クエスト")]
    public class LocalRaidQuestMB : MasterBookBase
    {
        [Nest(false, 0)]
        [PropertyOrder(7)]
        [Description("初回報酬")]
        public IReadOnlyList<UserItem> FirstBattleRewards { get; }

        [Nest(false, 0)]
        [PropertyOrder(8)]
        [Description("通常報酬")]
        public IReadOnlyList<UserItem> FixedBattleRewards { get; }

        [PropertyOrder(9)]
        [Description("敵Idリスト")]
        public IReadOnlyList<long> LocalRaidEnemyIds { get; }

        [PropertyOrder(2)]
        [Description("LocalRaidEventScheduleMBのId")]
        public long LocalRaidEventScheduleId { get; set; }

        [PropertyOrder(3)]
        [Description("修練レベル(イベント時のクエスト参照用)")]
        public int LocalRaidLevel { get; }

        [PropertyOrder(5)]
        [Description("レベル")]
        public int Level { get; }

        [PropertyOrder(1)]
        [Description("クエスト名キー")]
        public long LocalRaidBannerId { get; }

        [PropertyOrder(4)]
        [Description("イベント経過日数")]
        public long OverDayFromStartEventTime { get; }

        [PropertyOrder(6)]
        [Description("個人戦力目安")]
        public long RecommendedBattlePower { get; }

        [SerializationConstructor]
        public LocalRaidQuestMB(long id, bool? isIgnore, string memo, long localRaidBannerId, long localRaidEventScheduleId, int localRaidLevel, long overDayFromStartEventTime, IReadOnlyList<UserItem> firstBattleRewards, IReadOnlyList<UserItem> fixedBattleRewards, IReadOnlyList<long> localRaidEnemyIds, int level, long recommendedBattlePower)
            : base(id, isIgnore, memo)
        {
            LocalRaidBannerId = localRaidBannerId;
            FirstBattleRewards = firstBattleRewards;
            FixedBattleRewards = fixedBattleRewards;
            LocalRaidEnemyIds = localRaidEnemyIds;
            Level = level;
            RecommendedBattlePower = recommendedBattlePower;
            LocalRaidEventScheduleId = localRaidEventScheduleId;
            LocalRaidLevel = localRaidLevel;
            OverDayFromStartEventTime = overDayFromStartEventTime;
        }

        public LocalRaidQuestMB() : base(0L, false, "")
        {
        }
    }
}