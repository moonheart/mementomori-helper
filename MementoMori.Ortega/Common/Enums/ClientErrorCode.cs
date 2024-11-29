﻿namespace MementoMori.Ortega.Common.Enums;

public enum ClientErrorCode
{
    None,
    ApiHttpError,
    ApiHttpTimeOut,
    ApiHttpInvalidResponseData,
    ApiInvalidTimeStamp,
    ApiNeedToUpdateMasterData,
    ApiNeedToUpdateAsset,
    ApiDeserialize,
    ApiInvalidMasterDataVersion,
    ApiInvalidAssetVersion,
    ApiInvalidAccessToken,
    ApiRequireClientUpdate,
    ApiTryMax,
    ApiHttpMaintenanceError,
    ApiHttpBlockedDueToExcessiveAccess,
    ApiStatusCodeParseError,
    AddressableNetwork = 101,
    AddressableStorage,
    RawDataNetwork,
    MasterDataCatalog = 201,
    MasterDataDownload,
    MasterDataLoadFromLocal,
    MasterDataInvalidMasterVersion,
    SectionMaintenance = 301,
    SectionMaintenanceToTitle,
    BuyChallengeNotEnoughRemainingCount = 100001,
    BuyChallengeNotEnoughCurrency,
    NotEnoughItem,
    CannotClickContinuously,
    FormationEmpty = 100101,
    FormationNotEnoughBattlePower,
    FormationExistSameCharacter,
    FormationChangeOrder,
    FormationEmptyInParty,
    FormationSameParty,
    FormationListMax,
    FormationSameCharacterInParty,
    FormationElement,
    FormationEditEmpty,
    FormationCopyMessage,
    FormationNotAvailableFromCopy,
    FormationExistNotLockEquipmentCharacter,
    InputValidationError = 100201,
    InputTextEmpty,
    InputSameText,
    ChangeMyNameValidationError = 100301,
    ChangeMyNameNameEmpty,
    ChangeMyNameSameName,
    SnsNotInputGameDataId = 100401,
    SnsNotInputPassword,
    SnsNotEqualInputUserIdLength,
    SnsLackInputPasswordLength,
    EvolutionEquipmentEvolutionLimit = 100501,
    EvolutionEquipmentOverCharacterLevel,
    EvolutionEquipmentNotEnoughEvolutionItems,
    EvolutionEquipmentReleaseSeriesEffect,
    EvolutionEquipmentNotMeetRarityUpRequirements,
    BuyCharacterPossessionLimitNotEnoughCurrency = 100601,
    BuyCharacterPossessionLimitLimitBoxSize,
    BuyLackCurrency,
    BattleNotEnoughItem = 100701,
    BattleUnavailableSkip,
    BattleClearPartyUse,
    TutorialOpeningMovieLoad = 100801,
    VipLackLevel = 101001,
    ContentNotOpen = 102001,
    RankingTabLocked = 103001,
    AchieveRankingNotExistReward = 103101,
    PlayerInformationCannotAddFriendMessageBlocking = 104001,
    PlayerInformationCannotShowCharacterDetailDueToDifferentWorld,
    PlayerInformationCannotAddFriendDueToDifferentWorld,
    CharacterResetNotExist = 200001,
    CharacterResetMinLevel,
    CharacterResetLocked,
    CharacterRankUpNotSelectBase,
    CharacterRankUpSelectMaterial,
    CharacterRankUpCharacterLock,
    CharacterRankUpNotMaterial,
    CharacterRankUpDeckLock,
    CharacterRankUpLimit,
    CharacterRankUpBulkNotChecked,
    CharacterRankSelectLimit,
    CharacterEquipmentNotEquipped,
    CharacterEquipmentAllNoEquip,
    CharacterEquipmentAllError,
    CharacterLock,
    CharacterUnlock,
    CharacterShardReversionNotSelected,
    CharacterShardReversionNotExistsCharacter,
    CharacterLevelUpNotEnoughItem,
    CharacterEquipmentDetailTabRestriction,
    CharacterEquipmentInheritanceNotExist,
    CharacterReleaseDialogDeckLock,
    CharacterReleaseDialogCharacterLock,
    CharacterDetailNotPossessed,
    CharacterNoMoreSphereCanBeExtended,
    CharacterReleaseNotExistsReleasableCharacter,
    CharacterReleaseNotSelected,
    CharacterRankReleased,
    CharacterRankReleaseNotCount,
    FriendBlockTargetIsFriend = 300001,
    FriendBlockTargetIsApplying,
    FriendPointMax,
    FriendNotExistsCanReceiveAndSendPoint,
    FriendPointReceiveLimitPerDay,
    FriendNotExistsWaitApproval,
    FriendNotExistsFriendRequest,
    FriendPlayerIdCopied,
    FriendNotInputPlayerId,
    FriendSearchYourId,
    FriendCountMax,
    FriendApplicationHasBeenWithdrawn,
    FriendCampaignEmptyCode = 310001,
    FriendCampaignInvalidCharacterNum,
    FriendCampaignOutOfTerm,
    FriendCampaignCodeAlreadyUsed,
    ItemBoxExistsStrongerEquipment = 400001,
    ItemBoxExistsOpenedSphereSlotEquipment,
    ItemBoxExistsStrengthenedEquipment,
    ItemBoxCannotGetEquipmentSetMaterial,
    ItemBoxSelectCharacter,
    ItemBoxSelectCountTreasureChestShortage,
    ItemBoxSelectCountSynthesisShortage,
    ItemBoxSelectCountShortage,
    ItemBoxSphereBatchSynthesisLackSphere,
    ItemBoxSphereBatchSynthesisLackItem,
    ItemBoxSphereBatchSynthesisLackCurrency,
    ItemBoxNotSelectedItem,
    ItemBoxNotSelectedElement,
    GachaNotEnoughItem = 500001,
    GachaNotEnoughCharacterBox,
    GachaPurchaseConfirmation,
    GachaTimeOut,
    GachaNotEnoughGold,
    GachaNotCaseData,
    GachaChangeDate,
    GachaCharacterReleaseReward,
    GachaSelectListNotDestinyCharacter,
    GachaSelectListNotStarsGuidanceCharacter,
    GuildNotJoin = 600001,
    GuildNotEnoughCurrency,
    GuildNameTextEmpty,
    GuildDescriptionTextEmpty,
    GuildAnnouncementTextEmpty,
    GuildJoinDateChange,
    GuildApplyDateChange,
    GuildInsufficientBattlePower,
    GuildGuildUserApplyCountMax,
    GuildJoinNotInput,
    GuildApproveMaxMemberError,
    GuildBattleCloseMessage,
    GuildBattleOutOfSeason,
    GuildMasterUnsubscribe,
    GuildBattleOutOfTime,
    GuildAlreadyFullSubLeader,
    GuildAlreadyOtherPosition,
    GuildAlreadyFullCommander,
    GuildRaidNotEnoughGuildFrame = 600101,
    GuildRaidNoAuthorities,
    GuildRaidNotEnoughBattleCount,
    GuildRaidGuildJoinTimeLimit,
    GuildRaidBattleTimeOut,
    GuildRaidWorldRewardAlreadyReceived,
    GuildRaidWorldDamageNotReached,
    GvgNotSelectedCharacter = 600201,
    GvgNotEnoughSelectedCharacterCount,
    GvgNotInBattleTime,
    GvgCastleStateChanged,
    GvgPartyCharacterSelectedMax,
    GvgPartyListEntryEmpty,
    GvgPartyListEntryCannotDeploy,
    GvgPartyListEntryLimitMinUnit,
    GvgYesterdayMvpNotExist,
    GvgYesterdayMvpIsMaintenance,
    GvgYesterdayMvpUpdateData,
    GuildMemberRecruitEmpty = 600301,
    GuildMemberRecruitMaxMember,
    GuildMemberRecruitMember,
    GuildMemberRecruitGuildNotOpen,
    GuildMemberRecruitRejectAll,
    GvgOutOfTimeLockEquipment = 600401,
    GvgCastleWaitDeclaring = 600501,
    LocalRaidNotEnoughRemainingCount = 700001,
    LocalRaidNotInputPassword,
    LocalRaidCannotChangeFormation,
    LocalRaidNotEnoughBattlePower,
    LocalRaidCannotJoinSameRoom,
    LocalRaidDismissedRoom,
    LocalRaidAlreadySentFriendInvitation,
    LocalRaidSendFriendInvitation,
    LocalRaidInvitationError,
    LocalRaidNotMaxPassword,
    DungeonBattleNotEnoughItem = 800001,
    DungeonBattleAllCharacterHPFull,
    DungeonBattleExceedUsedItemCount,
    DungeonBattleSoldOut,
    DungeonBattleNotEnoughUserItem,
    DungeonBattleSelectImpossibleReinforcement,
    DungeonBattleRecovered,
    DungeonBattleGridRevival,
    DungeonBattleGridRevivalRecovery,
    DungeonBattleMapUnexplored,
    ShopPurchaseLimit = 900001,
    ShopPurchaseRestriction,
    ShopReceivedTodayRewardOfMonthlyBoost,
    ShopReceivedLastDayRewardOfMonthlyBoost,
    ShopNotPurchasedOfAchievementPack,
    ShopReceivedRewardOfAchievementPack,
    ShopUnAchievedOfAchievementPack,
    ShopReceivedRewardOfChargeBonus,
    ShopUnAchievedOfChargeBonus,
    ShopNotPurchasedFirstDay,
    ShopNotPurchasedSecondDay,
    ShopNotPurchasedThirdDay,
    ShopReceivedRewardOfFirstChargeBonus,
    ShopNotReachedDayOfFirstChargeBonus,
    ShopReceivedRewardOfGrowthPack,
    ShopNotReachedRarityOfGrowthPack,
    ShopAchievementPackPurchased,
    ShopMissionOpened,
    ShopGrowthPackLackRarity,
    ShopMonthlyBoostRewardDetailReceived,
    ShopMissionClosed,
    ShopNeedUpdate,
    ShopNotExistReceivableMissionReward,
    ShopMonthlyBoostNotPrePurchasePeriod,
    ShopMonthlyBoostAlreadyPrePurchased,
    ShopNotEnoughGold,
    ShopGrowthPackClosed,
    ExchangeNotEnoughUserItem = 1000001,
    ExchangeSoldOut,
    ExchangeNotSelectedConsumeItem,
    ExchangeNotExistSelectableItem,
    ExchangeUnowned,
    ExchangeExpired,
    AutoBattleQuickNotEnoughCurrency = 1100001,
    AutoBattleQuickNotEnoughRemainingCount,
    AutoBattleNotExistMapWorld = 1100101,
    AutoBattleNotOpenMapWorld,
    ChatSelectPlayerMessageNotInGuild = 1200001,
    ChatNotInput,
    ChatNotSelectPlayerMessage,
    ChatNotJoinedWorldGroup,
    ChatMaintenance,
    ChatBanned,
    ChatNotJoinedGuildInGroup,
    ChatIsInCoolTimeAnnounce,
    ChatNotEditableAnnounce,
    EquipmentAscendMaxItem = 1300001,
    EquipmentNoEquipEquipment,
    EquipmentAscendMaxLevelItem = 1300004,
    EquipmentAscendNoSelectItem,
    EquipmentDetailLackItem,
    EquipmentStrengtheningReachedUpperLimit,
    EquipmentAscendNoItem,
    EquipmentCanNotSelectSameSphere,
    EquipmentCanNotMoreSelectSphere,
    EquipmentAscendMaxExp,
    EquipmentAscendLegendMaxLevel,
    EquipmentAscendMatchlessMaxLevel,
    EquipmentAscendCanNotSelectSameLevel,
    EquipmentAscendSacredLevelSelectCannotIncreaseLevel,
    EquipmentAscendSacredLevelSelectCannotReduceLevel,
    LevelLinkBaseCharacterConditions = 1400001,
    LevelLinkLackItem,
    LevelLinkCannotOpenSlot,
    LevelLinkNotSelectCharacter,
    LevelLinkNotExistSelectableCharacter,
    LevelLinkReachedMaxLevel,
    TowerBattleDayChallengeLimit = 1500001,
    TowerBattleCannotSelectCharacter,
    TowerBattleMaxCleared,
    BountyQuestAutoSelect = 1600001,
    BountyQuestFormation,
    BountyQuestUpdateEmpty,
    BountyQuestNotSelected,
    PvpRemainingChallengesZero = 1700001,
    PvpReloadRivalList,
    PvpLeftChallengeEmpty,
    PvpGlobalIsNotOpen,
    PvpGlobalIsNotParticipate,
    PvpGlobalIsLocked,
    PvpGlobalCanNotChallengeAnyMore,
    PvpGlobalExchangeAlreadyAcquired,
    PvpGlobalExchangeExpired,
    PvpGlobalExchangeNotSelectedAnyItem,
    MyPageBeginnerMissionCloseContent = 1800001,
    MyPageBeginnerMissionDay,
    MyPageMissionRewardEmpty,
    MyPageAccountDelete,
    MyPagePresentBoxReceive,
    MyPagePresentBoxDelete,
    MyPagePresentBoxAllDelete,
    MyPagePresentDetailDelete,
    MyPageFavoriteCharacterUnselected,
    MyPageFavoriteCharacterMaxSelect,
    MyPageIdCopied,
    MyPageLoginBonusReceive,
    MyPageBirthDaySettingValidation,
    MyPageMenuAllDownloadDone,
    SphereEquipmentNotChangeEquippedSpheres = 1900001,
    SphereSynthesisNotSelectBaseSphere,
    SphereSynthesisNotEnoughGold,
    SphereSynthesisNotEnoughCurrency,
    SphereSynthesisNotEnoughSphere,
    SphereSynthesisMaxLevel,
    SphereOpenSlotNotEnoughGold,
    SphereOpenSlotNotEnoughCurrency,
    AgeVerificationInvalidBirthDate = 2000000,
    AgeVerificationConfirmCheckBoxOff,
    TitleWarningCheckBoxOff = 2100000,
    TitleApiError,
    MissionRewardReceived = 2200000,
    MissionSoldOut,
    MissionNexDay,
    MissionDontHaveOpenPanelGridItem,
    MissionLockedPanelSheet,
    PurchaseSystemNotAvailable = 2300001,
    PurchaseProductDataNotFound,
    PurchaseProductNotAvailable,
    PurchaseRestoreTransactionFailed,
    PurchaseIAPInitializeFailed,
    PurchaseIAPInitializeFailedServiceDisconnect,
    PurchaseProcessFailed,
    PurchaseCompleteFailed,
    PurchaseIAPInitializeTimeout,
    MusicPlayerNotSelectedMusicItem = 2400001,
    MusicPlayerNotSelectedPlayableMusicItem,
    MusicPlayerNotSelectedAddableMusicItemInPlaylist,
    MusicPlayerNotInputPlaylistName,
    MusicPlayerNotInitialized,
    MusicPlayerLiveMode,
    MusicPlayerNotEnoughItem,
    MusicPlayerMaxPlaylist,
    MusicPlayerMaxPlaylistMusicCount,
    MusicPlayerNotModified,
    MusicPlayerNotExistSelectableMusics,
    MusicPlayerSelectedAllMusics,
    GuildTowerNotSelectDifficulty = 2500001,
    GuildTowerNotExistChallengeCount,
    GuildTowerExistCharacterWithoutLockEquipment,
    GuildTowerNotOpenEvent,
    GuildTowerNoMoreEntry,
    GuildTowerNotSelectCharacter,
    GuildTowerAlreadyUsePrevEntry,
    GuildTowerReinforceJobRestrictionWithLevelCap,
    GuildTowerReinforceJobRestrictionWithMaxLevel,
    GuildTowerReinforceJobNotAddMaterialCount,
    GuildTowerFloorRewardAlreadyReceived,
    GuildTowerEndEvent,
    GuildTowerFloorRewardJoinGuildAfterEventEnd,
    GuildTowerNotChallengeableGuildJoinDay,
    GuildTowerNotExistGuildChallengeCount,
    GuildTowerReinforceJobResetNotSelectMaterial,
    GuildTowerMissionJoinGuildAfterEventEnd,
    StarsGuidanceTradeNotSelectConsumeItem = 2600001,
    StarsGuidanceTradeNotEnoughConsumeItem,
    StarsGuidanceTradeOverTodayExchangeCount,
    StarsGuidanceTradeAlreadyReachedMaxExchangeCount,
    PopularityVoteNotAvailableVoteReadyTerm = 2700001,
    PopularityVoteNotAvailableVoteAggregationTerm,
    PopularityVoteNotAvailableVoteNotEnoughItem,
    PopularityVoteNotSelectPercentage,
    PopularityVoteNotExistMyListCharacter,
    AppsFlyerOnConversionDataSuccess = 2800001,
    AppsFlyerDeeplink,
    AppsFlyerCast,
    EventPortalEventNoticeClosed = 2900001,
    LuckyChanceInputFormNameKanjiEmpty = 3000001,
    LuckyChanceInputFormNameFuriganaEmpty,
    LuckyChanceInputFormNameFuriganaTextError,
    LuckyChanceInputFormPostalCodeEmpty,
    LuckyChanceInputFormPostalCodeNotEnoughDigits,
    LuckyChanceInputFormPhoneNumberEmpty = 3000007,
    LuckyChanceInputFormPhoneNumberNotStartZero,
    LuckyChanceInputFormPhoneNumberNotEnoughDigits,
    LuckyChanceInputFormEmailEmpty,
    LuckyChanceInputFormEmailNotMatch,
    LuckyChanceInputFormAuthCodeEmpty,
    LuckyChanceInputFormAuthCodeNotEnoughDigits,
    LuckyChanceNotAgreePurposeOfUse,
    LuckyChanceNotEnoughItem,
    LuckyChanceAlreadyMaxDrawCount,
    LuckyChanceNotSelectAllExchangeItem,
    LuckyChanceInputFormPrefectureEmpty,
    LuckyChanceInputFormPrefectureTextError,
    LuckyChanceInputFormMunicipalityEmpty,
    LuckyChanceInputFormBlockNumberEmpty,
    BulkSphereSetMaxCustomCount = 3100001,
    BulkSphereSetMaxCustomCountPerCharacter,
    BulkSphereSetAlreadyTop,
    BulkSphereSetNotExistCustomSet,
    BulkSphereSetNotEditCustomName,
    BulkSphereSetNotInputCustomName,
    BulkSphereSetNotSelectCustomSet,
    BulkSphereSetNotExistCustomSetForSphereList,
    BulkSphereSetNotExistReleasableSphere,
    LinkAccountGooglePlaySignInFailed = 3200001
}