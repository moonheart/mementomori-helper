/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiErrorResponse } from "./apiErrorResponse";
import { IErrorResponse } from "./iErrorResponse";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { BattleResult } from "./battleResult";
import { BattleRewardResult } from "./battleRewardResult";
import { UserSyncData } from "./userSyncData";

export class BattleBossResponse extends ApiErrorResponse implements IErrorResponse, IUserSyncApiResponse {
    battleResult: BattleResult;
    battleRewardResult: BattleRewardResult;
    userSyncData: UserSyncData;
}
