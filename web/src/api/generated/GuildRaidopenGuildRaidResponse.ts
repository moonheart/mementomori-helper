/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IGuildSyncApiResponse } from "./iGuildSyncApiResponse";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { GuildRaidBossInfo } from "./guildRaidBossInfo";
import { GuildSyncData } from "./guildSyncData";
import { UserSyncData } from "./userSyncData";

export class GuildRaidOpenGuildRaidResponse extends ApiResponseBase implements IGuildSyncApiResponse, IUserSyncApiResponse {
    guildRaidBossInfo: GuildRaidBossInfo;
    guildSyncData: GuildSyncData;
    userSyncData: UserSyncData;
}
