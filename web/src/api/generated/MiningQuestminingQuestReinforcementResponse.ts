/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { UserSyncData } from "./userSyncData";

export class MiningQuestMiningQuestReinforcementResponse extends ApiResponseBase implements IUserSyncApiResponse {
    currentAttackPower: number;
    currentAttackFrequency: number;
    currentCollectionLevel: number;
    afterLevel: number;
    isDisplayMissionBadge: boolean;
    userSyncData: UserSyncData;
}
