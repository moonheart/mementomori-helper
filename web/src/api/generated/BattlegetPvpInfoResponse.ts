/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { PvpRankingPlayerInfo } from "./pvpRankingPlayerInfo";
import { UserSyncData } from "./userSyncData";

export class BattleGetPvpInfoResponse extends ApiResponseBase implements IUserSyncApiResponse {
    currentRank: number;
    existNewDefenseBattleLog: boolean;
    matchingRivalList: PvpRankingPlayerInfo[];
    topRankerList: PvpRankingPlayerInfo[];
    userSyncData: UserSyncData;
}
