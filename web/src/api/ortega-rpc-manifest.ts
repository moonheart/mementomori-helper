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
    BattleGetPvpInfoRequest,
    BattleGetPvpInfoResponse,
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
    BountyQuestRemakeRequest,
    BountyQuestRemakeResponse,
    BountyQuestRewardRequest,
    BountyQuestRewardResponse,
    BountyQuestStartRequest,
    BountyQuestStartResponse,
    CharacterGetCharacterStoryRewardRequest,
    CharacterGetCharacterStoryRewardResponse,
    CharacterGetDetailsInfoRequest,
    CharacterGetDetailsInfoResponse,
    CharacterRankUpRequest,
    CharacterRankUpResponse,
    CharacterReadCharacterStoryRequest,
    CharacterReadCharacterStoryResponse,
    DebugGiveAllSrCharacterRequest,
    DebugGiveAllSrCharacterResponse,
    DebugSetVipLevelRequest,
    DebugSetVipLevelResponse,
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
    EquipmentInheritanceEquipmentRequest,
    EquipmentInheritanceEquipmentResponse,
    EquipmentReinforcementRequest,
    EquipmentReinforcementResponse,
    EquipmentRemoveEquipmentRequest,
    EquipmentRemoveEquipmentResponse,
    EquipmentTrainingRequest,
    EquipmentTrainingResponse,
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
    GuildGetGuildBaseInfoRequest,
    GuildGetGuildBaseInfoResponse,
    GuildGetGuildIdRequest,
    GuildGetGuildIdResponse,
    GuildGetGuildMemberInfoRequest,
    GuildGetGuildMemberInfoResponse,
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
    ShopBuyProductRequest,
    ShopBuyProductResponse,
    ShopGetListRequest,
    ShopGetListResponse,
    ShopGetProductListRequest,
    ShopGetProductListResponse,
    ShopReceiveRewardRequest,
    ShopReceiveRewardResponse,
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
    UserGetMypageRequest,
    UserGetMypageResponse,
    UserGetUserDataRequest,
    UserGetUserDataResponse,
    UserLoginPlayerRequest,
    UserLoginPlayerResponse,
    VipGetDailyGiftRequest,
    VipGetDailyGiftResponse,
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
    "battle/getPvpInfo": {
        request: BattleGetPvpInfoRequest;
        response: BattleGetPvpInfoResponse;
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
    "character/getCharacterStoryReward": {
        request: CharacterGetCharacterStoryRewardRequest;
        response: CharacterGetCharacterStoryRewardResponse;
    };
    "character/getDetailsInfo": {
        request: CharacterGetDetailsInfoRequest;
        response: CharacterGetDetailsInfoResponse;
    };
    "character/rankUp": {
        request: CharacterRankUpRequest;
        response: CharacterRankUpResponse;
    };
    "character/readCharacterStory": {
        request: CharacterReadCharacterStoryRequest;
        response: CharacterReadCharacterStoryResponse;
    };
    "debug/giveAllSrCharacter": {
        request: DebugGiveAllSrCharacterRequest;
        response: DebugGiveAllSrCharacterResponse;
    };
    "debug/setVipLevel": {
        request: DebugSetVipLevelRequest;
        response: DebugSetVipLevelResponse;
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
    "shop/buyProduct": {
        request: ShopBuyProductRequest;
        response: ShopBuyProductResponse;
    };
    "shop/getList": {
        request: ShopGetListRequest;
        response: ShopGetListResponse;
    };
    "shop/getProductList": {
        request: ShopGetProductListRequest;
        response: ShopGetProductListResponse;
    };
    "shop/receiveReward": {
        request: ShopReceiveRewardRequest;
        response: ShopReceiveRewardResponse;
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
    "vip/getDailyGift": {
        request: VipGetDailyGiftRequest;
        response: VipGetDailyGiftResponse;
    };
    "weeklyTopics/getWeeklyTopicsInfo": {
        request: WeeklyTopicsGetWeeklyTopicsInfoRequest;
        response: WeeklyTopicsGetWeeklyTopicsInfoResponse;
    };
}
