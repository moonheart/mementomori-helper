/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { BattleResult } from "./battleResult";
import { BattleRewardResult } from "./battleRewardResult";
import { UserGuildRaidDtoInfo } from "./userGuildRaidDtoInfo";
import { UserSyncData } from "./userSyncData";

export class GuildRaidStartGuildRaidResponse extends ApiResponseBase implements IUserSyncApiResponse {
    battleResult: BattleResult;
    battleRewardResult: BattleRewardResult;
    userGuildRaidDtoInfo: UserGuildRaidDtoInfo;
    userSyncData: UserSyncData;
}
