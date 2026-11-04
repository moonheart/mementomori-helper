/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { BattleResult } from "./battleResult";
import { BattleRewardResult } from "./battleRewardResult";
import { PlayerInfo } from "./playerInfo";
import { UserSyncData } from "./userSyncData";

export class BattlePvpStartResponse extends ApiResponseBase implements IUserSyncApiResponse {
    afterRank: number;
    battleResult: BattleResult;
    battleRewardResult: BattleRewardResult;
    beforeRank: number;
    canBattle: boolean;
    isNewRecord: boolean;
    rivalBattlePower: number;
    rivalPlayerInfo: PlayerInfo;
    userSyncData: UserSyncData;
}
