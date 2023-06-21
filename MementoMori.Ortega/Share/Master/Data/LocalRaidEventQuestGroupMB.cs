using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("幻影の神殿イベントクエストグループ")]
	[MessagePackObject(true)]
	public class LocalRaidEventQuestGroupMB : MasterBookBase
	{
		[PropertyOrder(4)]
		[Description("イベント追加挑戦回数")]
		public int EventBattleCount
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("LocalRaidQuestMBのIdリスト")]
		public IReadOnlyList<long> LocalRaidQuestIds
		{
			get;
		}

		[Description("修練レベル")]
		[PropertyOrder(3)]
		public int LocalRaidLevel
		{
			get;
		}

		[Description("イベント経過日数")]
		[PropertyOrder(2)]
		public long OverDayFromStartEventTime
		{
			get;
		}

		[Description("ワールド生成経過日数")]
		[PropertyOrder(5)]
		public long OverDayFromCreateWorldTime
		{
			get;
		}

		[SerializationConstructor]
		public LocalRaidEventQuestGroupMB(long id, bool? isIgnore, string memo, IReadOnlyList<long> localRaidQuestIds, long overDayFromStartEventTime, int localRaidLevel, int eventBattleCount, long overDayFromCreateWorldTime)
			:base(id, isIgnore, memo)
		{
			LocalRaidQuestIds = localRaidQuestIds;
			OverDayFromStartEventTime = overDayFromStartEventTime;
			LocalRaidLevel = localRaidLevel;
			EventBattleCount = eventBattleCount;
			OverDayFromCreateWorldTime = overDayFromCreateWorldTime;
		}

		public LocalRaidEventQuestGroupMB() :base(0L, false, ""){}
	}
}
