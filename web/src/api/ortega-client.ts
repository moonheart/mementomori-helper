// @ts-nocheck
// 该文件由 C# 后端自动生成，包含运行时辅助对象，请勿手动修改
/* eslint-disable */

import rpcClient from './rpc-client';
import type { OrtegaRpcMap } from './ortega-rpc-manifest';

export const ortegaApi = {
    auth: {
        /** auth/comebackUser */
        comebackUser: (request: OrtegaRpcMap["auth/comebackUser"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["auth/comebackUser"]["response"]>("auth/comebackUser", request),
        /** auth/createAccessToken */
        createAccessToken: (request: OrtegaRpcMap["auth/createAccessToken"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["auth/createAccessToken"]["response"]>("auth/createAccessToken", request),
        /** auth/createUser */
        createUser: (request: OrtegaRpcMap["auth/createUser"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["auth/createUser"]["response"]>("auth/createUser", request),
        /** auth/createWorldPlayer */
        createWorldPlayer: (request: OrtegaRpcMap["auth/createWorldPlayer"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["auth/createWorldPlayer"]["response"]>("auth/createWorldPlayer", request),
        /** auth/getComebackUserData */
        getComebackUserData: (request: OrtegaRpcMap["auth/getComebackUserData"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["auth/getComebackUserData"]["response"]>("auth/getComebackUserData", request),
        /** auth/getDataUri */
        getDataUri: (request: OrtegaRpcMap["auth/getDataUri"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["auth/getDataUri"]["response"]>("auth/getDataUri", request),
        /** auth/getServerHost */
        getServerHost: (request: OrtegaRpcMap["auth/getServerHost"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["auth/getServerHost"]["response"]>("auth/getServerHost", request),
        /** auth/login */
        login: (request: OrtegaRpcMap["auth/login"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["auth/login"]["response"]>("auth/login", request),
    },
    badge: {
        /** badge/getCompetitionInfo */
        getCompetitionInfo: (request: OrtegaRpcMap["badge/getCompetitionInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["badge/getCompetitionInfo"]["response"]>("badge/getCompetitionInfo", request),
    },
    battle: {
        /** battle/auto */
        auto: (request: OrtegaRpcMap["battle/auto"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/auto"]["response"]>("battle/auto", request),
        /** battle/boss */
        boss: (request: OrtegaRpcMap["battle/boss"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/boss"]["response"]>("battle/boss", request),
        /** battle/bossQuick */
        bossQuick: (request: OrtegaRpcMap["battle/bossQuick"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/bossQuick"]["response"]>("battle/bossQuick", request),
        /** battle/bossRewardInfo */
        bossRewardInfo: (request: OrtegaRpcMap["battle/bossRewardInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/bossRewardInfo"]["response"]>("battle/bossRewardInfo", request),
        /** battle/getLegendLeagueInfo */
        getLegendLeagueInfo: (request: OrtegaRpcMap["battle/getLegendLeagueInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/getLegendLeagueInfo"]["response"]>("battle/getLegendLeagueInfo", request),
        /** battle/getLegendLeagueLogs */
        getLegendLeagueLogs: (request: OrtegaRpcMap["battle/getLegendLeagueLogs"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/getLegendLeagueLogs"]["response"]>("battle/getLegendLeagueLogs", request),
        /** battle/getLegendLeagueResultDetail */
        getLegendLeagueResultDetail: (request: OrtegaRpcMap["battle/getLegendLeagueResultDetail"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/getLegendLeagueResultDetail"]["response"]>("battle/getLegendLeagueResultDetail", request),
        /** battle/getLegendLeagueTopRanker */
        getLegendLeagueTopRanker: (request: OrtegaRpcMap["battle/getLegendLeagueTopRanker"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/getLegendLeagueTopRanker"]["response"]>("battle/getLegendLeagueTopRanker", request),
        /** battle/getPvpBattleLogs */
        getPvpBattleLogs: (request: OrtegaRpcMap["battle/getPvpBattleLogs"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/getPvpBattleLogs"]["response"]>("battle/getPvpBattleLogs", request),
        /** battle/getPvpBattleResultDetail */
        getPvpBattleResultDetail: (request: OrtegaRpcMap["battle/getPvpBattleResultDetail"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/getPvpBattleResultDetail"]["response"]>("battle/getPvpBattleResultDetail", request),
        /** battle/getPvpInfo */
        getPvpInfo: (request: OrtegaRpcMap["battle/getPvpInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/getPvpInfo"]["response"]>("battle/getPvpInfo", request),
        /** battle/getPvpTopRanker */
        getPvpTopRanker: (request: OrtegaRpcMap["battle/getPvpTopRanker"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/getPvpTopRanker"]["response"]>("battle/getPvpTopRanker", request),
        /** battle/legendLeagueStart */
        legendLeagueStart: (request: OrtegaRpcMap["battle/legendLeagueStart"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/legendLeagueStart"]["response"]>("battle/legendLeagueStart", request),
        /** battle/nextQuest */
        nextQuest: (request: OrtegaRpcMap["battle/nextQuest"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/nextQuest"]["response"]>("battle/nextQuest", request),
        /** battle/pvpStart */
        pvpStart: (request: OrtegaRpcMap["battle/pvpStart"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/pvpStart"]["response"]>("battle/pvpStart", request),
        /** battle/quick */
        quick: (request: OrtegaRpcMap["battle/quick"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/quick"]["response"]>("battle/quick", request),
        /** battle/rewardAutoBattle */
        rewardAutoBattle: (request: OrtegaRpcMap["battle/rewardAutoBattle"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/rewardAutoBattle"]["response"]>("battle/rewardAutoBattle", request),
    },
    bookSort: {
        /** bookSort/bulkUnlockGridCell */
        bulkUnlockGridCell: (request: OrtegaRpcMap["bookSort/bulkUnlockGridCell"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["bookSort/bulkUnlockGridCell"]["response"]>("bookSort/bulkUnlockGridCell", request),
        /** bookSort/getInfo */
        getInfo: (request: OrtegaRpcMap["bookSort/getInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["bookSort/getInfo"]["response"]>("bookSort/getInfo", request),
        /** bookSort/getRewardInfo */
        getRewardInfo: (request: OrtegaRpcMap["bookSort/getRewardInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["bookSort/getRewardInfo"]["response"]>("bookSort/getRewardInfo", request),
        /** bookSort/selectBonusFloorReward */
        selectBonusFloorReward: (request: OrtegaRpcMap["bookSort/selectBonusFloorReward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["bookSort/selectBonusFloorReward"]["response"]>("bookSort/selectBonusFloorReward", request),
        /** bookSort/unlockGridCell */
        unlockGridCell: (request: OrtegaRpcMap["bookSort/unlockGridCell"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["bookSort/unlockGridCell"]["response"]>("bookSort/unlockGridCell", request),
        /** bookSort/upFloor */
        upFloor: (request: OrtegaRpcMap["bookSort/upFloor"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["bookSort/upFloor"]["response"]>("bookSort/upFloor", request),
    },
    bookSortAssistance: {
        /** bookSortAssistance/addAssistance */
        addAssistance: (request: OrtegaRpcMap["bookSortAssistance/addAssistance"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["bookSortAssistance/addAssistance"]["response"]>("bookSortAssistance/addAssistance", request),
        /** bookSortAssistance/dispatch */
        dispatch: (request: OrtegaRpcMap["bookSortAssistance/dispatch"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["bookSortAssistance/dispatch"]["response"]>("bookSortAssistance/dispatch", request),
        /** bookSortAssistance/getInfo */
        getInfo: (request: OrtegaRpcMap["bookSortAssistance/getInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["bookSortAssistance/getInfo"]["response"]>("bookSortAssistance/getInfo", request),
        /** bookSortAssistance/reward */
        reward: (request: OrtegaRpcMap["bookSortAssistance/reward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["bookSortAssistance/reward"]["response"]>("bookSortAssistance/reward", request),
    },
    bountyQuest: {
        /** bountyQuest/getList */
        getList: (request: OrtegaRpcMap["bountyQuest/getList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["bountyQuest/getList"]["response"]>("bountyQuest/getList", request),
        /** bountyQuest/quick */
        quick: (request: OrtegaRpcMap["bountyQuest/quick"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["bountyQuest/quick"]["response"]>("bountyQuest/quick", request),
        /** bountyQuest/remake */
        remake: (request: OrtegaRpcMap["bountyQuest/remake"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["bountyQuest/remake"]["response"]>("bountyQuest/remake", request),
        /** bountyQuest/reward */
        reward: (request: OrtegaRpcMap["bountyQuest/reward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["bountyQuest/reward"]["response"]>("bountyQuest/reward", request),
        /** bountyQuest/start */
        start: (request: OrtegaRpcMap["bountyQuest/start"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["bountyQuest/start"]["response"]>("bountyQuest/start", request),
    },
    character: {
        /** character/bulkLevelUp */
        bulkLevelUp: (request: OrtegaRpcMap["character/bulkLevelUp"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["character/bulkLevelUp"]["response"]>("character/bulkLevelUp", request),
        /** character/getCharacterStoryReward */
        getCharacterStoryReward: (request: OrtegaRpcMap["character/getCharacterStoryReward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["character/getCharacterStoryReward"]["response"]>("character/getCharacterStoryReward", request),
        /** character/getDetailsInfo */
        getDetailsInfo: (request: OrtegaRpcMap["character/getDetailsInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["character/getDetailsInfo"]["response"]>("character/getDetailsInfo", request),
        /** character/levelUp */
        levelUp: (request: OrtegaRpcMap["character/levelUp"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["character/levelUp"]["response"]>("character/levelUp", request),
        /** character/rankUp */
        rankUp: (request: OrtegaRpcMap["character/rankUp"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["character/rankUp"]["response"]>("character/rankUp", request),
        /** character/readCharacterStory */
        readCharacterStory: (request: OrtegaRpcMap["character/readCharacterStory"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["character/readCharacterStory"]["response"]>("character/readCharacterStory", request),
    },
    debug: {
        /** debug/addMusicPlayLog */
        addMusicPlayLog: (request: OrtegaRpcMap["debug/addMusicPlayLog"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["debug/addMusicPlayLog"]["response"]>("debug/addMusicPlayLog", request),
        /** debug/giveAllSrCharacter */
        giveAllSrCharacter: (request: OrtegaRpcMap["debug/giveAllSrCharacter"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["debug/giveAllSrCharacter"]["response"]>("debug/giveAllSrCharacter", request),
        /** debug/setMiningQuestBenefitPurchaseCount */
        setMiningQuestBenefitPurchaseCount: (request: OrtegaRpcMap["debug/setMiningQuestBenefitPurchaseCount"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["debug/setMiningQuestBenefitPurchaseCount"]["response"]>("debug/setMiningQuestBenefitPurchaseCount", request),
        /** debug/setMiningQuestMaxScore */
        setMiningQuestMaxScore: (request: OrtegaRpcMap["debug/setMiningQuestMaxScore"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["debug/setMiningQuestMaxScore"]["response"]>("debug/setMiningQuestMaxScore", request),
        /** debug/setMiningQuestTotalRewardCount */
        setMiningQuestTotalRewardCount: (request: OrtegaRpcMap["debug/setMiningQuestTotalRewardCount"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["debug/setMiningQuestTotalRewardCount"]["response"]>("debug/setMiningQuestTotalRewardCount", request),
        /** debug/setVipLevel */
        setVipLevel: (request: OrtegaRpcMap["debug/setVipLevel"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["debug/setVipLevel"]["response"]>("debug/setVipLevel", request),
        /** debug/unionMissionResetAcceptCount */
        unionMissionResetAcceptCount: (request: OrtegaRpcMap["debug/unionMissionResetAcceptCount"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["debug/unionMissionResetAcceptCount"]["response"]>("debug/unionMissionResetAcceptCount", request),
    },
    dungeonBattle: {
        /** dungeonBattle/execBattle */
        execBattle: (request: OrtegaRpcMap["dungeonBattle/execBattle"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["dungeonBattle/execBattle"]["response"]>("dungeonBattle/execBattle", request),
        /** dungeonBattle/execGuest */
        execGuest: (request: OrtegaRpcMap["dungeonBattle/execGuest"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["dungeonBattle/execGuest"]["response"]>("dungeonBattle/execGuest", request),
        /** dungeonBattle/execRecovery */
        execRecovery: (request: OrtegaRpcMap["dungeonBattle/execRecovery"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["dungeonBattle/execRecovery"]["response"]>("dungeonBattle/execRecovery", request),
        /** dungeonBattle/execReinforceRelic */
        execReinforceRelic: (request: OrtegaRpcMap["dungeonBattle/execReinforceRelic"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["dungeonBattle/execReinforceRelic"]["response"]>("dungeonBattle/execReinforceRelic", request),
        /** dungeonBattle/execRevive */
        execRevive: (request: OrtegaRpcMap["dungeonBattle/execRevive"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["dungeonBattle/execRevive"]["response"]>("dungeonBattle/execRevive", request),
        /** dungeonBattle/execShop */
        execShop: (request: OrtegaRpcMap["dungeonBattle/execShop"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["dungeonBattle/execShop"]["response"]>("dungeonBattle/execShop", request),
        /** dungeonBattle/finishBattle */
        finishBattle: (request: OrtegaRpcMap["dungeonBattle/finishBattle"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["dungeonBattle/finishBattle"]["response"]>("dungeonBattle/finishBattle", request),
        /** dungeonBattle/getBattleGridData */
        getBattleGridData: (request: OrtegaRpcMap["dungeonBattle/getBattleGridData"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["dungeonBattle/getBattleGridData"]["response"]>("dungeonBattle/getBattleGridData", request),
        /** dungeonBattle/getDungeonBattleInfo */
        getDungeonBattleInfo: (request: OrtegaRpcMap["dungeonBattle/getDungeonBattleInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["dungeonBattle/getDungeonBattleInfo"]["response"]>("dungeonBattle/getDungeonBattleInfo", request),
        /** dungeonBattle/leaveShop */
        leaveShop: (request: OrtegaRpcMap["dungeonBattle/leaveShop"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["dungeonBattle/leaveShop"]["response"]>("dungeonBattle/leaveShop", request),
        /** dungeonBattle/proceedLayer */
        proceedLayer: (request: OrtegaRpcMap["dungeonBattle/proceedLayer"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["dungeonBattle/proceedLayer"]["response"]>("dungeonBattle/proceedLayer", request),
        /** dungeonBattle/rewardBattleReceiveRelic */
        rewardBattleReceiveRelic: (request: OrtegaRpcMap["dungeonBattle/rewardBattleReceiveRelic"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["dungeonBattle/rewardBattleReceiveRelic"]["response"]>("dungeonBattle/rewardBattleReceiveRelic", request),
        /** dungeonBattle/rewardBattleReinforceRelic */
        rewardBattleReinforceRelic: (request: OrtegaRpcMap["dungeonBattle/rewardBattleReinforceRelic"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["dungeonBattle/rewardBattleReinforceRelic"]["response"]>("dungeonBattle/rewardBattleReinforceRelic", request),
        /** dungeonBattle/rewardClearLayer */
        rewardClearLayer: (request: OrtegaRpcMap["dungeonBattle/rewardClearLayer"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["dungeonBattle/rewardClearLayer"]["response"]>("dungeonBattle/rewardClearLayer", request),
        /** dungeonBattle/selectGrid */
        selectGrid: (request: OrtegaRpcMap["dungeonBattle/selectGrid"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["dungeonBattle/selectGrid"]["response"]>("dungeonBattle/selectGrid", request),
        /** dungeonBattle/skipBattle */
        skipBattle: (request: OrtegaRpcMap["dungeonBattle/skipBattle"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["dungeonBattle/skipBattle"]["response"]>("dungeonBattle/skipBattle", request),
        /** dungeonBattle/useRecoveryItem */
        useRecoveryItem: (request: OrtegaRpcMap["dungeonBattle/useRecoveryItem"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["dungeonBattle/useRecoveryItem"]["response"]>("dungeonBattle/useRecoveryItem", request),
    },
    equipment: {
        /** equipment/cast */
        cast: (request: OrtegaRpcMap["equipment/cast"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["equipment/cast"]["response"]>("equipment/cast", request),
        /** equipment/castMany */
        castMany: (request: OrtegaRpcMap["equipment/castMany"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["equipment/castMany"]["response"]>("equipment/castMany", request),
        /** equipment/changeEquipment */
        changeEquipment: (request: OrtegaRpcMap["equipment/changeEquipment"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["equipment/changeEquipment"]["response"]>("equipment/changeEquipment", request),
        /** equipment/changeSphere */
        changeSphere: (request: OrtegaRpcMap["equipment/changeSphere"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["equipment/changeSphere"]["response"]>("equipment/changeSphere", request),
        /** equipment/composeManySphere */
        composeManySphere: (request: OrtegaRpcMap["equipment/composeManySphere"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["equipment/composeManySphere"]["response"]>("equipment/composeManySphere", request),
        /** equipment/inheritanceEquipment */
        inheritanceEquipment: (request: OrtegaRpcMap["equipment/inheritanceEquipment"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["equipment/inheritanceEquipment"]["response"]>("equipment/inheritanceEquipment", request),
        /** equipment/reinforcement */
        reinforcement: (request: OrtegaRpcMap["equipment/reinforcement"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["equipment/reinforcement"]["response"]>("equipment/reinforcement", request),
        /** equipment/removeEquipment */
        removeEquipment: (request: OrtegaRpcMap["equipment/removeEquipment"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["equipment/removeEquipment"]["response"]>("equipment/removeEquipment", request),
        /** equipment/training */
        training: (request: OrtegaRpcMap["equipment/training"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["equipment/training"]["response"]>("equipment/training", request),
        /** equipment/unlockSphereSlot */
        unlockSphereSlot: (request: OrtegaRpcMap["equipment/unlockSphereSlot"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["equipment/unlockSphereSlot"]["response"]>("equipment/unlockSphereSlot", request),
    },
    equipmentSynchro: {
        /** equipmentSynchro/rewardMission */
        rewardMission: (request: OrtegaRpcMap["equipmentSynchro/rewardMission"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["equipmentSynchro/rewardMission"]["response"]>("equipmentSynchro/rewardMission", request),
    },
    friend: {
        /** friend/applyFriend */
        applyFriend: (request: OrtegaRpcMap["friend/applyFriend"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["friend/applyFriend"]["response"]>("friend/applyFriend", request),
        /** friend/bulkApplyFriends */
        bulkApplyFriends: (request: OrtegaRpcMap["friend/bulkApplyFriends"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["friend/bulkApplyFriends"]["response"]>("friend/bulkApplyFriends", request),
        /** friend/bulkTransferFriendPoint */
        bulkTransferFriendPoint: (request: OrtegaRpcMap["friend/bulkTransferFriendPoint"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["friend/bulkTransferFriendPoint"]["response"]>("friend/bulkTransferFriendPoint", request),
        /** friend/cancelAllApplyFriend */
        cancelAllApplyFriend: (request: OrtegaRpcMap["friend/cancelAllApplyFriend"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["friend/cancelAllApplyFriend"]["response"]>("friend/cancelAllApplyFriend", request),
        /** friend/getFriendCampaignInfo */
        getFriendCampaignInfo: (request: OrtegaRpcMap["friend/getFriendCampaignInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["friend/getFriendCampaignInfo"]["response"]>("friend/getFriendCampaignInfo", request),
        /** friend/getFriendStatus */
        getFriendStatus: (request: OrtegaRpcMap["friend/getFriendStatus"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["friend/getFriendStatus"]["response"]>("friend/getFriendStatus", request),
        /** friend/getPlayerInfoList */
        getPlayerInfoList: (request: OrtegaRpcMap["friend/getPlayerInfoList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["friend/getPlayerInfoList"]["response"]>("friend/getPlayerInfoList", request),
        /** friend/receiveFriendPoint */
        receiveFriendPoint: (request: OrtegaRpcMap["friend/receiveFriendPoint"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["friend/receiveFriendPoint"]["response"]>("friend/receiveFriendPoint", request),
        /** friend/removeFriend */
        removeFriend: (request: OrtegaRpcMap["friend/removeFriend"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["friend/removeFriend"]["response"]>("friend/removeFriend", request),
        /** friend/replyAllFriend */
        replyAllFriend: (request: OrtegaRpcMap["friend/replyAllFriend"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["friend/replyAllFriend"]["response"]>("friend/replyAllFriend", request),
        /** friend/replyFriend */
        replyFriend: (request: OrtegaRpcMap["friend/replyFriend"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["friend/replyFriend"]["response"]>("friend/replyFriend", request),
        /** friend/rewardFriendMission */
        rewardFriendMission: (request: OrtegaRpcMap["friend/rewardFriendMission"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["friend/rewardFriendMission"]["response"]>("friend/rewardFriendMission", request),
        /** friend/searchFriend */
        searchFriend: (request: OrtegaRpcMap["friend/searchFriend"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["friend/searchFriend"]["response"]>("friend/searchFriend", request),
        /** friend/sendFriendPoint */
        sendFriendPoint: (request: OrtegaRpcMap["friend/sendFriendPoint"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["friend/sendFriendPoint"]["response"]>("friend/sendFriendPoint", request),
        /** friend/updateBlockList */
        updateBlockList: (request: OrtegaRpcMap["friend/updateBlockList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["friend/updateBlockList"]["response"]>("friend/updateBlockList", request),
        /** friend/useFriendCode */
        useFriendCode: (request: OrtegaRpcMap["friend/useFriendCode"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["friend/useFriendCode"]["response"]>("friend/useFriendCode", request),
    },
    gacha: {
        /** gacha/changeGachaRelic */
        changeGachaRelic: (request: OrtegaRpcMap["gacha/changeGachaRelic"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["gacha/changeGachaRelic"]["response"]>("gacha/changeGachaRelic", request),
        /** gacha/draw */
        draw: (request: OrtegaRpcMap["gacha/draw"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["gacha/draw"]["response"]>("gacha/draw", request),
        /** gacha/getList */
        getList: (request: OrtegaRpcMap["gacha/getList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["gacha/getList"]["response"]>("gacha/getList", request),
        /** gacha/getLotteryItemList */
        getLotteryItemList: (request: OrtegaRpcMap["gacha/getLotteryItemList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["gacha/getLotteryItemList"]["response"]>("gacha/getLotteryItemList", request),
        /** gacha/setSelectList */
        setSelectList: (request: OrtegaRpcMap["gacha/setSelectList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["gacha/setSelectList"]["response"]>("gacha/setSelectList", request),
    },
    globalGvg: {
        /** globalGvg/receiveGlobalGvgReward */
        receiveGlobalGvgReward: (request: OrtegaRpcMap["globalGvg/receiveGlobalGvgReward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["globalGvg/receiveGlobalGvgReward"]["response"]>("globalGvg/receiveGlobalGvgReward", request),
    },
    guild: {
        /** guild/chatRecruitGuildMember */
        chatRecruitGuildMember: (request: OrtegaRpcMap["guild/chatRecruitGuildMember"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guild/chatRecruitGuildMember"]["response"]>("guild/chatRecruitGuildMember", request),
        /** guild/searchGuildByRecruitGuildMemberChat */
        searchGuildByRecruitGuildMemberChat: (request: OrtegaRpcMap["guild/searchGuildByRecruitGuildMemberChat"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guild/searchGuildByRecruitGuildMemberChat"]["response"]>("guild/searchGuildByRecruitGuildMemberChat", request),
    },
    guildInfo: {
        /** guildInfo/getGuildBaseInfo */
        getGuildBaseInfo: (request: OrtegaRpcMap["guildInfo/getGuildBaseInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildInfo/getGuildBaseInfo"]["response"]>("guildInfo/getGuildBaseInfo", request),
        /** guildInfo/getGuildId */
        getGuildId: (request: OrtegaRpcMap["guildInfo/getGuildId"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildInfo/getGuildId"]["response"]>("guildInfo/getGuildId", request),
        /** guildInfo/getGuildMemberInfo */
        getGuildMemberInfo: (request: OrtegaRpcMap["guildInfo/getGuildMemberInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildInfo/getGuildMemberInfo"]["response"]>("guildInfo/getGuildMemberInfo", request),
    },
    guildNote: {
        /** guildNote/createGuildNote */
        createGuildNote: (request: OrtegaRpcMap["guildNote/createGuildNote"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildNote/createGuildNote"]["response"]>("guildNote/createGuildNote", request),
        /** guildNote/getGuildNoteList */
        getGuildNoteList: (request: OrtegaRpcMap["guildNote/getGuildNoteList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildNote/getGuildNoteList"]["response"]>("guildNote/getGuildNoteList", request),
        /** guildNote/getReactionDetailInfo */
        getReactionDetailInfo: (request: OrtegaRpcMap["guildNote/getReactionDetailInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildNote/getReactionDetailInfo"]["response"]>("guildNote/getReactionDetailInfo", request),
        /** guildNote/reactGuildNote */
        reactGuildNote: (request: OrtegaRpcMap["guildNote/reactGuildNote"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildNote/reactGuildNote"]["response"]>("guildNote/reactGuildNote", request),
        /** guildNote/shareGuildNote */
        shareGuildNote: (request: OrtegaRpcMap["guildNote/shareGuildNote"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildNote/shareGuildNote"]["response"]>("guildNote/shareGuildNote", request),
        /** guildNote/updateGuildNote */
        updateGuildNote: (request: OrtegaRpcMap["guildNote/updateGuildNote"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildNote/updateGuildNote"]["response"]>("guildNote/updateGuildNote", request),
        /** guildNote/updateGuildNoteList */
        updateGuildNoteList: (request: OrtegaRpcMap["guildNote/updateGuildNoteList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildNote/updateGuildNoteList"]["response"]>("guildNote/updateGuildNoteList", request),
    },
    guildRaid: {
        /** guildRaid/getGuildRaidInfo */
        getGuildRaidInfo: (request: OrtegaRpcMap["guildRaid/getGuildRaidInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildRaid/getGuildRaidInfo"]["response"]>("guildRaid/getGuildRaidInfo", request),
        /** guildRaid/getGuildRaidWorldRewardInfo */
        getGuildRaidWorldRewardInfo: (request: OrtegaRpcMap["guildRaid/getGuildRaidWorldRewardInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildRaid/getGuildRaidWorldRewardInfo"]["response"]>("guildRaid/getGuildRaidWorldRewardInfo", request),
        /** guildRaid/giveGuildRaidWorldRewardItem */
        giveGuildRaidWorldRewardItem: (request: OrtegaRpcMap["guildRaid/giveGuildRaidWorldRewardItem"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildRaid/giveGuildRaidWorldRewardItem"]["response"]>("guildRaid/giveGuildRaidWorldRewardItem", request),
        /** guildRaid/openGuildRaid */
        openGuildRaid: (request: OrtegaRpcMap["guildRaid/openGuildRaid"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildRaid/openGuildRaid"]["response"]>("guildRaid/openGuildRaid", request),
        /** guildRaid/quickStartGuildRaid */
        quickStartGuildRaid: (request: OrtegaRpcMap["guildRaid/quickStartGuildRaid"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildRaid/quickStartGuildRaid"]["response"]>("guildRaid/quickStartGuildRaid", request),
        /** guildRaid/startGuildRaid */
        startGuildRaid: (request: OrtegaRpcMap["guildRaid/startGuildRaid"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildRaid/startGuildRaid"]["response"]>("guildRaid/startGuildRaid", request),
        /** guildRaid/startGuildRaidTrial */
        startGuildRaidTrial: (request: OrtegaRpcMap["guildRaid/startGuildRaidTrial"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildRaid/startGuildRaidTrial"]["response"]>("guildRaid/startGuildRaidTrial", request),
    },
    guildTower: {
        /** guildTower/battle */
        battle: (request: OrtegaRpcMap["guildTower/battle"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildTower/battle"]["response"]>("guildTower/battle", request),
        /** guildTower/entryCharacter */
        entryCharacter: (request: OrtegaRpcMap["guildTower/entryCharacter"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildTower/entryCharacter"]["response"]>("guildTower/entryCharacter", request),
        /** guildTower/getGuildTowerInfo */
        getGuildTowerInfo: (request: OrtegaRpcMap["guildTower/getGuildTowerInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildTower/getGuildTowerInfo"]["response"]>("guildTower/getGuildTowerInfo", request),
        /** guildTower/getGuildTowerRanking */
        getGuildTowerRanking: (request: OrtegaRpcMap["guildTower/getGuildTowerRanking"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildTower/getGuildTowerRanking"]["response"]>("guildTower/getGuildTowerRanking", request),
        /** guildTower/getReinforcementJobData */
        getReinforcementJobData: (request: OrtegaRpcMap["guildTower/getReinforcementJobData"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildTower/getReinforcementJobData"]["response"]>("guildTower/getReinforcementJobData", request),
        /** guildTower/receiveFloorReward */
        receiveFloorReward: (request: OrtegaRpcMap["guildTower/receiveFloorReward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildTower/receiveFloorReward"]["response"]>("guildTower/receiveFloorReward", request),
        /** guildTower/reinforceJob */
        reinforceJob: (request: OrtegaRpcMap["guildTower/reinforceJob"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["guildTower/reinforceJob"]["response"]>("guildTower/reinforceJob", request),
    },
    item: {
        /** item/getAutoBattleRewardItemInfo */
        getAutoBattleRewardItemInfo: (request: OrtegaRpcMap["item/getAutoBattleRewardItemInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["item/getAutoBattleRewardItemInfo"]["response"]>("item/getAutoBattleRewardItemInfo", request),
        /** item/openTreasureChest */
        openTreasureChest: (request: OrtegaRpcMap["item/openTreasureChest"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["item/openTreasureChest"]["response"]>("item/openTreasureChest", request),
        /** item/useAutoBattleRewardItem */
        useAutoBattleRewardItem: (request: OrtegaRpcMap["item/useAutoBattleRewardItem"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["item/useAutoBattleRewardItem"]["response"]>("item/useAutoBattleRewardItem", request),
    },
    legendLeague: {
        /** legendLeague/buyLegendLeagueIconReward */
        buyLegendLeagueIconReward: (request: OrtegaRpcMap["legendLeague/buyLegendLeagueIconReward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["legendLeague/buyLegendLeagueIconReward"]["response"]>("legendLeague/buyLegendLeagueIconReward", request),
        /** legendLeague/getLegendLeagueIconRewardInfo */
        getLegendLeagueIconRewardInfo: (request: OrtegaRpcMap["legendLeague/getLegendLeagueIconRewardInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["legendLeague/getLegendLeagueIconRewardInfo"]["response"]>("legendLeague/getLegendLeagueIconRewardInfo", request),
    },
    levelLink: {
        /** levelLink/addExp */
        addExp: (request: OrtegaRpcMap["levelLink/addExp"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["levelLink/addExp"]["response"]>("levelLink/addExp", request),
        /** levelLink/levelUp */
        levelUp: (request: OrtegaRpcMap["levelLink/levelUp"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["levelLink/levelUp"]["response"]>("levelLink/levelUp", request),
        /** levelLink/resetCoolTime */
        resetCoolTime: (request: OrtegaRpcMap["levelLink/resetCoolTime"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["levelLink/resetCoolTime"]["response"]>("levelLink/resetCoolTime", request),
        /** levelLink/setMember */
        setMember: (request: OrtegaRpcMap["levelLink/setMember"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["levelLink/setMember"]["response"]>("levelLink/setMember", request),
        /** levelLink/sortMember */
        sortMember: (request: OrtegaRpcMap["levelLink/sortMember"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["levelLink/sortMember"]["response"]>("levelLink/sortMember", request),
        /** levelLink/unSetMember */
        unSetMember: (request: OrtegaRpcMap["levelLink/unSetMember"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["levelLink/unSetMember"]["response"]>("levelLink/unSetMember", request),
    },
    localGvg: {
        /** localGvg/receiveLocalGvgReward */
        receiveLocalGvgReward: (request: OrtegaRpcMap["localGvg/receiveLocalGvgReward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["localGvg/receiveLocalGvgReward"]["response"]>("localGvg/receiveLocalGvgReward", request),
    },
    localRaid: {
        /** localRaid/getLocalRaidBattleLogs */
        getLocalRaidBattleLogs: (request: OrtegaRpcMap["localRaid/getLocalRaidBattleLogs"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["localRaid/getLocalRaidBattleLogs"]["response"]>("localRaid/getLocalRaidBattleLogs", request),
        /** localRaid/getLocalRaidBattleResult */
        getLocalRaidBattleResult: (request: OrtegaRpcMap["localRaid/getLocalRaidBattleResult"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["localRaid/getLocalRaidBattleResult"]["response"]>("localRaid/getLocalRaidBattleResult", request),
        /** localRaid/getLocalRaidBattleResultOld */
        getLocalRaidBattleResultOld: (request: OrtegaRpcMap["localRaid/getLocalRaidBattleResultOld"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["localRaid/getLocalRaidBattleResultOld"]["response"]>("localRaid/getLocalRaidBattleResultOld", request),
        /** localRaid/getLocalRaidFriendInfo */
        getLocalRaidFriendInfo: (request: OrtegaRpcMap["localRaid/getLocalRaidFriendInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["localRaid/getLocalRaidFriendInfo"]["response"]>("localRaid/getLocalRaidFriendInfo", request),
        /** localRaid/getLocalRaidInfo */
        getLocalRaidInfo: (request: OrtegaRpcMap["localRaid/getLocalRaidInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["localRaid/getLocalRaidInfo"]["response"]>("localRaid/getLocalRaidInfo", request),
    },
    loginBonus: {
        /** loginBonus/getLimitedLoginBonusInfo */
        getLimitedLoginBonusInfo: (request: OrtegaRpcMap["loginBonus/getLimitedLoginBonusInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["loginBonus/getLimitedLoginBonusInfo"]["response"]>("loginBonus/getLimitedLoginBonusInfo", request),
        /** loginBonus/getMonthlyLoginBonusInfo */
        getMonthlyLoginBonusInfo: (request: OrtegaRpcMap["loginBonus/getMonthlyLoginBonusInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["loginBonus/getMonthlyLoginBonusInfo"]["response"]>("loginBonus/getMonthlyLoginBonusInfo", request),
        /** loginBonus/receiveDailyLimitedLoginBonus */
        receiveDailyLimitedLoginBonus: (request: OrtegaRpcMap["loginBonus/receiveDailyLimitedLoginBonus"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["loginBonus/receiveDailyLimitedLoginBonus"]["response"]>("loginBonus/receiveDailyLimitedLoginBonus", request),
        /** loginBonus/receiveDailyLoginBonus */
        receiveDailyLoginBonus: (request: OrtegaRpcMap["loginBonus/receiveDailyLoginBonus"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["loginBonus/receiveDailyLoginBonus"]["response"]>("loginBonus/receiveDailyLoginBonus", request),
        /** loginBonus/receiveLoginCountBonus */
        receiveLoginCountBonus: (request: OrtegaRpcMap["loginBonus/receiveLoginCountBonus"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["loginBonus/receiveLoginCountBonus"]["response"]>("loginBonus/receiveLoginCountBonus", request),
        /** loginBonus/receiveSpecialLimitedLoginBonus */
        receiveSpecialLimitedLoginBonus: (request: OrtegaRpcMap["loginBonus/receiveSpecialLimitedLoginBonus"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["loginBonus/receiveSpecialLimitedLoginBonus"]["response"]>("loginBonus/receiveSpecialLimitedLoginBonus", request),
    },
    luckyChance: {
        /** luckyChance/GetDetailInfo */
        GetDetailInfo: (request: OrtegaRpcMap["luckyChance/GetDetailInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["luckyChance/GetDetailInfo"]["response"]>("luckyChance/GetDetailInfo", request),
        /** luckyChance/registerRealPrizeCItemInfo */
        registerRealPrizeCItemInfo: (request: OrtegaRpcMap["luckyChance/registerRealPrizeCItemInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["luckyChance/registerRealPrizeCItemInfo"]["response"]>("luckyChance/registerRealPrizeCItemInfo", request),
        /** luckyChance/SelectButton */
        SelectButton: (request: OrtegaRpcMap["luckyChance/SelectButton"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["luckyChance/SelectButton"]["response"]>("luckyChance/SelectButton", request),
    },
    miningQuest: {
        /** miningQuest/finish */
        finish: (request: OrtegaRpcMap["miningQuest/finish"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["miningQuest/finish"]["response"]>("miningQuest/finish", request),
        /** miningQuest/getBenefitInfo */
        getBenefitInfo: (request: OrtegaRpcMap["miningQuest/getBenefitInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["miningQuest/getBenefitInfo"]["response"]>("miningQuest/getBenefitInfo", request),
        /** miningQuest/getCurrentParameters */
        getCurrentParameters: (request: OrtegaRpcMap["miningQuest/getCurrentParameters"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["miningQuest/getCurrentParameters"]["response"]>("miningQuest/getCurrentParameters", request),
        /** miningQuest/getGradeInfo */
        getGradeInfo: (request: OrtegaRpcMap["miningQuest/getGradeInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["miningQuest/getGradeInfo"]["response"]>("miningQuest/getGradeInfo", request),
        /** miningQuest/getInfo */
        getInfo: (request: OrtegaRpcMap["miningQuest/getInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["miningQuest/getInfo"]["response"]>("miningQuest/getInfo", request),
        /** miningQuest/getMaxRewardCountInfo */
        getMaxRewardCountInfo: (request: OrtegaRpcMap["miningQuest/getMaxRewardCountInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["miningQuest/getMaxRewardCountInfo"]["response"]>("miningQuest/getMaxRewardCountInfo", request),
        /** miningQuest/receiveGradeDailyReward */
        receiveGradeDailyReward: (request: OrtegaRpcMap["miningQuest/receiveGradeDailyReward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["miningQuest/receiveGradeDailyReward"]["response"]>("miningQuest/receiveGradeDailyReward", request),
        /** miningQuest/reinforcement */
        reinforcement: (request: OrtegaRpcMap["miningQuest/reinforcement"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["miningQuest/reinforcement"]["response"]>("miningQuest/reinforcement", request),
        /** miningQuest/start */
        start: (request: OrtegaRpcMap["miningQuest/start"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["miningQuest/start"]["response"]>("miningQuest/start", request),
        /** miningQuest/unlockBenefit */
        unlockBenefit: (request: OrtegaRpcMap["miningQuest/unlockBenefit"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["miningQuest/unlockBenefit"]["response"]>("miningQuest/unlockBenefit", request),
    },
    mission: {
        /** mission/getMissionInfo */
        getMissionInfo: (request: OrtegaRpcMap["mission/getMissionInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["mission/getMissionInfo"]["response"]>("mission/getMissionInfo", request),
        /** mission/receivePanelMissionBingoReward */
        receivePanelMissionBingoReward: (request: OrtegaRpcMap["mission/receivePanelMissionBingoReward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["mission/receivePanelMissionBingoReward"]["response"]>("mission/receivePanelMissionBingoReward", request),
        /** mission/rewardMission */
        rewardMission: (request: OrtegaRpcMap["mission/rewardMission"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["mission/rewardMission"]["response"]>("mission/rewardMission", request),
        /** mission/rewardMissionActivity */
        rewardMissionActivity: (request: OrtegaRpcMap["mission/rewardMissionActivity"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["mission/rewardMissionActivity"]["response"]>("mission/rewardMissionActivity", request),
    },
    notice: {
        /** notice/getMypageNoticeInfoList */
        getMypageNoticeInfoList: (request: OrtegaRpcMap["notice/getMypageNoticeInfoList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["notice/getMypageNoticeInfoList"]["response"]>("notice/getMypageNoticeInfoList", request),
        /** notice/getNoticeInfoList */
        getNoticeInfoList: (request: OrtegaRpcMap["notice/getNoticeInfoList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["notice/getNoticeInfoList"]["response"]>("notice/getNoticeInfoList", request),
    },
    popularityVote: {
        /** popularityVote/getInfo */
        getInfo: (request: OrtegaRpcMap["popularityVote/getInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["popularityVote/getInfo"]["response"]>("popularityVote/getInfo", request),
        /** popularityVote/rewardVoteItem */
        rewardVoteItem: (request: OrtegaRpcMap["popularityVote/rewardVoteItem"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["popularityVote/rewardVoteItem"]["response"]>("popularityVote/rewardVoteItem", request),
    },
    present: {
        /** present/deletePresent */
        deletePresent: (request: OrtegaRpcMap["present/deletePresent"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["present/deletePresent"]["response"]>("present/deletePresent", request),
        /** present/getList */
        getList: (request: OrtegaRpcMap["present/getList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["present/getList"]["response"]>("present/getList", request),
        /** present/receiveItem */
        receiveItem: (request: OrtegaRpcMap["present/receiveItem"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["present/receiveItem"]["response"]>("present/receiveItem", request),
    },
    quest: {
        /** quest/getQuestInfo  */
        getQuestInfo: (request: OrtegaRpcMap["quest/getQuestInfo "]["request"]) =>
            rpcClient.call<OrtegaRpcMap["quest/getQuestInfo "]["response"]>("quest/getQuestInfo ", request),
        /** quest/mapInfo */
        mapInfo: (request: OrtegaRpcMap["quest/mapInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["quest/mapInfo"]["response"]>("quest/mapInfo", request),
    },
    ranking: {
        /** ranking/getAchieveRankingPlayerInfo */
        getAchieveRankingPlayerInfo: (request: OrtegaRpcMap["ranking/getAchieveRankingPlayerInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["ranking/getAchieveRankingPlayerInfo"]["response"]>("ranking/getAchieveRankingPlayerInfo", request),
        /** ranking/getBulkAchieveRankingPlayerInfo */
        getBulkAchieveRankingPlayerInfo: (request: OrtegaRpcMap["ranking/getBulkAchieveRankingPlayerInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["ranking/getBulkAchieveRankingPlayerInfo"]["response"]>("ranking/getBulkAchieveRankingPlayerInfo", request),
        /** ranking/getGuildRanking */
        getGuildRanking: (request: OrtegaRpcMap["ranking/getGuildRanking"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["ranking/getGuildRanking"]["response"]>("ranking/getGuildRanking", request),
        /** ranking/getPlayerRanking */
        getPlayerRanking: (request: OrtegaRpcMap["ranking/getPlayerRanking"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["ranking/getPlayerRanking"]["response"]>("ranking/getPlayerRanking", request),
        /** ranking/getRankingBattleLog */
        getRankingBattleLog: (request: OrtegaRpcMap["ranking/getRankingBattleLog"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["ranking/getRankingBattleLog"]["response"]>("ranking/getRankingBattleLog", request),
        /** ranking/getTowerRanking */
        getTowerRanking: (request: OrtegaRpcMap["ranking/getTowerRanking"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["ranking/getTowerRanking"]["response"]>("ranking/getTowerRanking", request),
        /** ranking/receiveAchieveRankingReward */
        receiveAchieveRankingReward: (request: OrtegaRpcMap["ranking/receiveAchieveRankingReward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["ranking/receiveAchieveRankingReward"]["response"]>("ranking/receiveAchieveRankingReward", request),
    },
    rentalRaid: {
        /** rentalRaid/getShareRewardInfo */
        getShareRewardInfo: (request: OrtegaRpcMap["rentalRaid/getShareRewardInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["rentalRaid/getShareRewardInfo"]["response"]>("rentalRaid/getShareRewardInfo", request),
        /** rentalRaid/receiveShareReward */
        receiveShareReward: (request: OrtegaRpcMap["rentalRaid/receiveShareReward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["rentalRaid/receiveShareReward"]["response"]>("rentalRaid/receiveShareReward", request),
    },
    shop: {
        /** shop/bulkBuyProductWithWebCrystal */
        bulkBuyProductWithWebCrystal: (request: OrtegaRpcMap["shop/bulkBuyProductWithWebCrystal"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["shop/bulkBuyProductWithWebCrystal"]["response"]>("shop/bulkBuyProductWithWebCrystal", request),
        /** shop/buyProduct */
        buyProduct: (request: OrtegaRpcMap["shop/buyProduct"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["shop/buyProduct"]["response"]>("shop/buyProduct", request),
        /** shop/buyProductWithWebCrystal */
        buyProductWithWebCrystal: (request: OrtegaRpcMap["shop/buyProductWithWebCrystal"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["shop/buyProductWithWebCrystal"]["response"]>("shop/buyProductWithWebCrystal", request),
        /** shop/getBoiPaymentDetailHistory */
        getBoiPaymentDetailHistory: (request: OrtegaRpcMap["shop/getBoiPaymentDetailHistory"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["shop/getBoiPaymentDetailHistory"]["response"]>("shop/getBoiPaymentDetailHistory", request),
        /** shop/getBoiPaymentHistoryList */
        getBoiPaymentHistoryList: (request: OrtegaRpcMap["shop/getBoiPaymentHistoryList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["shop/getBoiPaymentHistoryList"]["response"]>("shop/getBoiPaymentHistoryList", request),
        /** shop/getBoiPointHistoryList */
        getBoiPointHistoryList: (request: OrtegaRpcMap["shop/getBoiPointHistoryList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["shop/getBoiPointHistoryList"]["response"]>("shop/getBoiPointHistoryList", request),
        /** shop/getList */
        getList: (request: OrtegaRpcMap["shop/getList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["shop/getList"]["response"]>("shop/getList", request),
        /** shop/getProductList */
        getProductList: (request: OrtegaRpcMap["shop/getProductList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["shop/getProductList"]["response"]>("shop/getProductList", request),
        /** shop/getWebStoreGuidancePanel */
        getWebStoreGuidancePanel: (request: OrtegaRpcMap["shop/getWebStoreGuidancePanel"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["shop/getWebStoreGuidancePanel"]["response"]>("shop/getWebStoreGuidancePanel", request),
        /** shop/receiveReward */
        receiveReward: (request: OrtegaRpcMap["shop/receiveReward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["shop/receiveReward"]["response"]>("shop/receiveReward", request),
        /** shop/registerActionLog */
        registerActionLog: (request: OrtegaRpcMap["shop/registerActionLog"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["shop/registerActionLog"]["response"]>("shop/registerActionLog", request),
    },
    starsGuidanceTradeShop: {
        /** starsGuidanceTradeShop/exchangeItem */
        exchangeItem: (request: OrtegaRpcMap["starsGuidanceTradeShop/exchangeItem"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["starsGuidanceTradeShop/exchangeItem"]["response"]>("starsGuidanceTradeShop/exchangeItem", request),
        /** starsGuidanceTradeShop/getStarsGuidanceTradeShopInfo */
        getStarsGuidanceTradeShopInfo: (request: OrtegaRpcMap["starsGuidanceTradeShop/getStarsGuidanceTradeShopInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["starsGuidanceTradeShop/getStarsGuidanceTradeShopInfo"]["response"]>("starsGuidanceTradeShop/getStarsGuidanceTradeShopInfo", request),
    },
    towerBattle: {
        /** towerBattle/quick */
        quick: (request: OrtegaRpcMap["towerBattle/quick"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["towerBattle/quick"]["response"]>("towerBattle/quick", request),
        /** towerBattle/start */
        start: (request: OrtegaRpcMap["towerBattle/start"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["towerBattle/start"]["response"]>("towerBattle/start", request),
    },
    tradeShop: {
        /** tradeShop/buyItem */
        buyItem: (request: OrtegaRpcMap["tradeShop/buyItem"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["tradeShop/buyItem"]["response"]>("tradeShop/buyItem", request),
        /** tradeShop/getList */
        getList: (request: OrtegaRpcMap["tradeShop/getList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["tradeShop/getList"]["response"]>("tradeShop/getList", request),
        /** tradeShop/resetTab */
        resetTab: (request: OrtegaRpcMap["tradeShop/resetTab"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["tradeShop/resetTab"]["response"]>("tradeShop/resetTab", request),
    },
    unionMission: {
        /** unionMission/acceptMission */
        acceptMission: (request: OrtegaRpcMap["unionMission/acceptMission"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["unionMission/acceptMission"]["response"]>("unionMission/acceptMission", request),
        /** unionMission/buyAcceptCount */
        buyAcceptCount: (request: OrtegaRpcMap["unionMission/buyAcceptCount"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["unionMission/buyAcceptCount"]["response"]>("unionMission/buyAcceptCount", request),
        /** unionMission/entryUnionMission */
        entryUnionMission: (request: OrtegaRpcMap["unionMission/entryUnionMission"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["unionMission/entryUnionMission"]["response"]>("unionMission/entryUnionMission", request),
        /** unionMission/getInfo */
        getInfo: (request: OrtegaRpcMap["unionMission/getInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["unionMission/getInfo"]["response"]>("unionMission/getInfo", request),
        /** unionMission/getRanking */
        getRanking: (request: OrtegaRpcMap["unionMission/getRanking"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["unionMission/getRanking"]["response"]>("unionMission/getRanking", request),
        /** unionMission/receiveIndividualTotalPointReward */
        receiveIndividualTotalPointReward: (request: OrtegaRpcMap["unionMission/receiveIndividualTotalPointReward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["unionMission/receiveIndividualTotalPointReward"]["response"]>("unionMission/receiveIndividualTotalPointReward", request),
        /** unionMission/receiveMissionReward */
        receiveMissionReward: (request: OrtegaRpcMap["unionMission/receiveMissionReward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["unionMission/receiveMissionReward"]["response"]>("unionMission/receiveMissionReward", request),
        /** unionMission/receiveTotalPointReward */
        receiveTotalPointReward: (request: OrtegaRpcMap["unionMission/receiveTotalPointReward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["unionMission/receiveTotalPointReward"]["response"]>("unionMission/receiveTotalPointReward", request),
        /** unionMission/refreshPersonalMission */
        refreshPersonalMission: (request: OrtegaRpcMap["unionMission/refreshPersonalMission"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["unionMission/refreshPersonalMission"]["response"]>("unionMission/refreshPersonalMission", request),
        /** unionMission/refreshSharedMission */
        refreshSharedMission: (request: OrtegaRpcMap["unionMission/refreshSharedMission"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["unionMission/refreshSharedMission"]["response"]>("unionMission/refreshSharedMission", request),
        /** unionMission/resetCoolTimeMission */
        resetCoolTimeMission: (request: OrtegaRpcMap["unionMission/resetCoolTimeMission"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["unionMission/resetCoolTimeMission"]["response"]>("unionMission/resetCoolTimeMission", request),
        /** unionMission/retireMission */
        retireMission: (request: OrtegaRpcMap["unionMission/retireMission"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["unionMission/retireMission"]["response"]>("unionMission/retireMission", request),
        /** unionMission/selectMission */
        selectMission: (request: OrtegaRpcMap["unionMission/selectMission"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["unionMission/selectMission"]["response"]>("unionMission/selectMission", request),
    },
    user: {
        /** user/getMypage */
        getMypage: (request: OrtegaRpcMap["user/getMypage"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["user/getMypage"]["response"]>("user/getMypage", request),
        /** user/getUserData */
        getUserData: (request: OrtegaRpcMap["user/getUserData"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["user/getUserData"]["response"]>("user/getUserData", request),
        /** user/loginPlayer */
        loginPlayer: (request: OrtegaRpcMap["user/loginPlayer"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["user/loginPlayer"]["response"]>("user/loginPlayer", request),
        /** user/saveUsedDeck */
        saveUsedDeck: (request: OrtegaRpcMap["user/saveUsedDeck"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["user/saveUsedDeck"]["response"]>("user/saveUsedDeck", request),
    },
    vip: {
        /** vip/getDailyGift */
        getDailyGift: (request: OrtegaRpcMap["vip/getDailyGift"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["vip/getDailyGift"]["response"]>("vip/getDailyGift", request),
    },
    webStore: {
        /** webStore/getLoginCampaignInfo */
        getLoginCampaignInfo: (request: OrtegaRpcMap["webStore/getLoginCampaignInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["webStore/getLoginCampaignInfo"]["response"]>("webStore/getLoginCampaignInfo", request),
        /** webStore/rewardLoginCampaignItem */
        rewardLoginCampaignItem: (request: OrtegaRpcMap["webStore/rewardLoginCampaignItem"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["webStore/rewardLoginCampaignItem"]["response"]>("webStore/rewardLoginCampaignItem", request),
    },
    weeklyTopics: {
        /** weeklyTopics/getWeeklyTopicsInfo */
        getWeeklyTopicsInfo: (request: OrtegaRpcMap["weeklyTopics/getWeeklyTopicsInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["weeklyTopics/getWeeklyTopicsInfo"]["response"]>("weeklyTopics/getWeeklyTopicsInfo", request),
    },

    /** 通用调用接口 */
    call: <K extends keyof OrtegaRpcMap & string>(
        uri: K,
        request: OrtegaRpcMap[K]['request']
    ) => rpcClient.call<OrtegaRpcMap[K]['response']>(uri, request),
};

export default ortegaApi;
