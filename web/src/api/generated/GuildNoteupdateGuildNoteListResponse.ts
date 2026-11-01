/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { GuildNoteInfo } from "./guildNoteInfo";
import { UserSyncData } from "./userSyncData";

export class GuildNoteUpdateGuildNoteListResponse extends ApiResponseBase implements IUserSyncApiResponse {
    guildNoteInfoList: GuildNoteInfo[];
    userSyncData: UserSyncData;
}
