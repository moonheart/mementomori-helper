/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { PlayerInfo } from "./playerInfo";
import { UserSyncData } from "./userSyncData";

export class RankingGetPlayerRankingResponse extends ApiResponseBase implements IUserSyncApiResponse {
    autoRanking: number;
    autoRankingPlayerList: PlayerInfo[];
    autoRankingToday: number;
    battlePower: number;
    battlePowerRanking: number;
    battlePowerRankingPlayerList: PlayerInfo[];
    battlePowerRankingToday: number;
    playerRankRanking: number;
    playerRankRankingPlayerList: PlayerInfo[];
    playerRankRankingToday: number;
    towerBattleRanking: number;
    towerBattleRankingPlayerList: PlayerInfo[];
    towerBattleRankingToday: number;
    userSyncData: UserSyncData;
}
