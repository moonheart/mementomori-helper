using MessagePack;

namespace MementoMori.Common;

public class UserSyncData
{
    public object[] BlockPlayerIdList { get; set; }
    public int[] ClearedTutorialIdList { get; set; }
    public object CreateUserIdTimestamp { get; set; }
    public DataLinkageMap DataLinkageMap { get; set; }
    public object[] DeletedCharacterGuidList { get; set; }
    public object[] DeletedEquipmentGuidList { get; set; }
    public bool? ExistVipDailyGift { get; set; }
    public object[] GivenItemCountInfoList { get; set; }
    public int? GuildJoinLimitCount { get; set; }
    public object IsDataLinkage { get; set; }
    public object IsJoinedGlobalGvg { get; set; }
    public object IsJoinedLocalGvg { get; set; }
    public object IsReceivedSnsShareReward { get; set; }
    public object IsValidContractPrivilege { get; set; }
    public object LegendLeagueClassType { get; set; }
    public int? LocalRaidChallengeCount { get; set; }
    public int? PresentCount { get; set; }
    public object PrivacySettingsType { get; set; }
    public object ReceivedAutoBattleRewardLastTime { get; set; }
    public ShopCurrencyMissionProgressMap ShopCurrencyMissionProgressMap { get; set; }
    public object[] ShopProductGuerrillaPackList { get; set; }
    public object TimeServerId { get; set; }
    public TreasureChestCeilingCountMap TreasureChestCeilingCountMap { get; set; }
    public UserBattleBossDtoInfo UserBattleBossDtoInfo { get; set; }
    public object UserBattleLegendLeagueDtoInfo { get; set; }
    public object UserBattlePvpDtoInfo { get; set; }
    public UserBoxSizeDtoInfo UserBoxSizeDtoInfo { get; set; }
    public UserCharacterBookDtoInfos[] UserCharacterBookDtoInfos { get; set; }
    public object[] UserCharacterCollectionDtoInfos { get; set; }
    public UserCharacterDtoInfos[] UserCharacterDtoInfos { get; set; }
    public UserDeckDtoInfos[] UserDeckDtoInfos { get; set; }
    public UserEquipmentDtoInfos[] UserEquipmentDtoInfos { get; set; }
    public UserItemDtoInfo[] UserItemDtoInfo { get; set; }
    public UserLevelLinkDtoInfo UserLevelLinkDtoInfo { get; set; }
    public UserLevelLinkMemberDtoInfos[] UserLevelLinkMemberDtoInfos { get; set; }
    public object[] UserMissionActivityDtoInfos { get; set; }
    public object[] UserMissionDtoInfos { get; set; }
    public UserMissionOccurrenceHistoryDtoInfo UserMissionOccurrenceHistoryDtoInfo { get; set; }
    public object[] UserFriendMissionDtoInfoList { get; set; }
    public UserNotificationDtoInfoInfos[] UserNotificationDtoInfoInfos { get; set; }
    public UserOpenContentDtoInfos[] UserOpenContentDtoInfos { get; set; }
    public UserSettingsDtoInfoList[] UserSettingsDtoInfoList { get; set; }
    public object[] UserShopAchievementPackDtoInfos { get; set; }
    public object UserShopFirstChargeBonusDtoInfo { get; set; }
    public object[] UserShopFreeGrowthPackDtoInfos { get; set; }
    public object[] UserShopMonthlyBoostDtoInfos { get; set; }
    public object[] UserShopSubscriptionDtoInfos { get; set; }
    public UserStatusDtoInfo UserStatusDtoInfo { get; set; }
    public UserTowerBattleDtoInfos[] UserTowerBattleDtoInfos { get; set; }
    public UserVipGiftDtoInfos[] UserVipGiftDtoInfos { get; set; }
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

public class UserBattleBossDtoInfo
{
    public long? BossLastChallengeTime { get; set; }
    public int? BossClearMaxQuestId { get; set; }
    public int? BossTodayUseCurrencyCount { get; set; }
    public int? BossTodayUseTicketCount { get; set; }
    public int? BossTodayWinCount { get; set; }
    public bool? IsOpenedNewQuest { get; set; }
}

public class UserBoxSizeDtoInfo
{
    public int? CharacterBoxSizeId { get; set; }
    public long? PlayerId { get; set; }
}

public class UserCharacterBookDtoInfos
{
    public int? CharacterId { get; set; }
    public int? MaxCharacterLevel { get; set; }
    public int? MaxCharacterRarityFlags { get; set; }
    public int? MaxEpisodeId { get; set; }
}

public class UserCharacterDtoInfos
{
    public string Guid { get; set; }
    public long? PlayerId { get; set; }
    public int? CharacterId { get; set; }
    public int? Level { get; set; }
    public int? Exp { get; set; }
    public int? RarityFlags { get; set; }
    public bool? IsLocked { get; set; }
}

public class UserDeckDtoInfos
{
    public int? DeckNo { get; set; }
    public int? DeckUseContentType { get; set; }
    public int? DeckBattlePower { get; set; }
    public string UserCharacterGuid1 { get; set; }
    public int? CharacterId1 { get; set; }
    public string UserCharacterGuid2 { get; set; }
    public int? CharacterId2 { get; set; }
    public string UserCharacterGuid3 { get; set; }
    public int? CharacterId3 { get; set; }
    public string UserCharacterGuid4 { get; set; }
    public int? CharacterId4 { get; set; }
    public string UserCharacterGuid5 { get; set; }
    public int? CharacterId5 { get; set; }
}

public class UserEquipmentDtoInfos
{
    public string CharacterGuid { get; set; }
    public long? CreateAt { get; set; }
    public long? PlayerId { get; set; }
    public int? AdditionalParameterHealth { get; set; }
    public int? AdditionalParameterIntelligence { get; set; }
    public int? AdditionalParameterMuscle { get; set; }
    public int? AdditionalParameterEnergy { get; set; }
    public int? EquipmentId { get; set; }
    public string Guid { get; set; }
    public int? SphereId1 { get; set; }
    public int? SphereId2 { get; set; }
    public int? SphereId3 { get; set; }
    public int? SphereId4 { get; set; }
    public int? SphereUnlockedCount { get; set; }
    public int? LegendSacredTreasureExp { get; set; }
    public int? LegendSacredTreasureLv { get; set; }
    public int? MatchlessSacredTreasureExp { get; set; }
    public int? MatchlessSacredTreasureLv { get; set; }
    public int? ReinforcementLv { get; set; }
}

public class UserItemDtoInfo
{
    public int? ItemCount { get; set; }
    public int? ItemId { get; set; }
    public int? ItemType { get; set; }
    public long? PlayerId { get; set; }
}

public class UserLevelLinkDtoInfo
{
    public int? PartyMaxLevel { get; set; }
    public int? PartyLevel { get; set; }
    public int? PartySubLevel { get; set; }
    public int? MemberMaxCount { get; set; }
    public int? BuyFrameCount { get; set; }
    public bool? IsPartyMode { get; set; }
}

public class UserLevelLinkMemberDtoInfos
{
    public int? CellNo { get; set; }
    public string UserCharacterGuid { get; set; }
    public int? CharacterId { get; set; }
    public long? UnavailableTime { get; set; }
}

public class UserMissionOccurrenceHistoryDtoInfo
{
    public long? BeginnerStartTime { get; set; }
    public int? ComebackStartTime { get; set; }
    public long? DailyStartTime { get; set; }
    public long? WeeklyStartTime { get; set; }
    public long? LimitedStartTime { get; set; }
    public int? LimitedMissionMBId { get; set; }
    public int? NewCharacterMissionMBId { get; set; }
}

public class UserNotificationDtoInfoInfos
{
    public int? NotificationType { get; set; }
    public int? Value { get; set; }
}

public class UserOpenContentDtoInfos
{
    public int? OpenContentId { get; set; }
}

public class UserSettingsDtoInfoList
{
    public int? PlayerSettingsType { get; set; }
    public int? Value { get; set; }
    public long? PlayerId { get; set; }
}

public class UserStatusDtoInfo
{
    public long? CreateAt { get; set; }
    public bool? IsFirstVisitGuildAtDay { get; set; }
    public bool? IsReachBattleLeagueTop50 { get; set; }
    public bool? IsAlreadyChangedName { get; set; }
    public int? Birthday { get; set; }
    public string Comment { get; set; }
    public long? PlayerId { get; set; }
    public int? MainCharacterIconId { get; set; }
    public int? FavoriteCharacterId1 { get; set; }
    public int? FavoriteCharacterId2 { get; set; }
    public int? FavoriteCharacterId3 { get; set; }
    public int? FavoriteCharacterId4 { get; set; }
    public int? FavoriteCharacterId5 { get; set; }
    public string Name { get; set; }
    public int? Rank { get; set; }
    public int? BoardRank { get; set; }
    public int? Exp { get; set; }
    public int? Vip { get; set; }
    public long? LastLoginTime { get; set; }
    public long? PreviousLoginTime { get; set; }
    public long? LastLvUpTime { get; set; }
    public int? VipExp { get; set; }
}

public class UserTowerBattleDtoInfos
{
    public int? BoughtCount { get; set; }
    public long? LastUpdateTime { get; set; }
    public int? MaxTowerBattleId { get; set; }
    public long? PlayerId { get; set; }
    public int? TodayBattleCount { get; set; }
    public int? TodayBoughtCountByCurrency { get; set; }
    public int? TodayClearNewFloorCount { get; set; }
    public int? TowerType { get; set; }
}

public class UserVipGiftDtoInfos
{
    public long? PlayerId { get; set; }
    public int? VipGiftId { get; set; }
    public int? VipLv { get; set; }
}