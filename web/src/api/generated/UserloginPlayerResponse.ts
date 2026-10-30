/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { BanChatInfo } from "./banChatInfo";
import { UserSyncData } from "./userSyncData";

export class UserLoginPlayerResponse extends ApiResponseBase implements IUserSyncApiResponse {
    authTokenOfMagicOnion: string;
    banChatInfo: BanChatInfo;
    userSyncData: UserSyncData;
}
