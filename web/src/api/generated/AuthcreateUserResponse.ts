/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { AccountMessageInfo } from "./accountMessageInfo";
import { WarningMessageInfo } from "./warningMessageInfo";
import { UserSyncData } from "./userSyncData";

export class AuthCreateUserResponse extends ApiResponseBase implements IUserSyncApiResponse {
    accountMessageInfo: AccountMessageInfo;
    clientKey: string;
    recommendWorldId: number;
    userId: number;
    warningMessageInfos: WarningMessageInfo[];
    worldIdList: number[];
    specialWorldDict: { [key: number]: string; };
    userSyncData: UserSyncData;
}
