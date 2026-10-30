/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { TreasureChestReward } from "./treasureChestReward";
import { UserSyncData } from "./userSyncData";

export class ItemOpenTreasureChestResponse extends ApiResponseBase implements IUserSyncApiResponse {
    rewardItems: TreasureChestReward[];
    userSyncData: UserSyncData;
}
