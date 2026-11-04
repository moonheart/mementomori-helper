/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { GachaSelectListType } from "./gachaSelectListType";
import { GachaCaseInfo } from "./gachaCaseInfo";
import { UserSyncData } from "./userSyncData";

export class GachaSetSelectListResponse extends ApiResponseBase implements IUserSyncApiResponse {
    gachaSelectListType: GachaSelectListType;
    setCharacterIdList: number[];
    gachaCaseInfoList: GachaCaseInfo[];
    userSyncData: UserSyncData;
}
