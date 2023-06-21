using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Data.Shop;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("VIP特典")]
	public class VipMB : MasterBookBase
	{
		[PropertyOrder(3)]
		[Description("放置バトル経験値ボーナス(%)")]
		public int AutoBattlePlayerExpBonus
		{
			get;
		}

		[Description("VIPデイリー報酬リスト")]
		[PropertyOrder(25)]
		[Nest(false, 0)]
		public IReadOnlyList<UserItem> DailyRewardItemList
		{
			get;
		}

		[Description("時空の洞窟コインドロップ増加\u200b(%)")]
		[PropertyOrder(9)]
		public int DungeonBattleCoinBonus
		{
			get;
		}

		[Description("時空の洞窟ゴールドボーナス(%)")]
		[PropertyOrder(10)]
		public int DungeonBattleGoldBonus
		{
			get;
		}

		[Description("時空の洞窟：見逃し可能回数")]
		[PropertyOrder(13)]
		public long DungeonBattleMissedCompensationCount
		{
			get;
		}

		[Description("運命ガチャが可能か否か")]
		[PropertyOrder(18)]
		public bool IsDestinyGachaAvailable
		{
			get;
		}

		[PropertyOrder(19)]
		[Description("運命ガチャのログが見れるか否か")]
		public bool IsDestinyGachaLogAvailable
		{
			get;
		}

		[Description("研磨時にロックが可能か否か\u200b")]
		[PropertyOrder(22)]
		public bool IsLockEquipmentTrainingAvailable
		{
			get;
		}

		[PropertyOrder(15)]
		[Description("祈りの泉一括派遣、受け取り\u200bが可能か否か")]
		public bool IsMultipleBountyQuestAvailable
		{
			get;
		}

		[PropertyOrder(20)]
		[Description("ボス/無窮の塔掃討が可能か否か")]
		public bool IsQuickBossBattleAvailable
		{
			get;
		}

		[Description("ギルドレイドの掃討が可能か否か")]
		[PropertyOrder(17)]
		public bool IsQuickStartGuildRaidAvailable
		{
			get;
		}

		[PropertyOrder(21)]
		[Description("神装強化の装備返還が可能か否か\u200b")]
		public bool IsRefundEquipmentMergeAvailable
		{
			get;
		}

		[Description("ログインボーナス見逃し補填回数")]
		[PropertyOrder(14)]
		public long LoginBonusMissedCompensationCount
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("VIPレベル")]
		public long Lv
		{
			get;
		}

		[Description("ボス/無窮の塔挑戦購入可能回数増加\u200b")]
		[PropertyOrder(11)]
		public long MaxBossBattleUseCurrencyCount
		{
			get;
		}

		[PropertyOrder(8)]
		[Description("英雄枠数増加\u200b")]
		public int MaxCharacterBoxPlus
		{
			get;
		}

		[Description("ギルドレイド挑戦可能回数")]
		[PropertyOrder(16)]
		public long MaxGuildRaidChallengeCount
		{
			get;
		}

		[Description("仮想通貨で高速周回できる回数")]
		[PropertyOrder(5)]
		public long MaxQuickUseCurrencyCount
		{
			get;
		}

		[PropertyOrder(12)]
		[Description("ショップの アイテム数増加\u200b")]
		public int MaxShopItemCountPlus
		{
			get;
		}

		[Description("ソロクエストを挑戦できる回数")]
		[PropertyOrder(6)]
		public int MaxSoloQuestCount
		{
			get;
		}

		[Description("チームクエストを挑戦できる回数")]
		[PropertyOrder(7)]
		public int MaxTeamQuestCount
		{
			get;
		}

		[Description("高速戦闘経験値ボーナス(%)")]
		[PropertyOrder(5)]
		public int QuickBattlePlayerExpBonus
		{
			get;
		}

		[Description("VIP到達時報酬リスト")]
		[Nest(false, 0)]
		[PropertyOrder(23)]
		public IReadOnlyList<UserItem> ReachRewardItemList
		{
			get;
		}

		[Description("必要累計 VIP経験値")]
		[PropertyOrder(2)]
		public long RequiredExp
		{
			get;
		}

		[Description("VIPギフトリスト")]
		[PropertyOrder(24)]
		[Nest(true, 0)]
		public IReadOnlyList<VipGiftInfo> VipGiftInfoList
		{
			get;
		}

		[SerializationConstructor]
		public VipMB(long id, bool? isIgnore, string memo, int autoBattlePlayerExpBonus, IReadOnlyList<UserItem> dailyRewardItemList, int dungeonBattleCoinBonus, int dungeonBattleGoldBonus, long dungeonBattleMissedCompensationCount, bool isDestinyGachaAvailable, bool isDestinyGachaLogAvailable, bool isLockEquipmentTrainingAvailable, bool isMultipleBountyQuestAvailable, bool isQuickBossBattleAvailable, bool isQuickStartGuildRaidAvailable, bool isRefundEquipmentMergeAvailable, long loginBonusMissedCompensationCount, long lv, long maxBossBattleUseCurrencyCount, int maxCharacterBoxPlus, long maxGuildRaidChallengeCount, long maxQuickUseCurrencyCount, int maxShopItemCountPlus, int maxSoloQuestCount, int maxTeamQuestCount, int quickBattlePlayerExpBonus, IReadOnlyList<UserItem> reachRewardItemList, long requiredExp, IReadOnlyList<VipGiftInfo> vipGiftInfoList)
			:base(id, isIgnore, memo)
		{
			this.AutoBattlePlayerExpBonus = autoBattlePlayerExpBonus;
			this.DailyRewardItemList = dailyRewardItemList;
			this.DungeonBattleCoinBonus = dungeonBattleCoinBonus;
			this.DungeonBattleGoldBonus = dungeonBattleGoldBonus;
			this.DungeonBattleMissedCompensationCount = dungeonBattleMissedCompensationCount;
			this.IsDestinyGachaAvailable = isDestinyGachaAvailable;
			this.IsDestinyGachaLogAvailable = isDestinyGachaLogAvailable;
			this.IsLockEquipmentTrainingAvailable = isLockEquipmentTrainingAvailable;
			this.IsMultipleBountyQuestAvailable = isMultipleBountyQuestAvailable;
			this.IsQuickBossBattleAvailable = isQuickBossBattleAvailable;
			this.IsQuickStartGuildRaidAvailable = isQuickStartGuildRaidAvailable;
			this.IsRefundEquipmentMergeAvailable = isRefundEquipmentMergeAvailable;
			this.LoginBonusMissedCompensationCount = loginBonusMissedCompensationCount;
			this.Lv = lv;
			this.MaxBossBattleUseCurrencyCount = maxBossBattleUseCurrencyCount;
			this.MaxCharacterBoxPlus = maxCharacterBoxPlus;
			this.MaxGuildRaidChallengeCount = maxGuildRaidChallengeCount;
			this.MaxQuickUseCurrencyCount = maxQuickUseCurrencyCount;
			this.MaxShopItemCountPlus = maxShopItemCountPlus;
			this.MaxSoloQuestCount = maxSoloQuestCount;
			this.MaxTeamQuestCount = maxTeamQuestCount;
			this.QuickBattlePlayerExpBonus = quickBattlePlayerExpBonus;
			this.ReachRewardItemList = reachRewardItemList;
			this.RequiredExp = requiredExp;
			this.VipGiftInfoList = vipGiftInfoList;
		}

		public VipMB() :base(0L, false, ""){}
	}
}
