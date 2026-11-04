/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { GachaCaseInfo } from "./gachaCaseInfo";
import { UserSyncData } from "./userSyncData";

export class GachaChangeGachaRelicResponse extends ApiResponseBase implements IUserSyncApiResponse {
    gachaCaseInfoList: GachaCaseInfo[];
    userSyncData: UserSyncData;
}
