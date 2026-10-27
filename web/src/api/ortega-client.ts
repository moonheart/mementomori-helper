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
        /** battle/getPvpInfo */
        getPvpInfo: (request: OrtegaRpcMap["battle/getPvpInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["battle/getPvpInfo"]["response"]>("battle/getPvpInfo", request),
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
        /** character/getCharacterStoryReward */
        getCharacterStoryReward: (request: OrtegaRpcMap["character/getCharacterStoryReward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["character/getCharacterStoryReward"]["response"]>("character/getCharacterStoryReward", request),
        /** character/getDetailsInfo */
        getDetailsInfo: (request: OrtegaRpcMap["character/getDetailsInfo"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["character/getDetailsInfo"]["response"]>("character/getDetailsInfo", request),
        /** character/rankUp */
        rankUp: (request: OrtegaRpcMap["character/rankUp"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["character/rankUp"]["response"]>("character/rankUp", request),
        /** character/readCharacterStory */
        readCharacterStory: (request: OrtegaRpcMap["character/readCharacterStory"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["character/readCharacterStory"]["response"]>("character/readCharacterStory", request),
    },
    debug: {
        /** debug/giveAllSrCharacter */
        giveAllSrCharacter: (request: OrtegaRpcMap["debug/giveAllSrCharacter"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["debug/giveAllSrCharacter"]["response"]>("debug/giveAllSrCharacter", request),
        /** debug/setVipLevel */
        setVipLevel: (request: OrtegaRpcMap["debug/setVipLevel"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["debug/setVipLevel"]["response"]>("debug/setVipLevel", request),
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
    shop: {
        /** shop/buyProduct */
        buyProduct: (request: OrtegaRpcMap["shop/buyProduct"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["shop/buyProduct"]["response"]>("shop/buyProduct", request),
        /** shop/getList */
        getList: (request: OrtegaRpcMap["shop/getList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["shop/getList"]["response"]>("shop/getList", request),
        /** shop/getProductList */
        getProductList: (request: OrtegaRpcMap["shop/getProductList"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["shop/getProductList"]["response"]>("shop/getProductList", request),
        /** shop/receiveReward */
        receiveReward: (request: OrtegaRpcMap["shop/receiveReward"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["shop/receiveReward"]["response"]>("shop/receiveReward", request),
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
    },
    vip: {
        /** vip/getDailyGift */
        getDailyGift: (request: OrtegaRpcMap["vip/getDailyGift"]["request"]) =>
            rpcClient.call<OrtegaRpcMap["vip/getDailyGift"]["response"]>("vip/getDailyGift", request),
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
