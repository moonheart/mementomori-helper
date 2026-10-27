import type { IUserItem } from '@/api/generated/iUserItem';
import type { ShopProductGuerrillaPack } from '@/api/generated/shopProductGuerrillaPack';
import type { UserEquipmentDtoInfo } from '@/api/generated/userEquipmentDtoInfo';
import type { UserItemDtoInfo } from '@/api/generated/userItemDtoInfo';
import type { UserLevelLinkMemberDtoInfo } from '@/api/generated/userLevelLinkMemberDtoInfo';
import type { UserSyncData } from '@/api/generated/userSyncData';
import type { UserTowerBattleDtoInfo } from '@/api/generated/userTowerBattleDtoInfo';

const isNonEmptyArray = <T>(value: T[] | null | undefined): value is T[] =>
    Array.isArray(value) && value.length > 0;

const isNonEmptyObject = <T extends object>(value: T | null | undefined): value is T =>
    !!value && !Array.isArray(value) && Object.keys(value).length > 0;

const mergePrimitiveArray = <T extends string | number>(
    base: T[] | null | undefined,
    incoming: T[]
): T[] => {
    const merged = new Set<T>(base ?? []);
    for (const item of incoming) {
        merged.add(item);
    }
    return Array.from(merged);
};

const mergeArrayByKey = <T>(
    base: T[] | null | undefined,
    incoming: T[],
    keyFn: (item: T) => string | number
): T[] => {
    const result = Array.isArray(base) ? [...base] : [];
    const indexMap = new Map<string | number, number>();

    result.forEach((item, index) => {
        indexMap.set(keyFn(item), index);
    });

    for (const item of incoming) {
        const key = keyFn(item);
        const existingIndex = indexMap.get(key);
        if (existingIndex !== undefined) {
            result[existingIndex] = item;
        } else {
            indexMap.set(key, result.length);
            result.push(item);
        }
    }

    return result;
};

const mergeRecord = <T extends object>(base: T | null | undefined, incoming: T): T => ({
    ...(base ?? {}),
    ...incoming,
});

const mergeUserItemList = (
    base: UserItemDtoInfo[] | null | undefined,
    incoming: UserItemDtoInfo[]
): UserItemDtoInfo[] =>
    mergeArrayByKey(base, incoming, (item) => `${item.itemType}_${item.itemId}`);

const mergeUserEquipmentList = (
    base: UserEquipmentDtoInfo[] | null | undefined,
    incoming: UserEquipmentDtoInfo[]
): UserEquipmentDtoInfo[] =>
    mergeArrayByKey(base, incoming, (item) => item.guid);

const mergeUserTowerBattleList = (
    base: UserTowerBattleDtoInfo[] | null | undefined,
    incoming: UserTowerBattleDtoInfo[]
): UserTowerBattleDtoInfo[] =>
    mergeArrayByKey(base, incoming, (item) => item.towerType);

const mergeLevelLinkMemberList = (
    base: UserLevelLinkMemberDtoInfo[] | null | undefined,
    incoming: UserLevelLinkMemberDtoInfo[]
): UserLevelLinkMemberDtoInfo[] =>
    mergeArrayByKey(base, incoming, (item) => item.cellNo);

const mergeShopProductGuerrillaPackList = (
    base: ShopProductGuerrillaPack[] | null | undefined,
    incoming: ShopProductGuerrillaPack[]
): ShopProductGuerrillaPack[] =>
    mergeArrayByKey(base, incoming, (item) => item.shopGuerrillaPackId);

const applyGivenItemCountInfoList = (
    base: UserItemDtoInfo[] | null | undefined,
    incoming: IUserItem[],
    playerId: number
): UserItemDtoInfo[] => {
    const result = Array.isArray(base) ? [...base] : [];

    for (const userItem of incoming) {
        const targetIndex = result.findIndex(
            (item) => item.itemId === userItem.itemId && item.itemType === userItem.itemType
        );
        if (targetIndex >= 0) {
            const current = result[targetIndex];
            result[targetIndex] = {
                ...current,
                itemCount: (current.itemCount ?? 0) + userItem.itemCount,
            };
        } else {
            result.push({
                itemId: userItem.itemId,
                itemType: userItem.itemType,
                itemCount: userItem.itemCount,
                playerId,
            } as UserItemDtoInfo);
        }
    }

    return result;
};

export const mergeUserSyncData = (
    base: UserSyncData | null | undefined,
    incoming: UserSyncData | null | undefined
): UserSyncData | null => {
    if (!incoming) {
        return base ?? null;
    }

    const result = { ...(base ?? {}) } as UserSyncData;
    const source = incoming as Partial<UserSyncData>;

    if (isNonEmptyArray(source.blockPlayerIdList)) {
        result.blockPlayerIdList = mergePrimitiveArray(result.blockPlayerIdList, source.blockPlayerIdList);
    }
    if (source.canJoinTodayLegendLeague != null) {
        result.canJoinTodayLegendLeague = source.canJoinTodayLegendLeague;
    }
    if (isNonEmptyArray(source.clearedTutorialIdList)) {
        result.clearedTutorialIdList = mergePrimitiveArray(result.clearedTutorialIdList, source.clearedTutorialIdList);
    }
    if (isNonEmptyArray(source.confirmedItemQuestList)) {
        result.confirmedItemQuestList = mergeArrayByKey(
            result.confirmedItemQuestList,
            source.confirmedItemQuestList,
            (item) => `${item.itemType}_${item.itemId}_${item.questId}`
        );
    }
    if (source.createUserIdTimestamp != null) {
        result.createUserIdTimestamp = source.createUserIdTimestamp;
    }
    if (source.createWorldLocalTimeStamp != null) {
        result.createWorldLocalTimeStamp = source.createWorldLocalTimeStamp;
    }
    if (isNonEmptyObject(source.dataLinkageMap)) {
        result.dataLinkageMap = mergeRecord(result.dataLinkageMap, source.dataLinkageMap);
    }
    if (isNonEmptyArray(source.deletedCharacterGuidList) && isNonEmptyArray(result.userCharacterDtoInfos)) {
        result.userCharacterDtoInfos = result.userCharacterDtoInfos.filter(
            (item) => !source.deletedCharacterGuidList?.includes(item.guid)
        );
    }
    if (isNonEmptyArray(source.deletedEquipmentGuidList) && isNonEmptyArray(result.userEquipmentDtoInfos)) {
        result.userEquipmentDtoInfos = result.userEquipmentDtoInfos.filter(
            (item) => !source.deletedEquipmentGuidList?.includes(item.guid)
        );
    }
    if (source.existUnconfirmedRetrieveItemHistory != null) {
        result.existUnconfirmedRetrieveItemHistory = source.existUnconfirmedRetrieveItemHistory;
    }
    if (source.existVipDailyGift != null) {
        result.existVipDailyGift = source.existVipDailyGift;
    }
    if (isNonEmptyArray(source.givenItemCountInfoList)) {
        result.userItemDtoInfo = applyGivenItemCountInfoList(
            result.userItemDtoInfo,
            source.givenItemCountInfoList,
            result.userStatusDtoInfo?.playerId ?? 0
        );
    }
    if (source.guildJoinLimitCount != null) {
        result.guildJoinLimitCount = source.guildJoinLimitCount;
    }
    if (source.hasTransitionedPanelPictureBook != null) {
        result.hasTransitionedPanelPictureBook = source.hasTransitionedPanelPictureBook;
    }
    if (source.isDataLinkage != null) {
        result.isDataLinkage = source.isDataLinkage;
    }
    if (source.isJoinedGlobalGvg != null) {
        result.isJoinedGlobalGvg = source.isJoinedGlobalGvg;
    }
    if (source.isJoinedLocalGvg != null) {
        result.isJoinedLocalGvg = source.isJoinedLocalGvg;
    }
    if (source.isReceivedSnsShareReward != null) {
        result.isReceivedSnsShareReward = source.isReceivedSnsShareReward;
    }
    if (source.isRetrievedItem != null) {
        result.isRetrievedItem = source.isRetrievedItem;
    }
    if (source.isValidContractPrivilege != null) {
        result.isValidContractPrivilege = source.isValidContractPrivilege;
    }
    if (source.latestAnnounceChatRegistrationLocalTimestamp != null) {
        result.latestAnnounceChatRegistrationLocalTimestamp = source.latestAnnounceChatRegistrationLocalTimestamp;
    }
    if (source.latestGuildSurveyCreationLocalTimestamp != null) {
        result.latestGuildSurveyCreationLocalTimestamp = source.latestGuildSurveyCreationLocalTimestamp;
    }
    if (isNonEmptyObject(source.leadLockEquipmentDialogInfoMap)) {
        result.leadLockEquipmentDialogInfoMap = mergeRecord(
            result.leadLockEquipmentDialogInfoMap,
            source.leadLockEquipmentDialogInfoMap
        );
    }
    if (source.legendLeagueClassType != null) {
        result.legendLeagueClassType = source.legendLeagueClassType;
    }
    if (source.localRaidChallengeCount != null) {
        result.localRaidChallengeCount = source.localRaidChallengeCount;
    }
    if (isNonEmptyObject(source.lockedEquipmentCharacterGuidListMap)) {
        result.lockedEquipmentCharacterGuidListMap = mergeRecord(
            result.lockedEquipmentCharacterGuidListMap,
            source.lockedEquipmentCharacterGuidListMap
        );
    }
    if (isNonEmptyObject(source.lockedUserEquipmentDtoInfoListMap)) {
        result.lockedUserEquipmentDtoInfoListMap = mergeRecord(
            result.lockedUserEquipmentDtoInfoListMap,
            source.lockedUserEquipmentDtoInfoListMap
        );
    }
    if (isNonEmptyArray(source.friendBattleFavoritePlayerIdList)) {
        result.friendBattleFavoritePlayerIdList = mergePrimitiveArray(
            result.friendBattleFavoritePlayerIdList,
            source.friendBattleFavoritePlayerIdList
        );
    }
    if (source.presentCount != null) {
        result.presentCount = source.presentCount;
    }
    if (source.privacySettingsType != null) {
        result.privacySettingsType = source.privacySettingsType;
    }
    if (isNonEmptyObject(source.receivableAchieveRankingRewardIdMap)) {
        result.receivableAchieveRankingRewardIdMap = source.receivableAchieveRankingRewardIdMap;
    }
    if (isNonEmptyArray(source.receivedAchieveRankingRewardIdList)) {
        result.receivedAchieveRankingRewardIdList = mergePrimitiveArray(
            result.receivedAchieveRankingRewardIdList,
            source.receivedAchieveRankingRewardIdList
        );
    }
    if (source.receivedAutoBattleRewardLastTime != null) {
        result.receivedAutoBattleRewardLastTime = source.receivedAutoBattleRewardLastTime;
    }
    if (isNonEmptyArray(source.receivedGuildTowerFloorRewardIdList)) {
        result.receivedGuildTowerFloorRewardIdList = mergePrimitiveArray(
            result.receivedGuildTowerFloorRewardIdList,
            source.receivedGuildTowerFloorRewardIdList
        );
    }
    if (isNonEmptyObject(source.releaseLockEquipmentCooldownTimeStampMap)) {
        result.releaseLockEquipmentCooldownTimeStampMap = mergeRecord(
            result.releaseLockEquipmentCooldownTimeStampMap,
            source.releaseLockEquipmentCooldownTimeStampMap
        );
    }
    if (isNonEmptyObject(source.shopCurrencyMissionProgressMap)) {
        result.shopCurrencyMissionProgressMap = mergeRecord(
            result.shopCurrencyMissionProgressMap,
            source.shopCurrencyMissionProgressMap
        );
    }
    if (isNonEmptyArray(source.shopProductGuerrillaPackList)) {
        result.shopProductGuerrillaPackList = mergeShopProductGuerrillaPackList(
            result.shopProductGuerrillaPackList,
            source.shopProductGuerrillaPackList
        );
    }
    if (typeof source.stripePoint === 'number' && source.stripePoint > 0) {
        result.stripePoint = source.stripePoint;
    }
    if (source.timeServerId != null) {
        result.timeServerId = source.timeServerId;
    }
    if (isNonEmptyObject(source.treasureChestCeilingCountMap)) {
        result.treasureChestCeilingCountMap = mergeRecord(
            result.treasureChestCeilingCountMap,
            source.treasureChestCeilingCountMap
        );
    }
    if (source.userBattleBossDtoInfo != null) {
        result.userBattleBossDtoInfo = source.userBattleBossDtoInfo;
    }
    if (source.userBattleLegendLeagueDtoInfo != null) {
        result.userBattleLegendLeagueDtoInfo = source.userBattleLegendLeagueDtoInfo;
    }
    if (source.userBattlePvpDtoInfo != null) {
        result.userBattlePvpDtoInfo = source.userBattlePvpDtoInfo;
    }
    if (source.userBoxSizeDtoInfo != null) {
        result.userBoxSizeDtoInfo = source.userBoxSizeDtoInfo;
    }
    if (isNonEmptyArray(source.userCharacterBookDtoInfos)) {
        result.userCharacterBookDtoInfos = source.userCharacterBookDtoInfos;
    }
    if (isNonEmptyArray(source.userCharacterCollectionDtoInfos)) {
        result.userCharacterCollectionDtoInfos = source.userCharacterCollectionDtoInfos;
    }
    if (isNonEmptyArray(source.userCharacterDtoInfos)) {
        result.userCharacterDtoInfos = mergeArrayByKey(
            result.userCharacterDtoInfos,
            source.userCharacterDtoInfos,
            (item) => item.guid
        );
    }
    if (isNonEmptyArray(source.userDeckDtoInfos)) {
        result.userDeckDtoInfos = source.userDeckDtoInfos;
    }
    if (isNonEmptyArray(source.userEquipmentDtoInfos)) {
        result.userEquipmentDtoInfos = mergeUserEquipmentList(
            result.userEquipmentDtoInfos,
            source.userEquipmentDtoInfos
        );
    }
    if (source.userEquipmentStatusDtoInfo != null) {
        result.userEquipmentStatusDtoInfo = source.userEquipmentStatusDtoInfo;
    }
    if (isNonEmptyArray(source.userItemDtoInfo)) {
        result.userItemDtoInfo = mergeUserItemList(result.userItemDtoInfo, source.userItemDtoInfo);
    }
    if (source.userLevelLinkDtoInfo != null) {
        result.userLevelLinkDtoInfo = source.userLevelLinkDtoInfo;
    }
    if (isNonEmptyArray(source.userLevelLinkMemberDtoInfos)) {
        result.userLevelLinkMemberDtoInfos = mergeLevelLinkMemberList(
            result.userLevelLinkMemberDtoInfos,
            source.userLevelLinkMemberDtoInfos
        );
    }
    if (isNonEmptyArray(source.userMissionActivityDtoInfos)) {
        result.userMissionActivityDtoInfos = source.userMissionActivityDtoInfos;
    }
    if (isNonEmptyArray(source.userMissionDtoInfos)) {
        result.userMissionDtoInfos = source.userMissionDtoInfos;
    }
    if (source.userMissionOccurrenceHistoryDtoInfo != null) {
        result.userMissionOccurrenceHistoryDtoInfo = source.userMissionOccurrenceHistoryDtoInfo;
    }
    if (isNonEmptyArray(source.userFriendMissionDtoInfoList)) {
        result.userFriendMissionDtoInfoList = source.userFriendMissionDtoInfoList;
    }
    if (isNonEmptyObject(source.userGuidanceTimeMap)) {
        result.userGuidanceTimeMap = mergeRecord(result.userGuidanceTimeMap, source.userGuidanceTimeMap);
    }
    if (isNonEmptyArray(source.userNotificationDtoInfoInfos)) {
        result.userNotificationDtoInfoInfos = source.userNotificationDtoInfoInfos;
    }
    if (isNonEmptyArray(source.userOpenContentDtoInfos)) {
        result.userOpenContentDtoInfos = source.userOpenContentDtoInfos;
    }
    if (source.userFriendBattleOptionDtoInfo != null) {
        result.userFriendBattleOptionDtoInfo = source.userFriendBattleOptionDtoInfo;
    }
    if (source.userRecruitGuildMemberSettingDtoInfo != null) {
        result.userRecruitGuildMemberSettingDtoInfo = source.userRecruitGuildMemberSettingDtoInfo;
    }
    if (isNonEmptyArray(source.userSettingsDtoInfoList)) {
        result.userSettingsDtoInfoList = source.userSettingsDtoInfoList;
    }
    if (isNonEmptyArray(source.userShopAchievementPackDtoInfos)) {
        result.userShopAchievementPackDtoInfos = source.userShopAchievementPackDtoInfos;
    }
    if (source.userShopFirstChargeBonusDtoInfo != null) {
        result.userShopFirstChargeBonusDtoInfo = source.userShopFirstChargeBonusDtoInfo;
    }
    if (isNonEmptyArray(source.userShopFreeGrowthPackDtoInfos)) {
        result.userShopFreeGrowthPackDtoInfos = source.userShopFreeGrowthPackDtoInfos;
    }
    if (isNonEmptyArray(source.userShopMonthlyBoostDtoInfos)) {
        result.userShopMonthlyBoostDtoInfos = source.userShopMonthlyBoostDtoInfos;
    }
    if (isNonEmptyArray(source.userShopSubscriptionDtoInfos)) {
        result.userShopSubscriptionDtoInfos = source.userShopSubscriptionDtoInfos;
    }
    if (source.userStatusDtoInfo != null) {
        result.userStatusDtoInfo = source.userStatusDtoInfo;
    }
    if (source.userSyncGvgDeckDtoInfo != null) {
        result.userSyncGvgDeckDtoInfo = source.userSyncGvgDeckDtoInfo;
    }
    if (isNonEmptyArray(source.userTowerBattleDtoInfos)) {
        result.userTowerBattleDtoInfos = mergeUserTowerBattleList(
            result.userTowerBattleDtoInfos,
            source.userTowerBattleDtoInfos
        );
    }
    if (isNonEmptyArray(source.userVipGiftDtoInfos)) {
        result.userVipGiftDtoInfos = source.userVipGiftDtoInfos;
    }
    if (source.existPurchasableOneWeekLimitedPack != null) {
        result.existPurchasableOneWeekLimitedPack = source.existPurchasableOneWeekLimitedPack;
    }
    if (source.chatSettingData != null) {
        result.chatSettingData = source.chatSettingData;
    }
    if (isNonEmptyArray(source.chatEmoticonList)) {
        result.chatEmoticonList = source.chatEmoticonList;
    }
    if (isNonEmptyArray(source.characterIconEffectList)) {
        result.characterIconEffectList = source.characterIconEffectList;
    }

    return result;
};
