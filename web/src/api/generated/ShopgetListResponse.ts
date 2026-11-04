/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { ShopTabInfo } from "./shopTabInfo";
import { ShopBannerInfo } from "./shopBannerInfo";

export class ShopGetListResponse extends ApiResponseBase {
    shopTabInfoList: ShopTabInfo[];
    shopBannerInfo: ShopBannerInfo;
}
