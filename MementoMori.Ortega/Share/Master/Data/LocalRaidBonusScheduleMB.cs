using System.ComponentModel;
using MementoMori.Ortega.Share.Data.LocalRaid;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("幻影の神殿ボーナススケジュール")]
	public class LocalRaidBonusScheduleMB : MasterBookBase, IHasStartEndTime
	{
		[Nest(false, 0)]
		[PropertyOrder(1)]
		[Description("報酬増加時間リスト")]
		public IReadOnlyList<LocalRaidStartEndTime> LocalRaidStartEndTimes { get; }

		[PropertyOrder(2)]
		[Description("報酬増加倍率(100%=10000)")]
		public int RewardBonusRate { get; }

		[PropertyOrder(3)]
		[Description("開始日時")]
		public string StartTime { get; }

		[PropertyOrder(4)]
		[Description("終了日時")]
		public string EndTime { get; }

		[SerializationConstructor]
		public LocalRaidBonusScheduleMB(long id, bool? isIgnore, string memo, IReadOnlyList<LocalRaidStartEndTime> localRaidStartEndTimes, int rewardBonusRate, string startTime, string endTime)
			:base(id, isIgnore, memo)
		{
			this.LocalRaidStartEndTimes = localRaidStartEndTimes;
			this.RewardBonusRate = rewardBonusRate;
			this.StartTime = startTime;
			this.EndTime = endTime;
		}

		public LocalRaidBonusScheduleMB() :base(0L, null, "")
		{
		}
	}
}
