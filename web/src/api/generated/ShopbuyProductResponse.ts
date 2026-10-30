/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { AcquisitionShopRewardInfo } from "./acquisitionShopRewardInfo";
import { ShopProductInfo } from "./shopProductInfo";
import { UserStripePointHistoryInfo } from "./userStripePointHistoryInfo";
import { UserSyncData } from "./userSyncData";

export class ShopBuyProductResponse extends ApiResponseBase implements IUserSyncApiResponse {
    givenPlayerId: number;
    givenPlayerName: string;
    rewardInfo: AcquisitionShopRewardInfo;
    shopProductInfo: ShopProductInfo;
    userStripePointHistoryInfo: UserStripePointHistoryInfo;
    userSyncData: UserSyncData;
}
