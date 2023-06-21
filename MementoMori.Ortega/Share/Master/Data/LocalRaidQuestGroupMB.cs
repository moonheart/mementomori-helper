using System.ComponentModel;
using MementoMori.Ortega.Share.Data.LocalRaid;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("幻影の神殿クエストグループ")]
	public class LocalRaidQuestGroupMB : MasterBookBase
	{
		[Description("最大挑戦回数")]
		[PropertyOrder(3)]
		public int MaxBattleCount
		{
			get;
		}

		[Description("幻影の神殿Idグループリスト")]
		[PropertyOrder(1)]
		[Nest(true, 0)]
		public IReadOnlyList<LocalRaidQuestIdGroup> LocalRaidQuestIdGroups
		{
			get;
		}

		[Description("修練レベル")]
		[PropertyOrder(2)]
		public int LocalRaidLevel
		{
			get;
		}

		[Description("ワールド生成経過日数")]
		[PropertyOrder(4)]
		public long OverDayFromCreateWorldTime
		{
			get;
		}

		[SerializationConstructor]
		public LocalRaidQuestGroupMB(long id, bool? isIgnore, string memo, IReadOnlyList<LocalRaidQuestIdGroup> localRaidQuestIdGroups, int localRaidLevel, int maxBattleCount, long overDayFromCreateWorldTime)
			:base(id, isIgnore, memo)
		{
			LocalRaidQuestIdGroups = localRaidQuestIdGroups;
			LocalRaidLevel = localRaidLevel;
			MaxBattleCount = maxBattleCount;
			OverDayFromCreateWorldTime = overDayFromCreateWorldTime;
		}

		public LocalRaidQuestGroupMB() :base(0L, false, ""){}
	}
}
