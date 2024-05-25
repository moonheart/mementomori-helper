using System.ComponentModel;
using MementoMori.Ortega.Share.Data.LocalRaid;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("幻影の神殿イベントスケジュール")]
	[MessagePackObject(true)]
	public class LocalRaidEventScheduleMB : MasterBookBase, IHasStartEndTime
	{
        [Nest(false, 0)]
        [PropertyOrder(1)]
        [Description("報酬増加時間リスト")]
        public IReadOnlyList<LocalRaidStartEndTime> LocalRaidStartEndTimes { get; }

        [PropertyOrder(2)]
        [Description("報酬増加倍率(100%=10000)")]
        public int RewardBonusRate { get; }

        [PropertyOrder(4)]
        [Description("開始日時")]
        public string StartTime { get; }

        [PropertyOrder(5)]
        [Description("終了日時")]
        public string EndTime { get; }

        [PropertyOrder(6)]
        [Description("イベントチュートリアルダイアログのID")]
        public long LocalRaidEventTutorialId { get; }

        [PropertyOrder(3)]
        [Description("イベント追加挑戦回数")]
        public int EventBattleCount { get; }

		[SerializationConstructor]
		public LocalRaidEventScheduleMB(long id, bool? isIgnore, string memo, IReadOnlyList<LocalRaidStartEndTime> localRaidStartEndTimes, int rewardBonusRate, int eventBattleCount, string startTime, string endTime, long localRaidEventTutorialId)
			:base(id, isIgnore, memo)
		{
			LocalRaidStartEndTimes = localRaidStartEndTimes;
			RewardBonusRate = rewardBonusRate;
			StartTime = startTime;
			EndTime = endTime;
			LocalRaidEventTutorialId = localRaidEventTutorialId;
            EventBattleCount = eventBattleCount;
		}

		public LocalRaidEventScheduleMB() :base(0L, false, ""){}
	}
}
