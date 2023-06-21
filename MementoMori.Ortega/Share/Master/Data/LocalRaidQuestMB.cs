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
		[Description("初回報酬")]
		[PropertyOrder(4)]
		[Nest(false, 0)]
		public IReadOnlyList<UserItem> FirstBattleRewards
		{
			get;
		}

		[Nest(false, 0)]
		[Description("通常報酬")]
		[PropertyOrder(5)]
		public IReadOnlyList<UserItem> FixedBattleRewards
		{
			get;
		}

		[PropertyOrder(6)]
		[Description("敵Idリスト")]
		public IReadOnlyList<long> LocalRaidEnemyIds
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("レベル")]
		public int Level
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("クエスト名キー")]
		public long LocalRaidBannerId
		{
			get;
		}

		[Description("個人戦力目安")]
		[PropertyOrder(3)]
		public long RecommendedBattlePower
		{
			get;
		}

		[SerializationConstructor]
		public LocalRaidQuestMB(long id, bool? isIgnore, string memo, long localRaidBannerId, IReadOnlyList<UserItem> firstBattleRewards, IReadOnlyList<UserItem> fixedBattleRewards, IReadOnlyList<long> localRaidEnemyIds, int level, long recommendedBattlePower)
			:base(id, isIgnore, memo)
		{
			LocalRaidBannerId = localRaidBannerId;
			FirstBattleRewards = firstBattleRewards;
			FixedBattleRewards = fixedBattleRewards;
			LocalRaidEnemyIds = localRaidEnemyIds;
			Level = level;
			RecommendedBattlePower = recommendedBattlePower;
		}

		public LocalRaidQuestMB() :base(0L, false, ""){}
	}
}
