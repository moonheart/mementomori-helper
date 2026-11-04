/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { AutoBattleRewardResult } from "./autoBattleRewardResult";
import { UserSyncData } from "./userSyncData";

export class BattleRewardAutoBattleResponse extends ApiResponseBase implements IUserSyncApiResponse {
    autoBattleRewardResult: AutoBattleRewardResult;
    userSyncData: UserSyncData;
}
