/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { PvpOption } from "./pvpOption";
import { LocalRaidConfig } from "./localRaidConfig";
import { FriendManageOption } from "./friendManageOption";
import { GuildTowerOption } from "./guildTowerOption";
import { BountyQuestOption } from "./bountyQuestOption";
import { BountyQuestAutoConfig } from "./bountyQuestAutoConfig";
import { GachaConfigModel } from "./gachaConfigModel";
import { QuickActionType } from "./quickActionType";

export class PlayerOption {
    playerId: number;
    battleLeague: PvpOption = {"selectStrategy":0,"characterFilters":[],"excludePlayerIds":[]};
    legendLeague: PvpOption = {"selectStrategy":0,"characterFilters":[],"excludePlayerIds":[]};
    localRaid: LocalRaidConfig = {"rewardItems":[],"selfCreateRoom":false,"waitSeconds":3};
    friendManage: FriendManageOption = {"autoRemoveInactiveFriend":false,"autoRemoveWhitelist":[],"autoSendFriendRequest":false,"autoAcceptFriendRequest":false};
    guildTower: GuildTowerOption = {"autoEntry":false,"autoChallenge":false,"autoChallengeRetryCount":10,"autoReinforcement":false,"autoReceiveReward":false};
    bountyQuest: BountyQuestOption = {"maxRefreshCount":0,"todayRefreshCount":{}};
    bountyQuestAuto: BountyQuestAutoConfig = {"targetItems":[]};
    gachaConfig: GachaConfigModel = {"autoGachaConsumeUserItems":[],"targetRelicType":0,"autoGachaRelic":false};
    quickActionSwitch: { [key in QuickActionType]?: boolean; } = {};
}
