/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { BattleReward } from "./battleReward";
import { BattleRewardResult } from "./battleRewardResult";
import { UserSyncData } from "./userSyncData";

export class BattleBossQuickResponse extends ApiResponseBase implements IUserSyncApiResponse {
    battleRewardList: BattleReward[];
    battleRewardResult: BattleRewardResult;
    consumeBossChallengeTicketCount: number;
    userSyncData: UserSyncData;
}
