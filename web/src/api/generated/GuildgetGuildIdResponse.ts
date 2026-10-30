/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { UserSyncData } from "./userSyncData";

export class GuildGetGuildIdResponse extends ApiResponseBase implements IUserSyncApiResponse {
    guildId: number;
    userSyncData: UserSyncData;
}
