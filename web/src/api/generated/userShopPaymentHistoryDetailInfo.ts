/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { PurchasePointGroupType } from "./purchasePointGroupType";
import { ShopPaymentStateType } from "./shopPaymentStateType";
import { ShopPayType } from "./shopPayType";
import { BulkShopRewardInfo } from "./bulkShopRewardInfo";

export class UserShopPaymentHistoryDetailInfo {
    buyDateTime: Date;
    transactionId: string;
    purchasePointGroupType: PurchasePointGroupType;
    price: number;
    usePoint: number;
    discountPrice: number;
    shopPaymentStateType: ShopPaymentStateType;
    shopPayType: ShopPayType;
    savePoint: number;
    worldId: number;
    bulkShopRewardInfos: BulkShopRewardInfo[];
}
