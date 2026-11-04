/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { BattleResult } from "./battleResult";
import { PlayerInfo } from "./playerInfo";
import { UserSyncData } from "./userSyncData";

export class BattleLegendLeagueStartResponse extends ApiResponseBase implements IUserSyncApiResponse {
    battleResult: BattleResult;
    canBattle: boolean;
    getPoint: number;
    beforePoint: number;
    ranking: number;
    beforeRanking: number;
    rivalPlayerInfo: PlayerInfo;
    userSyncData: UserSyncData;
    point: number;
}
