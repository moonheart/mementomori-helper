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
        [PropertyOrder(3)]
        [Description("放置バトル経験値ボーナス(%)")]
        public int AutoBattlePlayerExpBonus { get; }

        [Nest(false, 0)]
        [PropertyOrder(31)]
        [Description("VIPデイリー報酬リスト")]
        public IReadOnlyList<UserItem> DailyRewardItemList { get; }

        [PropertyOrder(9)]
        [Description("時空の洞窟コインドロップ増加\u200b(%)")]
        public int DungeonBattleCoinBonus { get; }

        [PropertyOrder(10)]
        [Description("時空の洞窟ゴールドボーナス(%)")]
        public int DungeonBattleGoldBonus { get; }

        [PropertyOrder(13)]
        [Description("時空の洞窟：見逃し可能回数")]
        public long DungeonBattleMissedCompensationCount { get; }

        [PropertyOrder(19)]
        [Description("運命ガチャが可能か否か")]
        public bool IsDestinyGachaAvailable { get; }

        [PropertyOrder(20)]
        [Description("運命ガチャのログが見れるか否か")]
        public bool IsDestinyGachaLogAvailable { get; }

        [PropertyOrder(21)]
        [Description("星の導きガチャが可能か否か")]
        public bool IsStarsGuidanceGachaAvailable { get; }

        [PropertyOrder(22)]
        [Description("星の導きガチャのログが見れるか否か")]
        public bool IsStarsGuidanceGachaLogAvailable { get; }

        [PropertyOrder(23)]
        [Description("星導交換が可能か否か")]
        public bool IsStarsGuidanceTradeShopAvailable { get; }

        [PropertyOrder(26)]
        [Description("研磨時にロックが可能か否か\u200b")]
        public bool IsLockEquipmentTrainingAvailable { get; }

        [PropertyOrder(15)]
        [Description("祈りの泉一括派遣、受け取り\u200bが可能か否か")]
        public bool IsMultipleBountyQuestAvailable { get; }

        [PropertyOrder(18)]
        [Description("ギルドレイドの一括掃討が可能か否か")]
        public bool IsMultipleQuickStartGuildRaidAvailable { get; }

        [PropertyOrder(24)]
        [Description("ボス/無窮の塔掃討が可能か否か")]
        public bool IsQuickBossBattleAvailable { get; }

        [PropertyOrder(17)]
        [Description("ギルドレイドの掃討が可能か否か")]
        public bool IsQuickStartGuildRaidAvailable { get; }

        [PropertyOrder(25)]
        [Description("神装強化の装備返還が可能か否か\u200b")]
        public bool IsRefundEquipmentMergeAvailable { get; }

        [PropertyOrder(14)]
        [Description("ログインボーナス見逃し補填回数")]
        public long LoginBonusMissedCompensationCount { get; }

        [PropertyOrder(1)]
        [Description("VIPレベル")]
        public long Lv { get; }

        [PropertyOrder(11)]
        [Description("ボス/無窮の塔挑戦購入可能回数増加\u200b")]
        public long MaxBossBattleUseCurrencyCount { get; }

        [PropertyOrder(8)]
        [Description("英雄枠数増加\u200b")]
        public int MaxCharacterBoxPlus { get; }

        [PropertyOrder(28)]
        [Description("１か月の最大武具リセット回数")]
        public int MaxEquipmentResetCount { get; }

        [PropertyOrder(27)]
        [Description("1日の最大模擬戦回数")]
        public int MaxFriendBattleDailyCount { get; }

        [PropertyOrder(16)]
        [Description("ギルドレイド挑戦可能回数")]
        public long MaxGuildRaidChallengeCount { get; }

        [PropertyOrder(5)]
        [Description("仮想通貨で高速周回できる回数")]
        public long MaxQuickUseCurrencyCount { get; }

        [PropertyOrder(12)]
        [Description("ショップの アイテム数増加\u200b")]
        public int MaxShopItemCountPlus { get; }

        [PropertyOrder(6)]
        [Description("ソロクエストを挑戦できる回数")]
        public int MaxSoloQuestCount { get; }

        [PropertyOrder(7)]
        [Description("チームクエストを挑戦できる回数")]
        public int MaxTeamQuestCount { get; }

        [PropertyOrder(5)]
        [Description("高速戦闘経験値ボーナス(%)")]
        public int QuickBattlePlayerExpBonus { get; }

        [Nest(false, 0)]
        [PropertyOrder(29)]
        [Description("VIP到達時報酬リスト")]
        public IReadOnlyList<UserItem> ReachRewardItemList { get; }

        [PropertyOrder(2)]
        [Description("必要累計 VIP経験値")]
        public long RequiredExp { get; }

        [Nest(true, 0)]
        [PropertyOrder(30)]
        [Description("VIPギフトリスト")]
        public IReadOnlyList<VipGiftInfo> VipGiftInfoList { get; }

        [SerializationConstructor]
        public VipMB(long id, bool? isIgnore, string memo, int autoBattlePlayerExpBonus, IReadOnlyList<UserItem> dailyRewardItemList, int dungeonBattleCoinBonus, int dungeonBattleGoldBonus,
            long dungeonBattleMissedCompensationCount, bool isDestinyGachaAvailable, bool isDestinyGachaLogAvailable, bool isStarsGuidanceGachaAvailable, bool isStarsGuidanceGachaLogAvailable,
            bool isStarsGuidanceTradeShopAvailable, bool isLockEquipmentTrainingAvailable, bool isMultipleBountyQuestAvailable, bool isQuickBossBattleAvailable, bool isQuickStartGuildRaidAvailable,
            bool isMultipleQuickStartGuildRaidAvailable, bool isRefundEquipmentMergeAvailable, long loginBonusMissedCompensationCount, long lv, long maxBossBattleUseCurrencyCount,
            int maxCharacterBoxPlus, int maxEquipmentResetCount, int maxFriendBattleDailyCount, long maxGuildRaidChallengeCount, long maxQuickUseCurrencyCount, int maxShopItemCountPlus, int maxSoloQuestCount,
            int maxTeamQuestCount, int quickBattlePlayerExpBonus, IReadOnlyList<UserItem> reachRewardItemList, long requiredExp, IReadOnlyList<VipGiftInfo> vipGiftInfoList)
            : base(id, isIgnore, memo)
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
            MaxEquipmentResetCount = maxEquipmentResetCount;
            MaxFriendBattleDailyCount = maxFriendBattleDailyCount;
            MaxGuildRaidChallengeCount = maxGuildRaidChallengeCount;
            MaxQuickUseCurrencyCount = maxQuickUseCurrencyCount;
            MaxShopItemCountPlus = maxShopItemCountPlus;
            MaxSoloQuestCount = maxSoloQuestCount;
            MaxTeamQuestCount = maxTeamQuestCount;
            QuickBattlePlayerExpBonus = quickBattlePlayerExpBonus;
            ReachRewardItemList = reachRewardItemList;
            RequiredExp = requiredExp;
            VipGiftInfoList = vipGiftInfoList;
            IsStarsGuidanceGachaAvailable = isStarsGuidanceGachaAvailable;
            IsStarsGuidanceGachaLogAvailable = isStarsGuidanceGachaLogAvailable;
            IsStarsGuidanceTradeShopAvailable = isStarsGuidanceTradeShopAvailable;
        }

        public VipMB() : base(0, false, "")
        {
        }
    }
}