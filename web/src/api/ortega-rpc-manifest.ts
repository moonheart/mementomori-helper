// @ts-nocheck
// 该文件由 C# 后端自动生成，仅包含类型定义，请勿手动修改
/* eslint-disable */

import type {
    AuthComebackUserRequest,
    AuthComebackUserResponse,
    AuthCreateAccessTokenRequest,
    AuthCreateAccessTokenResponse,
    AuthCreateUserRequest,
    AuthCreateUserResponse,
    AuthCreateWorldPlayerRequest,
    AuthCreateWorldPlayerResponse,
    AuthGetComebackUserDataRequest,
    AuthGetComebackUserDataResponse,
    AuthGetDataUriRequest,
    AuthGetDataUriResponse,
    AuthGetServerHostRequest,
    AuthGetServerHostResponse,
    AuthLoginRequest,
    AuthLoginResponse,
    BattleAutoRequest,
    BattleAutoResponse,
    BattleBossQuickRequest,
    BattleBossQuickResponse,
    BattleBossRequest,
    BattleBossResponse,
    BattleBossRewardInfoRequest,
    BattleBossRewardInfoResponse,
    BattleGetCompetitionInfoRequest,
    BattleGetCompetitionInfoResponse,
    BattleGetLegendLeagueInfoRequest,
    BattleGetLegendLeagueInfoResponse,
    BattleGetLegendLeagueLogsRequest,
    BattleGetLegendLeagueLogsResponse,
    BattleGetLegendLeagueResultDetailRequest,
    BattleGetLegendLeagueResultDetailResponse,
    BattleGetLegendLeagueTopRankerRequest,
    BattleGetLegendLeagueTopRankerResponse,
    BattleGetPvpBattleLogsRequest,
    BattleGetPvpBattleLogsResponse,
    BattleGetPvpBattleResultDetailRequest,
    BattleGetPvpBattleResultDetailResponse,
    BattleGetPvpInfoRequest,
    BattleGetPvpInfoResponse,
    BattleGetPvpTopRankerRequest,
    BattleGetPvpTopRankerResponse,
    BattleLegendLeagueStartRequest,
    BattleLegendLeagueStartResponse,
    BattleNextQuestRequest,
    BattleNextQuestResponse,
    BattlePvpStartRequest,
    BattlePvpStartResponse,
    BattleQuickRequest,
    BattleQuickResponse,
    BattleRewardAutoBattleRequest,
    BattleRewardAutoBattleResponse,
    BookSortAssistanceBookSortAssistanceAddAssistanceRequest,
    BookSortAssistanceBookSortAssistanceAddAssistanceResponse,
    BookSortAssistanceBookSortAssistanceDispatchRequest,
    BookSortAssistanceBookSortAssistanceDispatchResponse,
    BookSortAssistanceBookSortAssistanceGetInfoRequest,
    BookSortAssistanceBookSortAssistanceGetInfoResponse,
    BookSortAssistanceBookSortAssistanceRewardRequest,
    BookSortAssistanceBookSortAssistanceRewardResponse,
    BookSortBookSortBulkUnlockGridCellRequest,
    BookSortBookSortBulkUnlockGridCellResponse,
    BookSortBookSortGetInfoRequest,
    BookSortBookSortGetInfoResponse,
    BookSortBookSortGetRewardInfoRequest,
    BookSortBookSortGetRewardInfoResponse,
    BookSortBookSortSelectBonusFloorRewardRequest,
    BookSortBookSortSelectBonusFloorRewardResponse,
    BookSortBookSortUnlockGridCellRequest,
    BookSortBookSortUnlockGridCellResponse,
    BookSortBookSortUpFloorRequest,
    BookSortBookSortUpFloorResponse,
    BountyQuestGetListRequest,
    BountyQuestGetListResponse,
    BountyQuestQuickRequest,
    BountyQuestQuickResponse,
    BountyQuestRemakeRequest,
    BountyQuestRemakeResponse,
    BountyQuestRewardRequest,
    BountyQuestRewardResponse,
    BountyQuestStartRequest,
    BountyQuestStartResponse,
    CharacterBulkLevelUpRequest,
    CharacterBulkLevelUpResponse,
    CharacterGetCharacterStoryRewardRequest,
    CharacterGetCharacterStoryRewardResponse,
    CharacterGetDetailsInfoRequest,
    CharacterGetDetailsInfoResponse,
    CharacterLevelUpRequest,
    CharacterLevelUpResponse,
    CharacterRankUpRequest,
    CharacterRankUpResponse,
    CharacterReadCharacterStoryRequest,
    CharacterReadCharacterStoryResponse,
    DebugAddMusicPlayLogRequest,
    DebugAddMusicPlayLogResponse,
    DebugGiveAllSrCharacterRequest,
    DebugGiveAllSrCharacterResponse,
    DebugSetMiningQuestBenefitPurchaseCountRequest,
    DebugSetMiningQuestBenefitPurchaseCountResponse,
    DebugSetMiningQuestMaxScoreRequest,
    DebugSetMiningQuestMaxScoreResponse,
    DebugSetMiningQuestTotalRewardCountRequest,
    DebugSetMiningQuestTotalRewardCountResponse,
    DebugSetVipLevelRequest,
    DebugSetVipLevelResponse,
    DebugUnionMissionResetAcceptCountRequest,
    DebugUnionMissionResetAcceptCountResponse,
    DungeonBattleExecBattleRequest,
    DungeonBattleExecBattleResponse,
    DungeonBattleExecGuestRequest,
    DungeonBattleExecGuestResponse,
    DungeonBattleExecRecoveryRequest,
    DungeonBattleExecRecoveryResponse,
    DungeonBattleExecReinforceRelicRequest,
    DungeonBattleExecReinforceRelicResponse,
    DungeonBattleExecReviveRequest,
    DungeonBattleExecReviveResponse,
    DungeonBattleExecShopRequest,
    DungeonBattleExecShopResponse,
    DungeonBattleFinishBattleRequest,
    DungeonBattleFinishBattleResponse,
    DungeonBattleGetBattleGridDataRequest,
    DungeonBattleGetBattleGridDataResponse,
    DungeonBattleGetDungeonBattleInfoRequest,
    DungeonBattleGetDungeonBattleInfoResponse,
    DungeonBattleLeaveShopRequest,
    DungeonBattleLeaveShopResponse,
    DungeonBattleProceedLayerRequest,
    DungeonBattleProceedLayerResponse,
    DungeonBattleRewardBattleReceiveRelicRequest,
    DungeonBattleRewardBattleReceiveRelicResponse,
    DungeonBattleRewardBattleReinforceRelicRequest,
    DungeonBattleRewardBattleReinforceRelicResponse,
    DungeonBattleRewardClearLayerRequest,
    DungeonBattleRewardClearLayerResponse,
    DungeonBattleSelectGridRequest,
    DungeonBattleSelectGridResponse,
    DungeonBattleSkipDungeonBattleRequest,
    DungeonBattleSkipDungeonBattleResponse,
    DungeonBattleUseRecoveryItemRequest,
    DungeonBattleUseRecoveryItemResponse,
    EquipmentCastManyRequest,
    EquipmentCastManyResponse,
    EquipmentCastRequest,
    EquipmentCastResponse,
    EquipmentChangeEquipmentRequest,
    EquipmentChangeEquipmentResponse,
    EquipmentChangeSphereRequest,
    EquipmentChangeSphereResponse,
    EquipmentComposeManySphereRequest,
    EquipmentComposeManySphereResponse,
    EquipmentInheritanceEquipmentRequest,
    EquipmentInheritanceEquipmentResponse,
    EquipmentReinforcementRequest,
    EquipmentReinforcementResponse,
    EquipmentRemoveEquipmentRequest,
    EquipmentRemoveEquipmentResponse,
    EquipmentSynchroEquipmentSynchroRewardMissionRequest,
    EquipmentSynchroEquipmentSynchroRewardMissionResponse,
    EquipmentTrainingRequest,
    EquipmentTrainingResponse,
    EquipmentUnlockSphereSlotRequest,
    EquipmentUnlockSphereSlotResponse,
    FriendApplyFriendRequest,
    FriendApplyFriendResponse,
    FriendBulkApplyFriendsRequest,
    FriendBulkApplyFriendsResponse,
    FriendBulkTransferFriendPointRequest,
    FriendBulkTransferFriendPointResponse,
    FriendCancelAllApplyFriendRequest,
    FriendCancelAllApplyFriendResponse,
    FriendGetFriendCampaignInfoRequest,
    FriendGetFriendCampaignInfoResponse,
    FriendGetFriendStatusRequest,
    FriendGetFriendStatusResponse,
    FriendGetPlayerInfoListRequest,
    FriendGetPlayerInfoListResponse,
    FriendReceiveFriendPointRequest,
    FriendReceiveFriendPointResponse,
    FriendRemoveFriendRequest,
    FriendRemoveFriendResponse,
    FriendReplyAllFriendRequest,
    FriendReplyAllFriendResponse,
    FriendReplyFriendRequest,
    FriendReplyFriendResponse,
    FriendRewardFriendMissionRequest,
    FriendRewardFriendMissionResponse,
    FriendSearchFriendRequest,
    FriendSearchFriendResponse,
    FriendSendFriendPointRequest,
    FriendSendFriendPointResponse,
    FriendUpdateBlockListRequest,
    FriendUpdateBlockListResponse,
    FriendUseFriendCodeRequest,
    FriendUseFriendCodeResponse,
    GachaChangeGachaRelicRequest,
    GachaChangeGachaRelicResponse,
    GachaDrawRequest,
    GachaDrawResponse,
    GachaGetListRequest,
    GachaGetListResponse,
    GachaGetLotteryItemListRequest,
    GachaGetLotteryItemListResponse,
    GachaSetSelectListRequest,
    GachaSetSelectListResponse,
    GlobalGvgReceiveGlobalGvgRewardRequest,
    GlobalGvgReceiveGlobalGvgRewardResponse,
    GuildChatRecruitGuildMemberRequest,
    GuildChatRecruitGuildMemberResponse,
    GuildGetGuildBaseInfoRequest,
    GuildGetGuildBaseInfoResponse,
    GuildGetGuildIdRequest,
    GuildGetGuildIdResponse,
    GuildGetGuildMemberInfoRequest,
    GuildGetGuildMemberInfoResponse,
    GuildNoteCreateGuildNoteRequest,
    GuildNoteCreateGuildNoteResponse,
    GuildNoteGetGuildNoteListRequest,
    GuildNoteGetGuildNoteListResponse,
    GuildNoteGetReactionDetailInfoRequest,
    GuildNoteGetReactionDetailInfoResponse,
    GuildNoteReactGuildNoteRequest,
    GuildNoteReactGuildNoteResponse,
    GuildNoteShareGuildNoteRequest,
    GuildNoteShareGuildNoteResponse,
    GuildNoteUpdateGuildNoteListRequest,
    GuildNoteUpdateGuildNoteListResponse,
    GuildNoteUpdateGuildNoteRequest,
    GuildNoteUpdateGuildNoteResponse,
    GuildRaidGetGuildRaidInfoRequest,
    GuildRaidGetGuildRaidInfoResponse,
    GuildRaidGetGuildRaidWorldRewardInfoRequest,
    GuildRaidGetGuildRaidWorldRewardInfoResponse,
    GuildRaidGiveGuildRaidWorldRewardItemRequest,
    GuildRaidGiveGuildRaidWorldRewardItemResponse,
    GuildRaidOpenGuildRaidRequest,
    GuildRaidOpenGuildRaidResponse,
    GuildRaidQuickStartGuildRaidRequest,
    GuildRaidQuickStartGuildRaidResponse,
    GuildRaidStartGuildRaidRequest,
    GuildRaidStartGuildRaidResponse,
    GuildRaidStartGuildRaidTrialRequest,
    GuildRaidStartGuildRaidTrialResponse,
    GuildSearchGuildByRecruitGuildMemberChatRequest,
    GuildSearchGuildByRecruitGuildMemberChatResponse,
    GuildTowerBattleRequest,
    GuildTowerBattleResponse,
    GuildTowerEntryCharacterRequest,
    GuildTowerEntryCharacterResponse,
    GuildTowerGetGuildTowerInfoRequest,
    GuildTowerGetGuildTowerInfoResponse,
    GuildTowerGetGuildTowerRankingRequest,
    GuildTowerGetGuildTowerRankingResponse,
    GuildTowerGetReinforcementJobDataRequest,
    GuildTowerGetReinforcementJobDataResponse,
    GuildTowerReceiveFloorRewardRequest,
    GuildTowerReceiveFloorRewardResponse,
    GuildTowerReinforceJobRequest,
    GuildTowerReinforceJobResponse,
    ItemGetAutoBattleRewardItemInfoRequest,
    ItemGetAutoBattleRewardItemInfoResponse,
    ItemOpenTreasureChestRequest,
    ItemOpenTreasureChestResponse,
    ItemUseAutoBattleRewardItemRequest,
    ItemUseAutoBattleRewardItemResponse,
    LegendLeagueBuyLegendLeagueIconRewardRequest,
    LegendLeagueBuyLegendLeagueIconRewardResponse,
    LegendLeagueGetLegendLeagueIconRewardInfoRequest,
    LegendLeagueGetLegendLeagueIconRewardInfoResponse,
    LevelLinkAddExpRequest,
    LevelLinkAddExpResponse,
    LevelLinkLevelUpRequest,
    LevelLinkLevelUpResponse,
    LevelLinkResetCoolTimeRequest,
    LevelLinkResetCoolTimeResponse,
    LevelLinkSetMemberRequest,
    LevelLinkSetMemberResponse,
    LevelLinkSortMemberRequest,
    LevelLinkSortMemberResponse,
    LevelLinkUnSetMemberRequest,
    LevelLinkUnSetMemberResponse,
    LocalGvgReceiveLocalGvgRewardRequest,
    LocalGvgReceiveLocalGvgRewardResponse,
    LocalRaidGetLocalRaidBattleLogsRequest,
    LocalRaidGetLocalRaidBattleLogsResponse,
    LocalRaidGetLocalRaidBattleResultOldRequest,
    LocalRaidGetLocalRaidBattleResultOldResponse,
    LocalRaidGetLocalRaidBattleResultRequest,
    LocalRaidGetLocalRaidBattleResultResponse,
    LocalRaidGetLocalRaidFriendInfoRequest,
    LocalRaidGetLocalRaidFriendInfoResponse,
    LocalRaidGetLocalRaidInfoRequest,
    LocalRaidGetLocalRaidInfoResponse,
    LoginBonusGetLimitedLoginBonusInfoRequest,
    LoginBonusGetLimitedLoginBonusInfoResponse,
    LoginBonusGetMonthlyLoginBonusInfoRequest,
    LoginBonusGetMonthlyLoginBonusInfoResponse,
    LoginBonusReceiveDailyLimitedLoginBonusRequest,
    LoginBonusReceiveDailyLimitedLoginBonusResponse,
    LoginBonusReceiveDailyLoginBonusRequest,
    LoginBonusReceiveDailyLoginBonusResponse,
    LoginBonusReceiveLoginCountBonusRequest,
    LoginBonusReceiveLoginCountBonusResponse,
    LoginBonusReceiveSpecialLimitedLoginBonusRequest,
    LoginBonusReceiveSpecialLimitedLoginBonusResponse,
    LuckyChanceLuckyChanceGetDetailInfoRequest,
    LuckyChanceLuckyChanceGetDetailInfoResponse,
    LuckyChanceLuckyChanceSelectButtonRequest,
    LuckyChanceLuckyChanceSelectButtonResponse,
    LuckyChanceRegisterRealPrizeCItemInfoRequest,
    LuckyChanceRegisterRealPrizeCItemInfoResponse,
    MiningQuestMiningQuestFinishRequest,
    MiningQuestMiningQuestFinishResponse,
    MiningQuestMiningQuestGetBenefitInfoRequest,
    MiningQuestMiningQuestGetBenefitInfoResponse,
    MiningQuestMiningQuestGetCurrentParametersRequest,
    MiningQuestMiningQuestGetCurrentParametersResponse,
    MiningQuestMiningQuestGetGradeInfoRequest,
    MiningQuestMiningQuestGetGradeInfoResponse,
    MiningQuestMiningQuestGetInfoRequest,
    MiningQuestMiningQuestGetInfoResponse,
    MiningQuestMiningQuestGetMaxRewardCountInfoRequest,
    MiningQuestMiningQuestGetMaxRewardCountInfoResponse,
    MiningQuestMiningQuestReceiveGradeDailyRewardRequest,
    MiningQuestMiningQuestReceiveGradeDailyRewardResponse,
    MiningQuestMiningQuestReinforcementRequest,
    MiningQuestMiningQuestReinforcementResponse,
    MiningQuestMiningQuestStartRequest,
    MiningQuestMiningQuestStartResponse,
    MiningQuestMiningQuestUnlockBenefitRequest,
    MiningQuestMiningQuestUnlockBenefitResponse,
    MissionGetMissionInfoRequest,
    MissionGetMissionInfoResponse,
    MissionReceivePanelMissionBingoRewardRequest,
    MissionReceivePanelMissionBingoRewardResponse,
    MissionRewardMissionActivityRequest,
    MissionRewardMissionActivityResponse,
    MissionRewardMissionRequest,
    MissionRewardMissionResponse,
    NoticeGetMyPageNoticeInfoListRequest,
    NoticeGetMyPageNoticeInfoListResponse,
    NoticeGetNoticeInfoListRequest,
    NoticeGetNoticeInfoListResponse,
    PopularityVoteGetInfoRequest,
    PopularityVoteGetInfoResponse,
    PopularityVoteRewardVoteItemRequest,
    PopularityVoteRewardVoteItemResponse,
    PresentDeletePresentRequest,
    PresentDeletePresentResponse,
    PresentGetListRequest,
    PresentGetListResponse,
    PresentReceiveItemRequest,
    PresentReceiveItemResponse,
    QuestGetQuestInfoRequest,
    QuestGetQuestInfoResponse,
    QuestMapInfoRequest,
    QuestMapInfoResponse,
    RankingGetAchieveRankingPlayerInfoRequest,
    RankingGetAchieveRankingPlayerInfoResponse,
    RankingGetBulkAchieveRankingPlayerInfoRequest,
    RankingGetBulkAchieveRankingPlayerInfoResponse,
    RankingGetGuildRankingRequest,
    RankingGetGuildRankingResponse,
    RankingGetPlayerRankingRequest,
    RankingGetPlayerRankingResponse,
    RankingGetRankingBattleLogRequest,
    RankingGetRankingBattleLogResponse,
    RankingGetTowerRankingRequest,
    RankingGetTowerRankingResponse,
    RankingReceiveAchieveRankingRewardRequest,
    RankingReceiveAchieveRankingRewardResponse,
    RentalRaidGetShareRewardInfoRequest,
    RentalRaidGetShareRewardInfoResponse,
    RentalRaidReceiveShareRewardRequest,
    RentalRaidReceiveShareRewardResponse,
    ShopBulkBuyProductWithWebCrystalRequest,
    ShopBulkBuyProductWithWebCrystalResponse,
    ShopBuyProductRequest,
    ShopBuyProductResponse,
    ShopBuyProductWithWebCrystalRequest,
    ShopBuyProductWithWebCrystalResponse,
    ShopGetBoiPaymentDetailHistoryRequest,
    ShopGetBoiPaymentDetailHistoryResponse,
    ShopGetBoiPaymentHistoryListRequest,
    ShopGetBoiPaymentHistoryListResponse,
    ShopGetBoiPointHistoryListRequest,
    ShopGetBoiPointHistoryListResponse,
    ShopGetListRequest,
    ShopGetListResponse,
    ShopGetProductListRequest,
    ShopGetProductListResponse,
    ShopGetWebStoreGuidancePanelRequest,
    ShopGetWebStoreGuidancePanelResponse,
    ShopReceiveRewardRequest,
    ShopReceiveRewardResponse,
    ShopRegisterActionLogRequest,
    ShopRegisterActionLogResponse,
    StarsGuidanceTradeShopExchangeItemRequest,
    StarsGuidanceTradeShopExchangeItemResponse,
    StarsGuidanceTradeShopGetStarsGuidanceTradeShopInfoRequest,
    StarsGuidanceTradeShopGetStarsGuidanceTradeShopInfoResponse,
    TowerBattleStartRequest,
    TowerBattleStartResponse,
    TowerBattleTowerBattleQuickRequest,
    TowerBattleTowerBattleQuickResponse,
    TradeShopBuyItemRequest,
    TradeShopBuyItemResponse,
    TradeShopGetListRequest,
    TradeShopGetListResponse,
    TradeShopResetTabRequest,
    TradeShopResetTabResponse,
    UnionMissionEntryUnionMissionRequest,
    UnionMissionEntryUnionMissionResponse,
    UnionMissionUnionMissionAcceptMissionRequest,
    UnionMissionUnionMissionAcceptMissionResponse,
    UnionMissionUnionMissionBuyAcceptCountRequest,
    UnionMissionUnionMissionBuyAcceptCountResponse,
    UnionMissionUnionMissionGetInfoRequest,
    UnionMissionUnionMissionGetInfoResponse,
    UnionMissionUnionMissionGetRankingRequest,
    UnionMissionUnionMissionGetRankingResponse,
    UnionMissionUnionMissionReceiveIndividualTotalPointRewardRequest,
    UnionMissionUnionMissionReceiveIndividualTotalPointRewardResponse,
    UnionMissionUnionMissionReceiveMissionRewardRequest,
    UnionMissionUnionMissionReceiveMissionRewardResponse,
    UnionMissionUnionMissionReceiveTotalPointRewardRequest,
    UnionMissionUnionMissionReceiveTotalPointRewardResponse,
    UnionMissionUnionMissionRefreshPersonalMissionRequest,
    UnionMissionUnionMissionRefreshPersonalMissionResponse,
    UnionMissionUnionMissionRefreshSharedMissionRequest,
    UnionMissionUnionMissionRefreshSharedMissionResponse,
    UnionMissionUnionMissionResetCoolTimeMissionRequest,
    UnionMissionUnionMissionResetCoolTimeMissionResponse,
    UnionMissionUnionMissionRetireMissionRequest,
    UnionMissionUnionMissionRetireMissionResponse,
    UnionMissionUnionMissionSelectMissionRequest,
    UnionMissionUnionMissionSelectMissionResponse,
    UserGetMypageRequest,
    UserGetMypageResponse,
    UserGetUserDataRequest,
    UserGetUserDataResponse,
    UserLoginPlayerRequest,
    UserLoginPlayerResponse,
    UserSaveUsedDeckRequest,
    UserSaveUsedDeckResponse,
    VipGetDailyGiftRequest,
    VipGetDailyGiftResponse,
    WebStoreLoginCampaignGetLoginCampaignInfoRequest,
    WebStoreLoginCampaignGetLoginCampaignInfoResponse,
    WebStoreLoginCampaignRewardLoginCampaignItemRequest,
    WebStoreLoginCampaignRewardLoginCampaignItemResponse,
    WeeklyTopicsGetWeeklyTopicsInfoRequest,
    WeeklyTopicsGetWeeklyTopicsInfoResponse,
} from './generated';

export interface OrtegaRpcMap {
    "auth/comebackUser": {
        request: AuthComebackUserRequest;
        response: AuthComebackUserResponse;
    };
    "auth/createAccessToken": {
        request: AuthCreateAccessTokenRequest;
        response: AuthCreateAccessTokenResponse;
    };
    "auth/createUser": {
        request: AuthCreateUserRequest;
        response: AuthCreateUserResponse;
    };
    "auth/createWorldPlayer": {
        request: AuthCreateWorldPlayerRequest;
        response: AuthCreateWorldPlayerResponse;
    };
    "auth/getComebackUserData": {
        request: AuthGetComebackUserDataRequest;
        response: AuthGetComebackUserDataResponse;
    };
    "auth/getDataUri": {
        request: AuthGetDataUriRequest;
        response: AuthGetDataUriResponse;
    };
    "auth/getServerHost": {
        request: AuthGetServerHostRequest;
        response: AuthGetServerHostResponse;
    };
    "auth/login": {
        request: AuthLoginRequest;
        response: AuthLoginResponse;
    };
    "badge/getCompetitionInfo": {
        request: BattleGetCompetitionInfoRequest;
        response: BattleGetCompetitionInfoResponse;
    };
    "battle/auto": {
        request: BattleAutoRequest;
        response: BattleAutoResponse;
    };
    "battle/boss": {
        request: BattleBossRequest;
        response: BattleBossResponse;
    };
    "battle/bossQuick": {
        request: BattleBossQuickRequest;
        response: BattleBossQuickResponse;
    };
    "battle/bossRewardInfo": {
        request: BattleBossRewardInfoRequest;
        response: BattleBossRewardInfoResponse;
    };
    "battle/getLegendLeagueInfo": {
        request: BattleGetLegendLeagueInfoRequest;
        response: BattleGetLegendLeagueInfoResponse;
    };
    "battle/getLegendLeagueLogs": {
        request: BattleGetLegendLeagueLogsRequest;
        response: BattleGetLegendLeagueLogsResponse;
    };
    "battle/getLegendLeagueResultDetail": {
        request: BattleGetLegendLeagueResultDetailRequest;
        response: BattleGetLegendLeagueResultDetailResponse;
    };
    "battle/getLegendLeagueTopRanker": {
        request: BattleGetLegendLeagueTopRankerRequest;
        response: BattleGetLegendLeagueTopRankerResponse;
    };
    "battle/getPvpBattleLogs": {
        request: BattleGetPvpBattleLogsRequest;
        response: BattleGetPvpBattleLogsResponse;
    };
    "battle/getPvpBattleResultDetail": {
        request: BattleGetPvpBattleResultDetailRequest;
        response: BattleGetPvpBattleResultDetailResponse;
    };
    "battle/getPvpInfo": {
        request: BattleGetPvpInfoRequest;
        response: BattleGetPvpInfoResponse;
    };
    "battle/getPvpTopRanker": {
        request: BattleGetPvpTopRankerRequest;
        response: BattleGetPvpTopRankerResponse;
    };
    "battle/legendLeagueStart": {
        request: BattleLegendLeagueStartRequest;
        response: BattleLegendLeagueStartResponse;
    };
    "battle/nextQuest": {
        request: BattleNextQuestRequest;
        response: BattleNextQuestResponse;
    };
    "battle/pvpStart": {
        request: BattlePvpStartRequest;
        response: BattlePvpStartResponse;
    };
    "battle/quick": {
        request: BattleQuickRequest;
        response: BattleQuickResponse;
    };
    "battle/rewardAutoBattle": {
        request: BattleRewardAutoBattleRequest;
        response: BattleRewardAutoBattleResponse;
    };
    "bookSort/bulkUnlockGridCell": {
        request: BookSortBookSortBulkUnlockGridCellRequest;
        response: BookSortBookSortBulkUnlockGridCellResponse;
    };
    "bookSort/getInfo": {
        request: BookSortBookSortGetInfoRequest;
        response: BookSortBookSortGetInfoResponse;
    };
    "bookSort/getRewardInfo": {
        request: BookSortBookSortGetRewardInfoRequest;
        response: BookSortBookSortGetRewardInfoResponse;
    };
    "bookSort/selectBonusFloorReward": {
        request: BookSortBookSortSelectBonusFloorRewardRequest;
        response: BookSortBookSortSelectBonusFloorRewardResponse;
    };
    "bookSort/unlockGridCell": {
        request: BookSortBookSortUnlockGridCellRequest;
        response: BookSortBookSortUnlockGridCellResponse;
    };
    "bookSort/upFloor": {
        request: BookSortBookSortUpFloorRequest;
        response: BookSortBookSortUpFloorResponse;
    };
    "bookSortAssistance/addAssistance": {
        request: BookSortAssistanceBookSortAssistanceAddAssistanceRequest;
        response: BookSortAssistanceBookSortAssistanceAddAssistanceResponse;
    };
    "bookSortAssistance/dispatch": {
        request: BookSortAssistanceBookSortAssistanceDispatchRequest;
        response: BookSortAssistanceBookSortAssistanceDispatchResponse;
    };
    "bookSortAssistance/getInfo": {
        request: BookSortAssistanceBookSortAssistanceGetInfoRequest;
        response: BookSortAssistanceBookSortAssistanceGetInfoResponse;
    };
    "bookSortAssistance/reward": {
        request: BookSortAssistanceBookSortAssistanceRewardRequest;
        response: BookSortAssistanceBookSortAssistanceRewardResponse;
    };
    "bountyQuest/getList": {
        request: BountyQuestGetListRequest;
        response: BountyQuestGetListResponse;
    };
    "bountyQuest/quick": {
        request: BountyQuestQuickRequest;
        response: BountyQuestQuickResponse;
    };
    "bountyQuest/remake": {
        request: BountyQuestRemakeRequest;
        response: BountyQuestRemakeResponse;
    };
    "bountyQuest/reward": {
        request: BountyQuestRewardRequest;
        response: BountyQuestRewardResponse;
    };
    "bountyQuest/start": {
        request: BountyQuestStartRequest;
        response: BountyQuestStartResponse;
    };
    "character/bulkLevelUp": {
        request: CharacterBulkLevelUpRequest;
        response: CharacterBulkLevelUpResponse;
    };
    "character/getCharacterStoryReward": {
        request: CharacterGetCharacterStoryRewardRequest;
        response: CharacterGetCharacterStoryRewardResponse;
    };
    "character/getDetailsInfo": {
        request: CharacterGetDetailsInfoRequest;
        response: CharacterGetDetailsInfoResponse;
    };
    "character/levelUp": {
        request: CharacterLevelUpRequest;
        response: CharacterLevelUpResponse;
    };
    "character/rankUp": {
        request: CharacterRankUpRequest;
        response: CharacterRankUpResponse;
    };
    "character/readCharacterStory": {
        request: CharacterReadCharacterStoryRequest;
        response: CharacterReadCharacterStoryResponse;
    };
    "debug/addMusicPlayLog": {
        request: DebugAddMusicPlayLogRequest;
        response: DebugAddMusicPlayLogResponse;
    };
    "debug/giveAllSrCharacter": {
        request: DebugGiveAllSrCharacterRequest;
        response: DebugGiveAllSrCharacterResponse;
    };
    "debug/setMiningQuestBenefitPurchaseCount": {
        request: DebugSetMiningQuestBenefitPurchaseCountRequest;
        response: DebugSetMiningQuestBenefitPurchaseCountResponse;
    };
    "debug/setMiningQuestMaxScore": {
        request: DebugSetMiningQuestMaxScoreRequest;
        response: DebugSetMiningQuestMaxScoreResponse;
    };
    "debug/setMiningQuestTotalRewardCount": {
        request: DebugSetMiningQuestTotalRewardCountRequest;
        response: DebugSetMiningQuestTotalRewardCountResponse;
    };
    "debug/setVipLevel": {
        request: DebugSetVipLevelRequest;
        response: DebugSetVipLevelResponse;
    };
    "debug/unionMissionResetAcceptCount": {
        request: DebugUnionMissionResetAcceptCountRequest;
        response: DebugUnionMissionResetAcceptCountResponse;
    };
    "dungeonBattle/execBattle": {
        request: DungeonBattleExecBattleRequest;
        response: DungeonBattleExecBattleResponse;
    };
    "dungeonBattle/execGuest": {
        request: DungeonBattleExecGuestRequest;
        response: DungeonBattleExecGuestResponse;
    };
    "dungeonBattle/execRecovery": {
        request: DungeonBattleExecRecoveryRequest;
        response: DungeonBattleExecRecoveryResponse;
    };
    "dungeonBattle/execReinforceRelic": {
        request: DungeonBattleExecReinforceRelicRequest;
        response: DungeonBattleExecReinforceRelicResponse;
    };
    "dungeonBattle/execRevive": {
        request: DungeonBattleExecReviveRequest;
        response: DungeonBattleExecReviveResponse;
    };
    "dungeonBattle/execShop": {
        request: DungeonBattleExecShopRequest;
        response: DungeonBattleExecShopResponse;
    };
    "dungeonBattle/finishBattle": {
        request: DungeonBattleFinishBattleRequest;
        response: DungeonBattleFinishBattleResponse;
    };
    "dungeonBattle/getBattleGridData": {
        request: DungeonBattleGetBattleGridDataRequest;
        response: DungeonBattleGetBattleGridDataResponse;
    };
    "dungeonBattle/getDungeonBattleInfo": {
        request: DungeonBattleGetDungeonBattleInfoRequest;
        response: DungeonBattleGetDungeonBattleInfoResponse;
    };
    "dungeonBattle/leaveShop": {
        request: DungeonBattleLeaveShopRequest;
        response: DungeonBattleLeaveShopResponse;
    };
    "dungeonBattle/proceedLayer": {
        request: DungeonBattleProceedLayerRequest;
        response: DungeonBattleProceedLayerResponse;
    };
    "dungeonBattle/rewardBattleReceiveRelic": {
        request: DungeonBattleRewardBattleReceiveRelicRequest;
        response: DungeonBattleRewardBattleReceiveRelicResponse;
    };
    "dungeonBattle/rewardBattleReinforceRelic": {
        request: DungeonBattleRewardBattleReinforceRelicRequest;
        response: DungeonBattleRewardBattleReinforceRelicResponse;
    };
    "dungeonBattle/rewardClearLayer": {
        request: DungeonBattleRewardClearLayerRequest;
        response: DungeonBattleRewardClearLayerResponse;
    };
    "dungeonBattle/selectGrid": {
        request: DungeonBattleSelectGridRequest;
        response: DungeonBattleSelectGridResponse;
    };
    "dungeonBattle/skipBattle": {
        request: DungeonBattleSkipDungeonBattleRequest;
        response: DungeonBattleSkipDungeonBattleResponse;
    };
    "dungeonBattle/useRecoveryItem": {
        request: DungeonBattleUseRecoveryItemRequest;
        response: DungeonBattleUseRecoveryItemResponse;
    };
    "equipment/cast": {
        request: EquipmentCastRequest;
        response: EquipmentCastResponse;
    };
    "equipment/castMany": {
        request: EquipmentCastManyRequest;
        response: EquipmentCastManyResponse;
    };
    "equipment/changeEquipment": {
        request: EquipmentChangeEquipmentRequest;
        response: EquipmentChangeEquipmentResponse;
    };
    "equipment/changeSphere": {
        request: EquipmentChangeSphereRequest;
        response: EquipmentChangeSphereResponse;
    };
    "equipment/composeManySphere": {
        request: EquipmentComposeManySphereRequest;
        response: EquipmentComposeManySphereResponse;
    };
    "equipment/inheritanceEquipment": {
        request: EquipmentInheritanceEquipmentRequest;
        response: EquipmentInheritanceEquipmentResponse;
    };
    "equipment/reinforcement": {
        request: EquipmentReinforcementRequest;
        response: EquipmentReinforcementResponse;
    };
    "equipment/removeEquipment": {
        request: EquipmentRemoveEquipmentRequest;
        response: EquipmentRemoveEquipmentResponse;
    };
    "equipment/training": {
        request: EquipmentTrainingRequest;
        response: EquipmentTrainingResponse;
    };
    "equipment/unlockSphereSlot": {
        request: EquipmentUnlockSphereSlotRequest;
        response: EquipmentUnlockSphereSlotResponse;
    };
    "equipmentSynchro/rewardMission": {
        request: EquipmentSynchroEquipmentSynchroRewardMissionRequest;
        response: EquipmentSynchroEquipmentSynchroRewardMissionResponse;
    };
    "friend/applyFriend": {
        request: FriendApplyFriendRequest;
        response: FriendApplyFriendResponse;
    };
    "friend/bulkApplyFriends": {
        request: FriendBulkApplyFriendsRequest;
        response: FriendBulkApplyFriendsResponse;
    };
    "friend/bulkTransferFriendPoint": {
        request: FriendBulkTransferFriendPointRequest;
        response: FriendBulkTransferFriendPointResponse;
    };
    "friend/cancelAllApplyFriend": {
        request: FriendCancelAllApplyFriendRequest;
        response: FriendCancelAllApplyFriendResponse;
    };
    "friend/getFriendCampaignInfo": {
        request: FriendGetFriendCampaignInfoRequest;
        response: FriendGetFriendCampaignInfoResponse;
    };
    "friend/getFriendStatus": {
        request: FriendGetFriendStatusRequest;
        response: FriendGetFriendStatusResponse;
    };
    "friend/getPlayerInfoList": {
        request: FriendGetPlayerInfoListRequest;
        response: FriendGetPlayerInfoListResponse;
    };
    "friend/receiveFriendPoint": {
        request: FriendReceiveFriendPointRequest;
        response: FriendReceiveFriendPointResponse;
    };
    "friend/removeFriend": {
        request: FriendRemoveFriendRequest;
        response: FriendRemoveFriendResponse;
    };
    "friend/replyAllFriend": {
        request: FriendReplyAllFriendRequest;
        response: FriendReplyAllFriendResponse;
    };
    "friend/replyFriend": {
        request: FriendReplyFriendRequest;
        response: FriendReplyFriendResponse;
    };
    "friend/rewardFriendMission": {
        request: FriendRewardFriendMissionRequest;
        response: FriendRewardFriendMissionResponse;
    };
    "friend/searchFriend": {
        request: FriendSearchFriendRequest;
        response: FriendSearchFriendResponse;
    };
    "friend/sendFriendPoint": {
        request: FriendSendFriendPointRequest;
        response: FriendSendFriendPointResponse;
    };
    "friend/updateBlockList": {
        request: FriendUpdateBlockListRequest;
        response: FriendUpdateBlockListResponse;
    };
    "friend/useFriendCode": {
        request: FriendUseFriendCodeRequest;
        response: FriendUseFriendCodeResponse;
    };
    "gacha/changeGachaRelic": {
        request: GachaChangeGachaRelicRequest;
        response: GachaChangeGachaRelicResponse;
    };
    "gacha/draw": {
        request: GachaDrawRequest;
        response: GachaDrawResponse;
    };
    "gacha/getList": {
        request: GachaGetListRequest;
        response: GachaGetListResponse;
    };
    "gacha/getLotteryItemList": {
        request: GachaGetLotteryItemListRequest;
        response: GachaGetLotteryItemListResponse;
    };
    "gacha/setSelectList": {
        request: GachaSetSelectListRequest;
        response: GachaSetSelectListResponse;
    };
    "globalGvg/receiveGlobalGvgReward": {
        request: GlobalGvgReceiveGlobalGvgRewardRequest;
        response: GlobalGvgReceiveGlobalGvgRewardResponse;
    };
    "guild/chatRecruitGuildMember": {
        request: GuildChatRecruitGuildMemberRequest;
        response: GuildChatRecruitGuildMemberResponse;
    };
    "guild/searchGuildByRecruitGuildMemberChat": {
        request: GuildSearchGuildByRecruitGuildMemberChatRequest;
        response: GuildSearchGuildByRecruitGuildMemberChatResponse;
    };
    "guildInfo/getGuildBaseInfo": {
        request: GuildGetGuildBaseInfoRequest;
        response: GuildGetGuildBaseInfoResponse;
    };
    "guildInfo/getGuildId": {
        request: GuildGetGuildIdRequest;
        response: GuildGetGuildIdResponse;
    };
    "guildInfo/getGuildMemberInfo": {
        request: GuildGetGuildMemberInfoRequest;
        response: GuildGetGuildMemberInfoResponse;
    };
    "guildNote/createGuildNote": {
        request: GuildNoteCreateGuildNoteRequest;
        response: GuildNoteCreateGuildNoteResponse;
    };
    "guildNote/getGuildNoteList": {
        request: GuildNoteGetGuildNoteListRequest;
        response: GuildNoteGetGuildNoteListResponse;
    };
    "guildNote/getReactionDetailInfo": {
        request: GuildNoteGetReactionDetailInfoRequest;
        response: GuildNoteGetReactionDetailInfoResponse;
    };
    "guildNote/reactGuildNote": {
        request: GuildNoteReactGuildNoteRequest;
        response: GuildNoteReactGuildNoteResponse;
    };
    "guildNote/shareGuildNote": {
        request: GuildNoteShareGuildNoteRequest;
        response: GuildNoteShareGuildNoteResponse;
    };
    "guildNote/updateGuildNote": {
        request: GuildNoteUpdateGuildNoteRequest;
        response: GuildNoteUpdateGuildNoteResponse;
    };
    "guildNote/updateGuildNoteList": {
        request: GuildNoteUpdateGuildNoteListRequest;
        response: GuildNoteUpdateGuildNoteListResponse;
    };
    "guildRaid/getGuildRaidInfo": {
        request: GuildRaidGetGuildRaidInfoRequest;
        response: GuildRaidGetGuildRaidInfoResponse;
    };
    "guildRaid/getGuildRaidWorldRewardInfo": {
        request: GuildRaidGetGuildRaidWorldRewardInfoRequest;
        response: GuildRaidGetGuildRaidWorldRewardInfoResponse;
    };
    "guildRaid/giveGuildRaidWorldRewardItem": {
        request: GuildRaidGiveGuildRaidWorldRewardItemRequest;
        response: GuildRaidGiveGuildRaidWorldRewardItemResponse;
    };
    "guildRaid/openGuildRaid": {
        request: GuildRaidOpenGuildRaidRequest;
        response: GuildRaidOpenGuildRaidResponse;
    };
    "guildRaid/quickStartGuildRaid": {
        request: GuildRaidQuickStartGuildRaidRequest;
        response: GuildRaidQuickStartGuildRaidResponse;
    };
    "guildRaid/startGuildRaid": {
        request: GuildRaidStartGuildRaidRequest;
        response: GuildRaidStartGuildRaidResponse;
    };
    "guildRaid/startGuildRaidTrial": {
        request: GuildRaidStartGuildRaidTrialRequest;
        response: GuildRaidStartGuildRaidTrialResponse;
    };
    "guildTower/battle": {
        request: GuildTowerBattleRequest;
        response: GuildTowerBattleResponse;
    };
    "guildTower/entryCharacter": {
        request: GuildTowerEntryCharacterRequest;
        response: GuildTowerEntryCharacterResponse;
    };
    "guildTower/getGuildTowerInfo": {
        request: GuildTowerGetGuildTowerInfoRequest;
        response: GuildTowerGetGuildTowerInfoResponse;
    };
    "guildTower/getGuildTowerRanking": {
        request: GuildTowerGetGuildTowerRankingRequest;
        response: GuildTowerGetGuildTowerRankingResponse;
    };
    "guildTower/getReinforcementJobData": {
        request: GuildTowerGetReinforcementJobDataRequest;
        response: GuildTowerGetReinforcementJobDataResponse;
    };
    "guildTower/receiveFloorReward": {
        request: GuildTowerReceiveFloorRewardRequest;
        response: GuildTowerReceiveFloorRewardResponse;
    };
    "guildTower/reinforceJob": {
        request: GuildTowerReinforceJobRequest;
        response: GuildTowerReinforceJobResponse;
    };
    "item/getAutoBattleRewardItemInfo": {
        request: ItemGetAutoBattleRewardItemInfoRequest;
        response: ItemGetAutoBattleRewardItemInfoResponse;
    };
    "item/openTreasureChest": {
        request: ItemOpenTreasureChestRequest;
        response: ItemOpenTreasureChestResponse;
    };
    "item/useAutoBattleRewardItem": {
        request: ItemUseAutoBattleRewardItemRequest;
        response: ItemUseAutoBattleRewardItemResponse;
    };
    "legendLeague/buyLegendLeagueIconReward": {
        request: LegendLeagueBuyLegendLeagueIconRewardRequest;
        response: LegendLeagueBuyLegendLeagueIconRewardResponse;
    };
    "legendLeague/getLegendLeagueIconRewardInfo": {
        request: LegendLeagueGetLegendLeagueIconRewardInfoRequest;
        response: LegendLeagueGetLegendLeagueIconRewardInfoResponse;
    };
    "levelLink/addExp": {
        request: LevelLinkAddExpRequest;
        response: LevelLinkAddExpResponse;
    };
    "levelLink/levelUp": {
        request: LevelLinkLevelUpRequest;
        response: LevelLinkLevelUpResponse;
    };
    "levelLink/resetCoolTime": {
        request: LevelLinkResetCoolTimeRequest;
        response: LevelLinkResetCoolTimeResponse;
    };
    "levelLink/setMember": {
        request: LevelLinkSetMemberRequest;
        response: LevelLinkSetMemberResponse;
    };
    "levelLink/sortMember": {
        request: LevelLinkSortMemberRequest;
        response: LevelLinkSortMemberResponse;
    };
    "levelLink/unSetMember": {
        request: LevelLinkUnSetMemberRequest;
        response: LevelLinkUnSetMemberResponse;
    };
    "localGvg/receiveLocalGvgReward": {
        request: LocalGvgReceiveLocalGvgRewardRequest;
        response: LocalGvgReceiveLocalGvgRewardResponse;
    };
    "localRaid/getLocalRaidBattleLogs": {
        request: LocalRaidGetLocalRaidBattleLogsRequest;
        response: LocalRaidGetLocalRaidBattleLogsResponse;
    };
    "localRaid/getLocalRaidBattleResult": {
        request: LocalRaidGetLocalRaidBattleResultRequest;
        response: LocalRaidGetLocalRaidBattleResultResponse;
    };
    "localRaid/getLocalRaidBattleResultOld": {
        request: LocalRaidGetLocalRaidBattleResultOldRequest;
        response: LocalRaidGetLocalRaidBattleResultOldResponse;
    };
    "localRaid/getLocalRaidFriendInfo": {
        request: LocalRaidGetLocalRaidFriendInfoRequest;
        response: LocalRaidGetLocalRaidFriendInfoResponse;
    };
    "localRaid/getLocalRaidInfo": {
        request: LocalRaidGetLocalRaidInfoRequest;
        response: LocalRaidGetLocalRaidInfoResponse;
    };
    "loginBonus/getLimitedLoginBonusInfo": {
        request: LoginBonusGetLimitedLoginBonusInfoRequest;
        response: LoginBonusGetLimitedLoginBonusInfoResponse;
    };
    "loginBonus/getMonthlyLoginBonusInfo": {
        request: LoginBonusGetMonthlyLoginBonusInfoRequest;
        response: LoginBonusGetMonthlyLoginBonusInfoResponse;
    };
    "loginBonus/receiveDailyLimitedLoginBonus": {
        request: LoginBonusReceiveDailyLimitedLoginBonusRequest;
        response: LoginBonusReceiveDailyLimitedLoginBonusResponse;
    };
    "loginBonus/receiveDailyLoginBonus": {
        request: LoginBonusReceiveDailyLoginBonusRequest;
        response: LoginBonusReceiveDailyLoginBonusResponse;
    };
    "loginBonus/receiveLoginCountBonus": {
        request: LoginBonusReceiveLoginCountBonusRequest;
        response: LoginBonusReceiveLoginCountBonusResponse;
    };
    "loginBonus/receiveSpecialLimitedLoginBonus": {
        request: LoginBonusReceiveSpecialLimitedLoginBonusRequest;
        response: LoginBonusReceiveSpecialLimitedLoginBonusResponse;
    };
    "luckyChance/GetDetailInfo": {
        request: LuckyChanceLuckyChanceGetDetailInfoRequest;
        response: LuckyChanceLuckyChanceGetDetailInfoResponse;
    };
    "luckyChance/registerRealPrizeCItemInfo": {
        request: LuckyChanceRegisterRealPrizeCItemInfoRequest;
        response: LuckyChanceRegisterRealPrizeCItemInfoResponse;
    };
    "luckyChance/SelectButton": {
        request: LuckyChanceLuckyChanceSelectButtonRequest;
        response: LuckyChanceLuckyChanceSelectButtonResponse;
    };
    "miningQuest/finish": {
        request: MiningQuestMiningQuestFinishRequest;
        response: MiningQuestMiningQuestFinishResponse;
    };
    "miningQuest/getBenefitInfo": {
        request: MiningQuestMiningQuestGetBenefitInfoRequest;
        response: MiningQuestMiningQuestGetBenefitInfoResponse;
    };
    "miningQuest/getCurrentParameters": {
        request: MiningQuestMiningQuestGetCurrentParametersRequest;
        response: MiningQuestMiningQuestGetCurrentParametersResponse;
    };
    "miningQuest/getGradeInfo": {
        request: MiningQuestMiningQuestGetGradeInfoRequest;
        response: MiningQuestMiningQuestGetGradeInfoResponse;
    };
    "miningQuest/getInfo": {
        request: MiningQuestMiningQuestGetInfoRequest;
        response: MiningQuestMiningQuestGetInfoResponse;
    };
    "miningQuest/getMaxRewardCountInfo": {
        request: MiningQuestMiningQuestGetMaxRewardCountInfoRequest;
        response: MiningQuestMiningQuestGetMaxRewardCountInfoResponse;
    };
    "miningQuest/receiveGradeDailyReward": {
        request: MiningQuestMiningQuestReceiveGradeDailyRewardRequest;
        response: MiningQuestMiningQuestReceiveGradeDailyRewardResponse;
    };
    "miningQuest/reinforcement": {
        request: MiningQuestMiningQuestReinforcementRequest;
        response: MiningQuestMiningQuestReinforcementResponse;
    };
    "miningQuest/start": {
        request: MiningQuestMiningQuestStartRequest;
        response: MiningQuestMiningQuestStartResponse;
    };
    "miningQuest/unlockBenefit": {
        request: MiningQuestMiningQuestUnlockBenefitRequest;
        response: MiningQuestMiningQuestUnlockBenefitResponse;
    };
    "mission/getMissionInfo": {
        request: MissionGetMissionInfoRequest;
        response: MissionGetMissionInfoResponse;
    };
    "mission/receivePanelMissionBingoReward": {
        request: MissionReceivePanelMissionBingoRewardRequest;
        response: MissionReceivePanelMissionBingoRewardResponse;
    };
    "mission/rewardMission": {
        request: MissionRewardMissionRequest;
        response: MissionRewardMissionResponse;
    };
    "mission/rewardMissionActivity": {
        request: MissionRewardMissionActivityRequest;
        response: MissionRewardMissionActivityResponse;
    };
    "notice/getMypageNoticeInfoList": {
        request: NoticeGetMyPageNoticeInfoListRequest;
        response: NoticeGetMyPageNoticeInfoListResponse;
    };
    "notice/getNoticeInfoList": {
        request: NoticeGetNoticeInfoListRequest;
        response: NoticeGetNoticeInfoListResponse;
    };
    "popularityVote/getInfo": {
        request: PopularityVoteGetInfoRequest;
        response: PopularityVoteGetInfoResponse;
    };
    "popularityVote/rewardVoteItem": {
        request: PopularityVoteRewardVoteItemRequest;
        response: PopularityVoteRewardVoteItemResponse;
    };
    "present/deletePresent": {
        request: PresentDeletePresentRequest;
        response: PresentDeletePresentResponse;
    };
    "present/getList": {
        request: PresentGetListRequest;
        response: PresentGetListResponse;
    };
    "present/receiveItem": {
        request: PresentReceiveItemRequest;
        response: PresentReceiveItemResponse;
    };
    "quest/getQuestInfo ": {
        request: QuestGetQuestInfoRequest;
        response: QuestGetQuestInfoResponse;
    };
    "quest/mapInfo": {
        request: QuestMapInfoRequest;
        response: QuestMapInfoResponse;
    };
    "ranking/getAchieveRankingPlayerInfo": {
        request: RankingGetAchieveRankingPlayerInfoRequest;
        response: RankingGetAchieveRankingPlayerInfoResponse;
    };
    "ranking/getBulkAchieveRankingPlayerInfo": {
        request: RankingGetBulkAchieveRankingPlayerInfoRequest;
        response: RankingGetBulkAchieveRankingPlayerInfoResponse;
    };
    "ranking/getGuildRanking": {
        request: RankingGetGuildRankingRequest;
        response: RankingGetGuildRankingResponse;
    };
    "ranking/getPlayerRanking": {
        request: RankingGetPlayerRankingRequest;
        response: RankingGetPlayerRankingResponse;
    };
    "ranking/getRankingBattleLog": {
        request: RankingGetRankingBattleLogRequest;
        response: RankingGetRankingBattleLogResponse;
    };
    "ranking/getTowerRanking": {
        request: RankingGetTowerRankingRequest;
        response: RankingGetTowerRankingResponse;
    };
    "ranking/receiveAchieveRankingReward": {
        request: RankingReceiveAchieveRankingRewardRequest;
        response: RankingReceiveAchieveRankingRewardResponse;
    };
    "rentalRaid/getShareRewardInfo": {
        request: RentalRaidGetShareRewardInfoRequest;
        response: RentalRaidGetShareRewardInfoResponse;
    };
    "rentalRaid/receiveShareReward": {
        request: RentalRaidReceiveShareRewardRequest;
        response: RentalRaidReceiveShareRewardResponse;
    };
    "shop/bulkBuyProductWithWebCrystal": {
        request: ShopBulkBuyProductWithWebCrystalRequest;
        response: ShopBulkBuyProductWithWebCrystalResponse;
    };
    "shop/buyProduct": {
        request: ShopBuyProductRequest;
        response: ShopBuyProductResponse;
    };
    "shop/buyProductWithWebCrystal": {
        request: ShopBuyProductWithWebCrystalRequest;
        response: ShopBuyProductWithWebCrystalResponse;
    };
    "shop/getBoiPaymentDetailHistory": {
        request: ShopGetBoiPaymentDetailHistoryRequest;
        response: ShopGetBoiPaymentDetailHistoryResponse;
    };
    "shop/getBoiPaymentHistoryList": {
        request: ShopGetBoiPaymentHistoryListRequest;
        response: ShopGetBoiPaymentHistoryListResponse;
    };
    "shop/getBoiPointHistoryList": {
        request: ShopGetBoiPointHistoryListRequest;
        response: ShopGetBoiPointHistoryListResponse;
    };
    "shop/getList": {
        request: ShopGetListRequest;
        response: ShopGetListResponse;
    };
    "shop/getProductList": {
        request: ShopGetProductListRequest;
        response: ShopGetProductListResponse;
    };
    "shop/getWebStoreGuidancePanel": {
        request: ShopGetWebStoreGuidancePanelRequest;
        response: ShopGetWebStoreGuidancePanelResponse;
    };
    "shop/receiveReward": {
        request: ShopReceiveRewardRequest;
        response: ShopReceiveRewardResponse;
    };
    "shop/registerActionLog": {
        request: ShopRegisterActionLogRequest;
        response: ShopRegisterActionLogResponse;
    };
    "starsGuidanceTradeShop/exchangeItem": {
        request: StarsGuidanceTradeShopExchangeItemRequest;
        response: StarsGuidanceTradeShopExchangeItemResponse;
    };
    "starsGuidanceTradeShop/getStarsGuidanceTradeShopInfo": {
        request: StarsGuidanceTradeShopGetStarsGuidanceTradeShopInfoRequest;
        response: StarsGuidanceTradeShopGetStarsGuidanceTradeShopInfoResponse;
    };
    "towerBattle/quick": {
        request: TowerBattleTowerBattleQuickRequest;
        response: TowerBattleTowerBattleQuickResponse;
    };
    "towerBattle/start": {
        request: TowerBattleStartRequest;
        response: TowerBattleStartResponse;
    };
    "tradeShop/buyItem": {
        request: TradeShopBuyItemRequest;
        response: TradeShopBuyItemResponse;
    };
    "tradeShop/getList": {
        request: TradeShopGetListRequest;
        response: TradeShopGetListResponse;
    };
    "tradeShop/resetTab": {
        request: TradeShopResetTabRequest;
        response: TradeShopResetTabResponse;
    };
    "unionMission/acceptMission": {
        request: UnionMissionUnionMissionAcceptMissionRequest;
        response: UnionMissionUnionMissionAcceptMissionResponse;
    };
    "unionMission/buyAcceptCount": {
        request: UnionMissionUnionMissionBuyAcceptCountRequest;
        response: UnionMissionUnionMissionBuyAcceptCountResponse;
    };
    "unionMission/entryUnionMission": {
        request: UnionMissionEntryUnionMissionRequest;
        response: UnionMissionEntryUnionMissionResponse;
    };
    "unionMission/getInfo": {
        request: UnionMissionUnionMissionGetInfoRequest;
        response: UnionMissionUnionMissionGetInfoResponse;
    };
    "unionMission/getRanking": {
        request: UnionMissionUnionMissionGetRankingRequest;
        response: UnionMissionUnionMissionGetRankingResponse;
    };
    "unionMission/receiveIndividualTotalPointReward": {
        request: UnionMissionUnionMissionReceiveIndividualTotalPointRewardRequest;
        response: UnionMissionUnionMissionReceiveIndividualTotalPointRewardResponse;
    };
    "unionMission/receiveMissionReward": {
        request: UnionMissionUnionMissionReceiveMissionRewardRequest;
        response: UnionMissionUnionMissionReceiveMissionRewardResponse;
    };
    "unionMission/receiveTotalPointReward": {
        request: UnionMissionUnionMissionReceiveTotalPointRewardRequest;
        response: UnionMissionUnionMissionReceiveTotalPointRewardResponse;
    };
    "unionMission/refreshPersonalMission": {
        request: UnionMissionUnionMissionRefreshPersonalMissionRequest;
        response: UnionMissionUnionMissionRefreshPersonalMissionResponse;
    };
    "unionMission/refreshSharedMission": {
        request: UnionMissionUnionMissionRefreshSharedMissionRequest;
        response: UnionMissionUnionMissionRefreshSharedMissionResponse;
    };
    "unionMission/resetCoolTimeMission": {
        request: UnionMissionUnionMissionResetCoolTimeMissionRequest;
        response: UnionMissionUnionMissionResetCoolTimeMissionResponse;
    };
    "unionMission/retireMission": {
        request: UnionMissionUnionMissionRetireMissionRequest;
        response: UnionMissionUnionMissionRetireMissionResponse;
    };
    "unionMission/selectMission": {
        request: UnionMissionUnionMissionSelectMissionRequest;
        response: UnionMissionUnionMissionSelectMissionResponse;
    };
    "user/getMypage": {
        request: UserGetMypageRequest;
        response: UserGetMypageResponse;
    };
    "user/getUserData": {
        request: UserGetUserDataRequest;
        response: UserGetUserDataResponse;
    };
    "user/loginPlayer": {
        request: UserLoginPlayerRequest;
        response: UserLoginPlayerResponse;
    };
    "user/saveUsedDeck": {
        request: UserSaveUsedDeckRequest;
        response: UserSaveUsedDeckResponse;
    };
    "vip/getDailyGift": {
        request: VipGetDailyGiftRequest;
        response: VipGetDailyGiftResponse;
    };
    "webStore/getLoginCampaignInfo": {
        request: WebStoreLoginCampaignGetLoginCampaignInfoRequest;
        response: WebStoreLoginCampaignGetLoginCampaignInfoResponse;
    };
    "webStore/rewardLoginCampaignItem": {
        request: WebStoreLoginCampaignRewardLoginCampaignItemRequest;
        response: WebStoreLoginCampaignRewardLoginCampaignItemResponse;
    };
    "weeklyTopics/getWeeklyTopicsInfo": {
        request: WeeklyTopicsGetWeeklyTopicsInfoRequest;
        response: WeeklyTopicsGetWeeklyTopicsInfoResponse;
    };
}
