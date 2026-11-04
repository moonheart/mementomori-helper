/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { UserItem } from "./userItem";
import { SacredTreasureType } from "./sacredTreasureType";

export class TradeShopItem {
    tradeShopItemId: number;
    consumeItem1: UserItem;
    consumeItem2: UserItem;
    giveItem: UserItem;
    salePercent: number;
    tradeCount: number;
    limitTradeCount: number;
    sacredTreasureType: SacredTreasureType;
    sortOrder: number;
    requiredCharacterId: number;
    disabled: boolean;
    expirationTimeStamp: number;
    dedicatedItemId: number;
    isDedicated: boolean;
}
