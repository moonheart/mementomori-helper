using MementoMori.Ortega.Custom;
using MementoMori.Ortega.ServerLib.Models.MySql.Dto;
using MementoMori.Ortega.Share.Data.Chat;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Data.Shop;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data;

[MessagePackObject(true)]
public class UserSyncData
{
    public List<long> BlockPlayerIdList { get; set; }

    public bool? CanJoinTodayLegendLeague { get; set; }

    public List<long> ClearedTutorialIdList { get; set; }

    public List<ConfirmedItemQuest> ConfirmedItemQuestList { get; set; }

    public long? CreateUserIdTimestamp { get; set; }

    public long? CreateWorldLocalTimeStamp { get; set; }

    public Dictionary<SnsType, bool> DataLinkageMap { get; set; }

    public List<string> DeletedCharacterGuidList { get; set; }

    public List<string> DeletedEquipmentGuidList { get; set; }

    public bool? ExistUnconfirmedRetrieveItemHistory { get; set; }

    public bool? ExistVipDailyGift { get; set; }
    public List<IUserItem> GivenItemCountInfoList { get; set; }

    public int? GuildJoinLimitCount { get; set; }

    public bool? HasTransitionedPanelPictureBook { get; set; }

    public bool? IsDataLinkage { get; set; }

    public bool? IsJoinedGlobalGvg { get; set; }

    public bool? IsJoinedLocalGvg { get; set; }

    public bool? IsReceivedSnsShareReward { get; set; }

    public bool? IsRetrievedItem { get; set; }

    public bool? IsValidContractPrivilege { get; set; }

    public long? LatestAnnounceChatRegistrationLocalTimestamp { get; set; }

    public long? LatestGuildSurveyCreationLocalTimestamp { get; set; }

    public Dictionary<LockEquipmentDeckType, LeadLockEquipmentDialogInfo> LeadLockEquipmentDialogInfoMap { get; set; }

    public LegendLeagueClassType? LegendLeagueClassType { get; set; }

    public int? LocalRaidChallengeCount { get; set; }

    public Dictionary<LockEquipmentDeckType, List<string>> LockedEquipmentCharacterGuidListMap { get; set; }

    public Dictionary<LockEquipmentDeckType, List<UserEquipmentDtoInfo>> LockedUserEquipmentDtoInfoListMap { get; set; }

    public List<long> FriendBattleFavoritePlayerIdList { get; set; }

    public long? PresentCount { get; set; }

    public PrivacySettingsType? PrivacySettingsType { get; set; }

    public Dictionary<RankingDataType, long> ReceivableAchieveRankingRewardIdMap { get; set; }

    public List<long> ReceivedAchieveRankingRewardIdList { get; set; }

    public long? ReceivedAutoBattleRewardLastTime { get; set; }

    public List<long> ReceivedGuildTowerFloorRewardIdList { get; set; } = new();

    public Dictionary<LockEquipmentDeckType, long> ReleaseLockEquipmentCooldownTimeStampMap { get; set; }

    public int? TodayChallengeFriendBattleCount { get; set; }

    public Dictionary<string, long> ShopCurrencyMissionProgressMap { get; set; }

    public List<ShopProductGuerrillaPack> ShopProductGuerrillaPackList { get; set; }

    public long StripePoint { get; set; }

    public long? TimeServerId { get; set; }

    public Dictionary<long, long> TreasureChestCeilingCountMap { get; set; }

    public UserBattleBossDtoInfo UserBattleBossDtoInfo { get; set; }

    public UserBattleLegendLeagueDtoInfo UserBattleLegendLeagueDtoInfo { get; set; }

    public UserBattlePvpDtoInfo UserBattlePvpDtoInfo { get; set; }

    public UserBoxSizeDtoInfo UserBoxSizeDtoInfo { get; set; }

    public List<UserCharacterBookDtoInfo> UserCharacterBookDtoInfos { get; set; }

    public List<UserCharacterCollectionDtoInfo> UserCharacterCollectionDtoInfos { get; set; }

    public List<UserCharacterDtoInfo> UserCharacterDtoInfos { get; set; }

    public List<UserDeckDtoInfo> UserDeckDtoInfos { get; set; }

    public List<UserEquipmentDtoInfo> UserEquipmentDtoInfos { get; set; }
    
    public UserEquipmentStatusDtoInfo UserEquipmentStatusDtoInfo { get; set; }

    public List<UserItemDtoInfo> UserItemDtoInfo { get; set; }

    public UserLevelLinkDtoInfo UserLevelLinkDtoInfo { get; set; }

    public List<UserLevelLinkMemberDtoInfo> UserLevelLinkMemberDtoInfos { get; set; }

    public List<UserMissionActivityDtoInfo> UserMissionActivityDtoInfos { get; set; }

    public List<UserMissionDtoInfo> UserMissionDtoInfos { get; set; }

    public UserMissionOccurrenceHistoryDtoInfo UserMissionOccurrenceHistoryDtoInfo { get; set; }

    public List<UserFriendMissionDtoInfo> UserFriendMissionDtoInfoList { get; set; }

    public Dictionary<GuidanceType, long> UserGuidanceTimeMap { get; set; }
    public List<UserNotificationDtoInfo> UserNotificationDtoInfoInfos { get; set; }

    public List<UserOpenContentDtoInfo> UserOpenContentDtoInfos { get; set; }

    public UserFriendBattleOptionDtoInfo UserFriendBattleOptionDtoInfo { get; set; }

    public UserRecruitGuildMemberSettingDtoInfo UserRecruitGuildMemberSettingDtoInfo { get; set; }

    public List<UserSettingsDtoInfo> UserSettingsDtoInfoList { get; set; }

    public List<UserShopAchievementPackDtoInfo> UserShopAchievementPackDtoInfos { get; set; }

    public UserShopFirstChargeBonusDtoInfo UserShopFirstChargeBonusDtoInfo { get; set; }

    public List<UserShopFreeGrowthPackDtoInfo> UserShopFreeGrowthPackDtoInfos { get; set; }

    public List<UserShopMonthlyBoostDtoInfo> UserShopMonthlyBoostDtoInfos { get; set; }

    public List<UserShopSubscriptionDtoInfo> UserShopSubscriptionDtoInfos { get; set; }

    public UserStatusDtoInfo UserStatusDtoInfo { get; set; }

    public UserSyncGvgDeckDtoInfo UserSyncGvgDeckDtoInfo { get; set; }

    public List<UserTowerBattleDtoInfo> UserTowerBattleDtoInfos { get; set; }

    public List<UserVipGiftDtoInfo> UserVipGiftDtoInfos { get; set; }

    public bool? ExistPurchasableOneWeekLimitedPack { get; set; }

    public ChatSettingData ChatSettingData { get; set; }

    public void UserItemEditorMergeUserSyncData(UserSyncData userSyncData)
    {
        if (userSyncData == null) return;
        if (userSyncData.BlockPlayerIdList.IsNotNullOrEmpty()) BlockPlayerIdList = BlockPlayerIdList.Merge(userSyncData.BlockPlayerIdList);
        if (userSyncData.CanJoinTodayLegendLeague != null) CanJoinTodayLegendLeague = userSyncData.CanJoinTodayLegendLeague;
        if (userSyncData.ClearedTutorialIdList.IsNotNullOrEmpty()) ClearedTutorialIdList = ClearedTutorialIdList.Merge(userSyncData.ClearedTutorialIdList);
        if (userSyncData.CreateUserIdTimestamp != null) CreateUserIdTimestamp = userSyncData.CreateUserIdTimestamp;
        if (userSyncData.CreateWorldLocalTimeStamp != null) CreateWorldLocalTimeStamp = userSyncData.CreateWorldLocalTimeStamp;
        if (userSyncData.DataLinkageMap.IsNotNullOrEmpty()) DataLinkageMap = DataLinkageMap.Merge(userSyncData.DataLinkageMap);
        if (userSyncData.DeletedCharacterGuidList.IsNotNullOrEmpty()) UserCharacterDtoInfos.RemoveAll(d => userSyncData.DeletedCharacterGuidList.Contains(d.Guid));
        if (userSyncData.DeletedEquipmentGuidList.IsNotNullOrEmpty()) UserEquipmentDtoInfos.RemoveAll(d => userSyncData.DeletedEquipmentGuidList.Contains(d.Guid));
        if (userSyncData.UserEquipmentStatusDtoInfo != null) UserEquipmentStatusDtoInfo = userSyncData.UserEquipmentStatusDtoInfo;
        if (userSyncData.ExistVipDailyGift != null) ExistVipDailyGift = userSyncData.ExistVipDailyGift;
        if (userSyncData.GivenItemCountInfoList.IsNotNullOrEmpty())
        {
            foreach (var userItem in userSyncData.GivenItemCountInfoList)
            {
                var userItemDtoInfo = UserItemDtoInfo.FirstOrDefault(d => d.ItemId == userItem.ItemId && d.ItemType == userItem.ItemType);
                if (userItemDtoInfo != null)
                    userItemDtoInfo.ItemCount += userItem.ItemCount;
                else
                {
                    UserItemDtoInfo.Add(new UserItemDtoInfo
                    {
                        ItemId = userItem.ItemId,
                        ItemType = userItem.ItemType,
                        ItemCount = userItem.ItemCount,
                        PlayerId = UserStatusDtoInfo.PlayerId
                    });
                }
            }
        }

        if (userSyncData.GuildJoinLimitCount != null) GuildJoinLimitCount = userSyncData.GuildJoinLimitCount;
        if (userSyncData.IsDataLinkage != null) IsDataLinkage = userSyncData.IsDataLinkage;
        if (userSyncData.IsJoinedGlobalGvg != null) IsJoinedGlobalGvg = userSyncData.IsJoinedGlobalGvg;
        if (userSyncData.IsJoinedLocalGvg != null) IsJoinedLocalGvg = userSyncData.IsJoinedLocalGvg;
        if (userSyncData.IsReceivedSnsShareReward != null) IsReceivedSnsShareReward = userSyncData.IsReceivedSnsShareReward;
        if (userSyncData.IsValidContractPrivilege != null) IsValidContractPrivilege = userSyncData.IsValidContractPrivilege;
        if (userSyncData.LeadLockEquipmentDialogInfoMap != null) LeadLockEquipmentDialogInfoMap = LeadLockEquipmentDialogInfoMap.Merge(userSyncData.LeadLockEquipmentDialogInfoMap);
        if (userSyncData.LegendLeagueClassType != null) LegendLeagueClassType = userSyncData.LegendLeagueClassType;
        if (userSyncData.LocalRaidChallengeCount != null) LocalRaidChallengeCount = userSyncData.LocalRaidChallengeCount;
        if (userSyncData.LockedEquipmentCharacterGuidListMap.IsNotNullOrEmpty())
            LockedEquipmentCharacterGuidListMap = LockedEquipmentCharacterGuidListMap.Merge(userSyncData.LockedEquipmentCharacterGuidListMap);
        if (userSyncData.LockedUserEquipmentDtoInfoListMap.IsNotNullOrEmpty())
            LockedUserEquipmentDtoInfoListMap = LockedUserEquipmentDtoInfoListMap.Merge(userSyncData.LockedUserEquipmentDtoInfoListMap);
        if (userSyncData.PresentCount != null) PresentCount = userSyncData.PresentCount;
        if (userSyncData.PrivacySettingsType != null) PrivacySettingsType = userSyncData.PrivacySettingsType;
        if (userSyncData.ReceivableAchieveRankingRewardIdMap.IsNotNullOrEmpty())
            ReceivableAchieveRankingRewardIdMap = userSyncData.ReceivableAchieveRankingRewardIdMap;
        if (userSyncData.ReceivedAchieveRankingRewardIdList.IsNotNullOrEmpty())
            ReceivedAchieveRankingRewardIdList = ReceivedAchieveRankingRewardIdList.Merge(userSyncData.ReceivedAchieveRankingRewardIdList);
        if (userSyncData.ReceivedAutoBattleRewardLastTime != null) ReceivedAutoBattleRewardLastTime = userSyncData.ReceivedAutoBattleRewardLastTime;
        if (userSyncData.ReleaseLockEquipmentCooldownTimeStampMap.IsNotNullOrEmpty())
            ReleaseLockEquipmentCooldownTimeStampMap = ReleaseLockEquipmentCooldownTimeStampMap.Merge(userSyncData.ReleaseLockEquipmentCooldownTimeStampMap);
        if (userSyncData.ShopCurrencyMissionProgressMap.IsNotNullOrEmpty()) ShopCurrencyMissionProgressMap = ShopCurrencyMissionProgressMap.Merge(userSyncData.ShopCurrencyMissionProgressMap);
        if (userSyncData.ShopProductGuerrillaPackList.IsNotNullOrEmpty())
            ShopProductGuerrillaPackList = ShopProductGuerrillaPackList.Merge(userSyncData.ShopProductGuerrillaPackList, (x, y) => x.ShopGuerrillaPackId == y.ShopGuerrillaPackId);
        if (userSyncData.TimeServerId != null) TimeServerId = userSyncData.TimeServerId;
        if (userSyncData.TreasureChestCeilingCountMap.IsNotNullOrEmpty()) TreasureChestCeilingCountMap = TreasureChestCeilingCountMap.Merge(userSyncData.TreasureChestCeilingCountMap);
        if (userSyncData.UserBattleBossDtoInfo != null) UserBattleBossDtoInfo = userSyncData.UserBattleBossDtoInfo;
        if (userSyncData.UserBattleLegendLeagueDtoInfo != null) UserBattleLegendLeagueDtoInfo = userSyncData.UserBattleLegendLeagueDtoInfo;
        if (userSyncData.UserBattlePvpDtoInfo != null) UserBattlePvpDtoInfo = userSyncData.UserBattlePvpDtoInfo;
        if (userSyncData.UserBoxSizeDtoInfo != null) UserBoxSizeDtoInfo = userSyncData.UserBoxSizeDtoInfo;
        if (userSyncData.UserCharacterBookDtoInfos.IsNotNullOrEmpty()) UserCharacterBookDtoInfos = userSyncData.UserCharacterBookDtoInfos;
        if (userSyncData.UserCharacterCollectionDtoInfos.IsNotNullOrEmpty()) UserCharacterCollectionDtoInfos = userSyncData.UserCharacterCollectionDtoInfos;
        if (userSyncData.UserCharacterDtoInfos.IsNotNullOrEmpty()) UserCharacterDtoInfos = UserCharacterDtoInfos.Merge(userSyncData.UserCharacterDtoInfos, (a, b) => a.Guid == b.Guid);
        if (userSyncData.UserDeckDtoInfos.IsNotNullOrEmpty()) UserDeckDtoInfos = userSyncData.UserDeckDtoInfos;
        if (userSyncData.UserEquipmentDtoInfos.IsNotNullOrEmpty()) UserEquipmentDtoInfos = UserEquipmentDtoInfos.Merge(userSyncData.UserEquipmentDtoInfos, (a, b) => a.Guid == b.Guid);
        if (userSyncData.UserItemDtoInfo.IsNotNullOrEmpty()) UserItemDtoInfo = UserItemDtoInfo.Merge(userSyncData.UserItemDtoInfo, (a, b) => a.ItemType == b.ItemType && a.ItemId == b.ItemId);
        if (userSyncData.UserLevelLinkDtoInfo != null) UserLevelLinkDtoInfo = userSyncData.UserLevelLinkDtoInfo;
        if (userSyncData.UserLevelLinkMemberDtoInfos.IsNotNullOrEmpty()) UserLevelLinkMemberDtoInfos = userSyncData.UserLevelLinkMemberDtoInfos;
        if (userSyncData.UserMissionActivityDtoInfos.IsNotNullOrEmpty()) UserMissionActivityDtoInfos = userSyncData.UserMissionActivityDtoInfos;
        if (userSyncData.UserMissionDtoInfos.IsNotNullOrEmpty()) UserMissionDtoInfos = userSyncData.UserMissionDtoInfos;
        if (userSyncData.UserMissionOccurrenceHistoryDtoInfo != null) UserMissionOccurrenceHistoryDtoInfo = userSyncData.UserMissionOccurrenceHistoryDtoInfo;
        if (userSyncData.UserFriendMissionDtoInfoList.IsNotNullOrEmpty()) UserFriendMissionDtoInfoList = userSyncData.UserFriendMissionDtoInfoList;
        if (userSyncData.UserGuidanceTimeMap.IsNotNullOrEmpty()) UserGuidanceTimeMap = UserGuidanceTimeMap.Merge(userSyncData.UserGuidanceTimeMap);
        if (userSyncData.UserNotificationDtoInfoInfos.IsNotNullOrEmpty()) UserNotificationDtoInfoInfos = userSyncData.UserNotificationDtoInfoInfos;
        if (userSyncData.UserOpenContentDtoInfos.IsNotNullOrEmpty()) UserOpenContentDtoInfos = userSyncData.UserOpenContentDtoInfos;
        if (userSyncData.UserSettingsDtoInfoList.IsNotNullOrEmpty()) UserSettingsDtoInfoList = userSyncData.UserSettingsDtoInfoList;
        if (userSyncData.UserShopAchievementPackDtoInfos.IsNotNullOrEmpty()) UserShopAchievementPackDtoInfos = userSyncData.UserShopAchievementPackDtoInfos;
        if (userSyncData.UserShopFirstChargeBonusDtoInfo != null) UserShopFirstChargeBonusDtoInfo = userSyncData.UserShopFirstChargeBonusDtoInfo;
        if (userSyncData.UserShopFreeGrowthPackDtoInfos.IsNotNullOrEmpty()) UserShopFreeGrowthPackDtoInfos = userSyncData.UserShopFreeGrowthPackDtoInfos;
        if (userSyncData.UserShopMonthlyBoostDtoInfos.IsNotNullOrEmpty()) UserShopMonthlyBoostDtoInfos = userSyncData.UserShopMonthlyBoostDtoInfos;
        if (userSyncData.UserShopSubscriptionDtoInfos.IsNotNullOrEmpty()) UserShopSubscriptionDtoInfos = userSyncData.UserShopSubscriptionDtoInfos;
        if (userSyncData.UserStatusDtoInfo != null) UserStatusDtoInfo = userSyncData.UserStatusDtoInfo;
        if (userSyncData.UserTowerBattleDtoInfos.IsNotNullOrEmpty())
            UserTowerBattleDtoInfos = UserTowerBattleDtoInfos.Merge(userSyncData.UserTowerBattleDtoInfos, (a, b) => a.TowerType == b.TowerType);
        if (userSyncData.UserVipGiftDtoInfos.IsNotNullOrEmpty()) UserVipGiftDtoInfos = userSyncData.UserVipGiftDtoInfos;
        if (userSyncData.ReceivedGuildTowerFloorRewardIdList.IsNotNullOrEmpty())
            ReceivedGuildTowerFloorRewardIdList = ReceivedGuildTowerFloorRewardIdList.Merge(userSyncData.ReceivedGuildTowerFloorRewardIdList);
        if (userSyncData.ExistPurchasableOneWeekLimitedPack != null) ExistPurchasableOneWeekLimitedPack = userSyncData.ExistPurchasableOneWeekLimitedPack;
    }
}