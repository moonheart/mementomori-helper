namespace MementoMori.Friend;

public class BulkTransferFriendPoint
{
    public class Req
    {
    }

    public class Resp
    {
        public UserSyncData UserSyncData { get; set; }
    }

    public class UserSyncData
    {
        public object BlockPlayerIdList { get; set; }
        public object ClearedTutorialIdList { get; set; }
        public object CreateUserIdTimestamp { get; set; }
        public DataLinkageMap DataLinkageMap { get; set; }
        public object[] DeletedCharacterGuidList { get; set; }
        public object[] DeletedEquipmentGuidList { get; set; }
        public object ExistVipDailyGift { get; set; }
        public object[][] GivenItemCountInfoList { get; set; }
        public object GuildJoinLimitCount { get; set; }
        public object IsDataLinkage { get; set; }
        public object IsJoinedGlobalGvg { get; set; }
        public object IsJoinedLocalGvg { get; set; }
        public object IsReceivedSnsShareReward { get; set; }
        public object IsValidContractPrivilege { get; set; }
        public object LegendLeagueClassType { get; set; }
        public object LocalRaidChallengeCount { get; set; }
        public object PresentCount { get; set; }
        public object PrivacySettingsType { get; set; }
        public object ReceivedAutoBattleRewardLastTime { get; set; }
        public ShopCurrencyMissionProgressMap ShopCurrencyMissionProgressMap { get; set; }
        public object[] ShopProductGuerrillaPackList { get; set; }
        public object TimeServerId { get; set; }
        public TreasureChestCeilingCountMap TreasureChestCeilingCountMap { get; set; }
        public object UserBattleBossDtoInfo { get; set; }
        public object UserBattleLegendLeagueDtoInfo { get; set; }
        public object UserBattlePvpDtoInfo { get; set; }
        public object UserBoxSizeDtoInfo { get; set; }
        public object[] UserCharacterBookDtoInfos { get; set; }
        public object[] UserCharacterCollectionDtoInfos { get; set; }
        public object[] UserCharacterDtoInfos { get; set; }
        public object[] UserDeckDtoInfos { get; set; }
        public object[] UserEquipmentDtoInfos { get; set; }
        public UserItemDtoInfo[] UserItemDtoInfo { get; set; }
        public object UserLevelLinkDtoInfo { get; set; }
        public object[] UserLevelLinkMemberDtoInfos { get; set; }
        public object[] UserMissionActivityDtoInfos { get; set; }
        public object[] UserMissionDtoInfos { get; set; }
        public object UserMissionOccurrenceHistoryDtoInfo { get; set; }
        public object[] UserFriendMissionDtoInfoList { get; set; }
        public object UserNotificationDtoInfoInfos { get; set; }
        public object[] UserOpenContentDtoInfos { get; set; }
        public object[] UserSettingsDtoInfoList { get; set; }
        public object[] UserShopAchievementPackDtoInfos { get; set; }
        public object UserShopFirstChargeBonusDtoInfo { get; set; }
        public object[] UserShopFreeGrowthPackDtoInfos { get; set; }
        public object[] UserShopMonthlyBoostDtoInfos { get; set; }
        public object UserShopSubscriptionDtoInfos { get; set; }
        public object UserStatusDtoInfo { get; set; }
        public object[] UserTowerBattleDtoInfos { get; set; }
        public object[] UserVipGiftDtoInfos { get; set; }
    }

    public class DataLinkageMap
    {
    }

    public class ShopCurrencyMissionProgressMap
    {
    }

    public class TreasureChestCeilingCountMap
    {
    }

    public class UserItemDtoInfo
    {
        public int ItemCount { get; set; }
        public int ItemId { get; set; }
        public int ItemType { get; set; }
        public long PlayerId { get; set; }
    }
}