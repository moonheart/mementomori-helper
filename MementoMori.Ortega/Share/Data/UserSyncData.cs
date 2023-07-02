using System.Runtime.CompilerServices;
using MementoMori.Ortega.ServerLib.Models.MySql.Dto;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Data.Shop;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data
{
    [MessagePackObject(true)]
    public class UserSyncData
    {
        public List<long> BlockPlayerIdList { get; set; }

        public List<long> ClearedTutorialIdList { get; set; }

        public long? CreateUserIdTimestamp { get; set; }

        public Dictionary<SnsType, bool> DataLinkageMap { get; set; }

        public List<string> DeletedCharacterGuidList { get; set; }

        public List<string> DeletedEquipmentGuidList { get; set; }

        public bool? ExistVipDailyGift { get; set; }
        public List<IUserItem> GivenItemCountInfoList { get; set; }

        public int? GuildJoinLimitCount { get; set; }

        public bool? IsDataLinkage { get; set; }

        public bool? IsJoinedGlobalGvg { get; set; }

        public bool? IsJoinedLocalGvg { get; set; }

        public bool? IsReceivedSnsShareReward { get; set; }

        public bool? IsValidContractPrivilege { get; set; }

        public LegendLeagueClassType? LegendLeagueClassType { get; set; }

        public int? LocalRaidChallengeCount { get; set; }

        public long? PresentCount { get; set; }

        public PrivacySettingsType? PrivacySettingsType { get; set; }

        public long? ReceivedAutoBattleRewardLastTime { get; set; }

        public Dictionary<string, long> ShopCurrencyMissionProgressMap { get; set; }

        public List<ShopProductGuerrillaPack> ShopProductGuerrillaPackList { get; set; }

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

        public List<UserItemDtoInfo> UserItemDtoInfo { get; set; }

        public UserLevelLinkDtoInfo UserLevelLinkDtoInfo { get; set; }

        public List<UserLevelLinkMemberDtoInfo> UserLevelLinkMemberDtoInfos { get; set; }

        public List<UserMissionActivityDtoInfo> UserMissionActivityDtoInfos { get; set; }

        public List<UserMissionDtoInfo> UserMissionDtoInfos { get; set; }

        public UserMissionOccurrenceHistoryDtoInfo UserMissionOccurrenceHistoryDtoInfo { get; set; }

        public List<UserFriendMissionDtoInfo> UserFriendMissionDtoInfoList { get; set; }

        public List<UserNotificationDtoInfo> UserNotificationDtoInfoInfos { get; set; }

        public List<UserOpenContentDtoInfo> UserOpenContentDtoInfos { get; set; }

        public List<UserSettingsDtoInfo> UserSettingsDtoInfoList { get; set; }

        public List<UserShopAchievementPackDtoInfo> UserShopAchievementPackDtoInfos { get; set; }

        public UserShopFirstChargeBonusDtoInfo UserShopFirstChargeBonusDtoInfo { get; set; }

        public List<UserShopFreeGrowthPackDtoInfo> UserShopFreeGrowthPackDtoInfos { get; set; }

        public List<UserShopMonthlyBoostDtoInfo> UserShopMonthlyBoostDtoInfos { get; set; }

        public List<UserShopSubscriptionDtoInfo> UserShopSubscriptionDtoInfos { get; set; }

        public UserStatusDtoInfo UserStatusDtoInfo { get; set; }

        public List<UserTowerBattleDtoInfo> UserTowerBattleDtoInfos { get; set; }

        public List<UserVipGiftDtoInfo> UserVipGiftDtoInfos { get; set; }

        public void UserItemEditorMergeUserSyncData(UserSyncData userSyncData)
        {
            
        }

        public UserSyncData()
        {
        }
    }
}