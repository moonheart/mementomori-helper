/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { AcquisitionShopRewardInfo } from "./acquisitionShopRewardInfo";
import { ShopProductInfo } from "./shopProductInfo";
import { UserSyncData } from "./userSyncData";

export class ShopReceiveRewardResponse extends ApiResponseBase implements IUserSyncApiResponse {
    rewardInfo: AcquisitionShopRewardInfo;
    shopProductInfo: ShopProductInfo;
    userSyncData: UserSyncData;
}
