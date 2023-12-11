using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
    [NotUseOnBatch]
	[Description("無窮の塔\u3000階層情報")]
	public class TowerBattleQuestMB : MasterBookBase
	{
		[PropertyOrder(3)]
		[Description("クリアパーティの基準（ベース）戦闘力")]
		public long BaseClearPartyDeckPower
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(6)]
		[Description("確定獲得クリア報酬")]
		public IReadOnlyList<UserItem> BattleRewardsConfirmed
		{
			get;
		}

		[Nest(false, 0)]
		[Description("初回獲得クリア報酬")]
		[PropertyOrder(5)]
		public IReadOnlyList<UserItem> BattleRewardsFirst
		{
			get;
		}

		[Description("敵IDリスト")]
		[PropertyOrder(4)]
		public IReadOnlyList<long> EnemyIds
		{
			get;
		}

		[Description("階層")]
		[PropertyOrder(2)]
		public long Floor
		{
			get;
		}

		[Description("抽選報酬情報")]
		[PropertyOrder(7)]
		public long LotteryRewardInfoId
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("塔の種類")]
		public TowerType TowerType
		{
			get;
		}

		[SerializationConstructor]
		public TowerBattleQuestMB(long id, bool? isIgnore, string memo, IReadOnlyList<UserItem> battleRewardsConfirmed, IReadOnlyList<UserItem> battleRewardsFirst, IReadOnlyList<long> enemyIds, long floor, TowerType towerType, long baseClearPartyDeckPower, long lotteryRewardInfoId)
			:base(id, isIgnore, memo)
		{
			BattleRewardsConfirmed = battleRewardsConfirmed;
			BattleRewardsFirst = battleRewardsFirst;
			EnemyIds = enemyIds;
			Floor = floor;
			TowerType = towerType;
			BaseClearPartyDeckPower = baseClearPartyDeckPower;
			LotteryRewardInfoId = lotteryRewardInfoId;
		}

		public TowerBattleQuestMB() :base(0L, false, ""){}
	}
}
