/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IPlayerIconInfo } from "./iPlayerIconInfo";
import { UserCharacterInfo } from "./userCharacterInfo";
import { FriendStatusType } from "./friendStatusType";
import { PlayerGuildPositionType } from "./playerGuildPositionType";
import { LegendLeagueClassType } from "./legendLeagueClassType";
import { PlayerRecruitType } from "./playerRecruitType";
import { PlayerCommunicationPolicyType } from "./playerCommunicationPolicyType";
import { PlayerEventPolicyType } from "./playerEventPolicyType";
import { PlayerGuildBattlePolicyType } from "./playerGuildBattlePolicyType";

export class PlayerInfo implements IPlayerIconInfo {
    backgroundCharacterId: number;
    deckUserCharacterInfoList: UserCharacterInfo[];
    battlePower: number;
    comment: string;
    cumulativeGuildFame: number;
    friendStatus: FriendStatusType;
    guildId: number;
    guildJoinRequestUtcTimeStamp: number;
    guildJoinTimeStamp: number;
    guildName: string;
    guildPeriodTotalFame: number;
    isBlock: boolean;
    isRecruit: boolean;
    lastLoginTime: number;
    latestQuestId: number;
    latestTowerBattleQuestId: number;
    localRaidBattlePower: number;
    mainCharacterIconId: number;
    mainCharacterIconEffectId: number;
    npcNameKey: string;
    playerGuildPositionType: PlayerGuildPositionType;
    playerId: number;
    playerLevel: number;
    playerName: string;
    prevLegendLeagueClass: LegendLeagueClassType;
    recruitGuildMemberTimeStamp: number;
    playerRecruitType: PlayerRecruitType;
    communicationPolicyType: PlayerCommunicationPolicyType;
    eventPolicyType: PlayerEventPolicyType;
    guildBattlePolicyType: PlayerGuildBattlePolicyType;
    battleLeagueRankingToday: number;
    legendLeagueRankingToday: number;
    legendLeaguePointToday: number;
    chatBalloonItemId: number;
    isAllowedFriendBattle: boolean;
}
