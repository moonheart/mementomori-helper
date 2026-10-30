/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { PlayerInfo } from "./playerInfo";
import { UserSyncData } from "./userSyncData";

export class GuildGetGuildMemberInfoResponse extends ApiResponseBase implements IUserSyncApiResponse {
    playerInfoList: PlayerInfo[];
    userSyncData: UserSyncData;
}
