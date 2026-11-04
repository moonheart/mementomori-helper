/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { GachaCaseInfo } from "./gachaCaseInfo";
import { GachaDestinyLogInfo } from "./gachaDestinyLogInfo";
import { GachaElementInfo } from "./gachaElementInfo";
import { GachaSelectListType } from "./gachaSelectListType";
import { GachaStarsGuidanceLogInfo } from "./gachaStarsGuidanceLogInfo";
import { UserSyncData } from "./userSyncData";

export class GachaGetListResponse extends ApiResponseBase implements IUserSyncApiResponse {
    gachaCaseInfoList: GachaCaseInfo[];
    gachaDestinyLogInfoList: GachaDestinyLogInfo[];
    gachaElementInfo: GachaElementInfo;
    gachaSelectListCharacterIdMap: { [key in GachaSelectListType]?: number[]; };
    gachaStarsGuidanceLogInfoList: GachaStarsGuidanceLogInfo[];
    isGetFirstBonusRelicGacha: boolean;
    isGetSecondBonusRelicGacha: boolean;
    isFreeChangeRelicGacha: boolean;
    userSyncData: UserSyncData;
}
