using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Data.Shop;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [Description("VIP特典")]
    [MessagePackObject(true)]
    public class VipMB : MasterBookBase
    {
        [Description("放置バトル経験値ボーナス(%)")]
        [PropertyOrder(3)]
        public int AutoBattlePlayerExpBonus { get; }

        [Description("VIPデイリー報酬リスト")]
        [PropertyOrder(26)]
        [Nest(false, 0)]
        public IReadOnlyList<UserItem> DailyRewardItemList { get; }

        [PropertyOrder(9)]
        [Description("時空の洞窟コインドロップ増加\u200b(%)")]
        public int DungeonBattleCoinBonus { get; }

        [PropertyOrder(10)]
        [Description("時空の洞窟ゴールドボーナス(%)")]
        public int DungeonBattleGoldBonus { get; }

        [Description("時空の洞窟：見逃し可能回数")]
        [PropertyOrder(13)]
        public long DungeonBattleMissedCompensationCount { get; }

        [PropertyOrder(19)]
        [Description("運命ガチャが可能か否か")]
        public bool IsDestinyGachaAvailable { get; }

        [Description("運命ガチャのログが見れるか否か")]
        [PropertyOrder(20)]
        public bool IsDestinyGachaLogAvailable { get; }

        [Description("研磨時にロックが可能か否か\u200b")]
        [PropertyOrder(23)]
        public bool IsLockEquipmentTrainingAvailable { get; }

        [Description("祈りの泉一括派遣、受け取り\u200bが可能か否か")]
        [PropertyOrder(15)]
        public bool IsMultipleBountyQuestAvailable { get; }

        [PropertyOrder(18)]
        [Description("ギルドレイドの一括掃討が可能か否か")]
        public bool IsMultipleQuickStartGuildRaidAvailable { get; }

        [PropertyOrder(21)]
        [Description("ボス/無窮の塔掃討が可能か否か")]
        public bool IsQuickBossBattleAvailable { get; }

        [PropertyOrder(17)]
        [Description("ギルドレイドの掃討が可能か否か")]
        public bool IsQuickStartGuildRaidAvailable { get; }

        [Description("神装強化の装備返還が可能か否か\u200b")]
        [PropertyOrder(22)]
        public bool IsRefundEquipmentMergeAvailable { get; }

        [Description("ログインボーナス見逃し補填回数")]
        [PropertyOrder(14)]
        public long LoginBonusMissedCompensationCount { get; }

        [Description("VIPレベル")]
        [PropertyOrder(1)]
        public long Lv { get; }

        [PropertyOrder(11)]
        [Description("ボス/無窮の塔挑戦購入可能回数増加\u200b")]
        public long MaxBossBattleUseCurrencyCount { get; }

        [Description("英雄枠数増加\u200b")]
        [PropertyOrder(8)]
        public int MaxCharacterBoxPlus { get; }

        [Description("ギルドレイド挑戦可能回数")]
        [PropertyOrder(16)]
        public long MaxGuildRaidChallengeCount { get; }

        [PropertyOrder(5)]
        [Description("仮想通貨で高速周回できる回数")]
        public long MaxQuickUseCurrencyCount { get; }

        [PropertyOrder(12)]
        [Description("ショップの アイテム数増加\u200b")]
        public int MaxShopItemCountPlus { get; }

        [Description("ソロクエストを挑戦できる回数")]
        [PropertyOrder(6)]
        public int MaxSoloQuestCount { get; }

        [PropertyOrder(7)]
        [Description("チームクエストを挑戦できる回数")]
        public int MaxTeamQuestCount { get; }

        [PropertyOrder(5)]
        [Description("高速戦闘経験値ボーナス(%)")]
        public int QuickBattlePlayerExpBonus { get; }

        [Nest(false, 0)]
        [PropertyOrder(24)]
        [Description("VIP到達時報酬リスト")]
        public IReadOnlyList<UserItem> ReachRewardItemList { get; }

        [PropertyOrder(2)]
        [Description("必要累計 VIP経験値")]
        public long RequiredExp { get; }

        [Nest(true, 0)]
        [Description("VIPギフトリスト")]
        [PropertyOrder(25)]
        public IReadOnlyList<VipGiftInfo> VipGiftInfoList { get; }

        [SerializationConstructor]
        public VipMB(long id, bool? isIgnore, string memo, int autoBattlePlayerExpBonus, IReadOnlyList<UserItem> dailyRewardItemList, int dungeonBattleCoinBonus, int dungeonBattleGoldBonus,
            long dungeonBattleMissedCompensationCount, bool isDestinyGachaAvailable, bool isDestinyGachaLogAvailable, bool isLockEquipmentTrainingAvailable, bool isMultipleBountyQuestAvailable,
            bool isQuickBossBattleAvailable, bool isQuickStartGuildRaidAvailable, bool isMultipleQuickStartGuildRaidAvailable, bool isRefundEquipmentMergeAvailable,
            long loginBonusMissedCompensationCount, long lv, long maxBossBattleUseCurrencyCount, int maxCharacterBoxPlus, long maxGuildRaidChallengeCount, long maxQuickUseCurrencyCount,
            int maxShopItemCountPlus, int maxSoloQuestCount, int maxTeamQuestCount, int quickBattlePlayerExpBonus, IReadOnlyList<UserItem> reachRewardItemList, long requiredExp,
            IReadOnlyList<VipGiftInfo> vipGiftInfoList)
        :base( id, isIgnore, memo)
        {
            AutoBattlePlayerExpBonus = autoBattlePlayerExpBonus;
            DailyRewardItemList = dailyRewardItemList;
            DungeonBattleCoinBonus = dungeonBattleCoinBonus;
            DungeonBattleGoldBonus = dungeonBattleGoldBonus;
            DungeonBattleMissedCompensationCount = dungeonBattleMissedCompensationCount;
            IsDestinyGachaAvailable = isDestinyGachaAvailable;
            IsDestinyGachaLogAvailable = isDestinyGachaLogAvailable;
            IsLockEquipmentTrainingAvailable = isLockEquipmentTrainingAvailable;
            IsMultipleBountyQuestAvailable = isMultipleBountyQuestAvailable;
            IsQuickBossBattleAvailable = isQuickBossBattleAvailable;
            IsQuickStartGuildRaidAvailable = isQuickStartGuildRaidAvailable;
            IsMultipleQuickStartGuildRaidAvailable = isMultipleQuickStartGuildRaidAvailable;
            IsRefundEquipmentMergeAvailable = isRefundEquipmentMergeAvailable;
            LoginBonusMissedCompensationCount = loginBonusMissedCompensationCount;
            Lv = lv;
            MaxBossBattleUseCurrencyCount = maxBossBattleUseCurrencyCount;
            MaxCharacterBoxPlus = maxCharacterBoxPlus;
            MaxGuildRaidChallengeCount = maxGuildRaidChallengeCount;
            MaxQuickUseCurrencyCount = maxQuickUseCurrencyCount;
            MaxShopItemCountPlus = maxShopItemCountPlus;
            MaxSoloQuestCount = maxSoloQuestCount;
            MaxTeamQuestCount = maxTeamQuestCount;
            QuickBattlePlayerExpBonus = quickBattlePlayerExpBonus;
            ReachRewardItemList = reachRewardItemList;
            RequiredExp = requiredExp;
            VipGiftInfoList = vipGiftInfoList;
        }

        public VipMB():base( 0, false, "")
        {
            
        }
    }
}