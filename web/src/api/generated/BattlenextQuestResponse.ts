/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { UserBattleAutoDtoInfo } from "./userBattleAutoDtoInfo";
import { UserSyncData } from "./userSyncData";

export class BattleNextQuestResponse extends ApiResponseBase implements IUserSyncApiResponse {
    autoBattleDropEquipmentPercent: number;
    currentQuestAutoEnemyIds: number[];
    remainNextRankUpTime: number;
    userBattleAuto: UserBattleAutoDtoInfo;
    userSyncData: UserSyncData;
}
