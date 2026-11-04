/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IGuildSyncApiResponse } from "./iGuildSyncApiResponse";
import { GuildSyncData } from "./guildSyncData";

export class UnionMissionEntryUnionMissionResponse extends ApiResponseBase implements IGuildSyncApiResponse {
    guildSyncData: GuildSyncData;
}
