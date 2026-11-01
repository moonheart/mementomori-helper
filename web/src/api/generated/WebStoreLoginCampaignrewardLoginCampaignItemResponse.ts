/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { RarityItem } from "./rarityItem";
import { UserSyncData } from "./userSyncData";

export class WebStoreLoginCampaignRewardLoginCampaignItemResponse extends ApiResponseBase implements IUserSyncApiResponse {
    rewardItemList: RarityItem[];
    userSyncData: UserSyncData;
}
