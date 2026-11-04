/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { LegendLeagueRankingPlayerInfo } from "./legendLeagueRankingPlayerInfo";
import { UserSyncData } from "./userSyncData";

export class BattleGetLegendLeagueInfoResponse extends ApiResponseBase implements IUserSyncApiResponse {
    consecutiveVictoryCount: number;
    currentPoint: number;
    currentRanking: number;
    existNewDefenseBattleLog: boolean;
    isDisplayRewardBadge: boolean;
    isInTimeCanChallenge: boolean;
    matchingRivalList: LegendLeagueRankingPlayerInfo[];
    pvpBattlePower: number;
    topRankerList: LegendLeagueRankingPlayerInfo[];
    userSyncData: UserSyncData;
}
