/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IGuildSyncApiResponse } from "./iGuildSyncApiResponse";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { BattleResult } from "./battleResult";
import { BattleRewardResult } from "./battleRewardResult";
import { GuildSyncData } from "./guildSyncData";
import { UserSyncData } from "./userSyncData";

export class GuildTowerBattleResponse extends ApiResponseBase implements IGuildSyncApiResponse, IUserSyncApiResponse {
    battleResult: BattleResult;
    battleRewardResult: BattleRewardResult;
    guildSyncData: GuildSyncData;
    userSyncData: UserSyncData;
}
