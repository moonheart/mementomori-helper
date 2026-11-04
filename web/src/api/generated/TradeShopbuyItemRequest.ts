/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { TradeShopItemInfo } from "./tradeShopItemInfo";

export class TradeShopBuyItemRequest extends ApiRequestBase {
    tradeShopTabId: number;
    tradeShopItemInfos: TradeShopItemInfo[];
    tradeShopSphereId: number;
    tradeSphereCount: number;
}
