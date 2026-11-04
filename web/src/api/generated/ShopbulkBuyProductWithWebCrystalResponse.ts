/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { BulkShopRewardInfo } from "./bulkShopRewardInfo";
import { ShopProductInfo } from "./shopProductInfo";
import { UserSyncData } from "./userSyncData";

export class ShopBulkBuyProductWithWebCrystalResponse extends ApiResponseBase implements IUserSyncApiResponse {
    bulkShopRewardInfos: BulkShopRewardInfo[];
    shopProductInfos: ShopProductInfo[];
    userSyncData: UserSyncData;
}
