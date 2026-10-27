/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { UserItem } from "./userItem";
import { WorldRewardInfo } from "./worldRewardInfo";
import { UserSyncData } from "./userSyncData";

export class GuildRaidGiveGuildRaidWorldRewardItemResponse extends ApiResponseBase implements IUserSyncApiResponse {
    rewardItems: UserItem[];
    totalWorldDamage: number;
    worldRewardInfos: WorldRewardInfo[];
    userSyncData: UserSyncData;
}
