/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { PurchasePointGroupType } from "./purchasePointGroupType";
import { ShopPaymentStateType } from "./shopPaymentStateType";
import { ShopPayType } from "./shopPayType";

export class UserShopPaymentHistoryInfo {
    purchasePointGroupType: PurchasePointGroupType;
    buyDateTime: Date;
    discountPrice: number;
    productNameKey: string;
    transactionId: string;
    shopPaymentStateType: ShopPaymentStateType;
    shopPayType: ShopPayType;
    buyCount: number;
    playerId: number;
    worldId: number;
}
