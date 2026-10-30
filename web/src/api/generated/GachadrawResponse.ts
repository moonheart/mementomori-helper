/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { GachaResultItem } from "./gachaResultItem";
import { UserItem } from "./userItem";
import { GachaCaseInfo } from "./gachaCaseInfo";
import { GachaDestinyLogInfo } from "./gachaDestinyLogInfo";
import { UserSyncData } from "./userSyncData";

export class GachaDrawResponse extends ApiResponseBase implements IUserSyncApiResponse {
    bonusRewardItemList: GachaResultItem[];
    characterReleaseItemList: UserItem[];
    gachaCaseInfoList: GachaCaseInfo[];
    gachaDestinyLogInfoList: GachaDestinyLogInfo[];
    gachaRewardAddItemList: UserItem[];
    gachaRewardItemList: GachaResultItem[];
    isFreeChangeRelicGacha: boolean;
    isGetFirstBonusRelicGacha: boolean;
    isGetSecondBonusRelicGacha: boolean;
    userSyncData: UserSyncData;
}
