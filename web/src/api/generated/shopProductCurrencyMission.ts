/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ShopCurrencyMissionInfo } from "./shopCurrencyMissionInfo";
import { ShopCurrencyMissionType } from "./shopCurrencyMissionType";
import { UserItem } from "./userItem";
import { UserShopCurrencyMissionRewardDtoInfo } from "./userShopCurrencyMissionRewardDtoInfo";

export class ShopProductCurrencyMission {
    currentPoint: number;
    dialogImageId: number;
    endDateTime: string;
    explanationKey: string;
    isDisplayBuyPointButton: boolean;
    isPremium: boolean;
    nameKey: string;
    panelImageId: number;
    productId: string;
    shopProductPrice: number;
    shopCurrencyMissionInfoList: ShopCurrencyMissionInfo[];
    shopCurrencyMissionType: ShopCurrencyMissionType;
    summaryKey: string;
    userItem: UserItem;
    userShopCurrencyMissionRewardDtoInfoList: UserShopCurrencyMissionRewardDtoInfo[];
}
