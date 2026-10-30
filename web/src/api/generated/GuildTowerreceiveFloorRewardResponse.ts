/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IGuildSyncApiResponse } from "./iGuildSyncApiResponse";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { UserItem } from "./userItem";
import { GuildSyncData } from "./guildSyncData";
import { UserSyncData } from "./userSyncData";

export class GuildTowerReceiveFloorRewardResponse extends ApiResponseBase implements IGuildSyncApiResponse, IUserSyncApiResponse {
    rewardItemList: UserItem[];
    guildSyncData: GuildSyncData;
    userSyncData: UserSyncData;
}
